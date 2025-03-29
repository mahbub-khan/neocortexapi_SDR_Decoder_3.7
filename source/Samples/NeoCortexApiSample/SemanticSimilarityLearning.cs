using NeoCortexApi.Encoders;
using NeoCortexApi.Entities;
using NeoCortexApi.Network;
using NeoCortexApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeoCortexApi.Classifiers;
using System.Text.RegularExpressions;

namespace NeoCortexApiSample
{
    class SemanticSimilarityLearning
    {
        /// <summary>
        /// Runs the experiment to store active column and cell SDRs.
        /// </summary>
        /// <param name="sequences">Dictionary of sequences. KEY is the sequence name, VALUE is the list of elements in the sequence.</param>
        public void Run(Dictionary<string, List<double>> sequences)
        {
            Console.WriteLine($"Hello NeocortexApi! Experiment {nameof(MultiSequenceLearning)}");

            int inputBits = 100;
            int numColumns = 1024;

            // Configure HTM parameters
            HtmConfig cfg = new HtmConfig(new int[] { inputBits }, new int[] { numColumns })
            {
                Random = new ThreadSafeRandom(42),
                CellsPerColumn = 25,
                GlobalInhibition = true,
                LocalAreaDensity = -1,
                NumActiveColumnsPerInhArea = 0.02 * numColumns,
                PotentialRadius = (int)(0.15 * inputBits),
                MaxBoost = 10.0,
                DutyCyclePeriod = 25,
                MinPctOverlapDutyCycles = 0.75,
                MaxSynapsesPerSegment = (int)(0.02 * numColumns),
                ActivationThreshold = 15,
                ConnectedPermanence = 0.5,
                PermanenceDecrement = 0.25,
                PermanenceIncrement = 0.15,
                PredictedSegmentDecrement = 0.1
            };

            // Find the maximum value in the sequences
            double max = sequences.Values.SelectMany(list => list).Max();

            // Configure the scalar encoder
            Dictionary<string, object> settings = new Dictionary<string, object>()
            {
                { "W", 15 },
                { "N", inputBits },
                { "Radius", -1.0 },
                { "MinVal", 0.0 },
                { "Periodic", false },
                { "Name", "scalar" },
                { "ClipInput", false },
                { "MaxVal", max }
            };

            EncoderBase encoder = new ScalarEncoder(settings);

            // Run the experiment
            RunExperiment(inputBits, cfg, encoder, sequences);
        }

        /// <summary>
        /// Runs the experiment to store active column and cell SDRs.
        /// </summary>
        private void RunExperiment(int inputBits, HtmConfig cfg, EncoderBase encoder, Dictionary<string, List<double>> sequences)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            var mem = new Connections(cfg);
            bool isInStableState = false;
            int numStableCycles = 0;

            CortexLayer<object, object> layer1 = new CortexLayer<object, object>("L1");

            TemporalMemory tm = new TemporalMemory();
            var numUniqueInputs = GetNumberOfInputs(sequences);

            // Initialize Spatial Pooler with homeostatic plasticity controller
            HomeostaticPlasticityController hpc = new HomeostaticPlasticityController(mem, numUniqueInputs * 150,
                (isStable, numPatterns, actColAvg, seenInputs) =>
                {
                    
                    if (isStable)
                        Debug.WriteLine($"STABLE: Patterns: {numPatterns}, Inputs: {seenInputs}, iteration: {seenInputs / numPatterns}");

                    isInStableState = isStable;
                }, numOfCyclesToWaitOnChange: 5);

            SpatialPoolerMT sp = new SpatialPoolerMT(hpc);
            sp.Init(mem);
            tm.Init(mem);

            // Add encoder and spatial pooler to the layer
            layer1.HtmModules.Add("encoder", encoder);
            layer1.HtmModules.Add("sp", sp);

            // Dictionary to store SDRs for each input
            SDRStorage sdrStorage = new SDRStorage();

            //
            // Training SP to get stable. New-born stage.
            //
            int maxCycles = 20;
            int cycle = 0;
            for (int i = 0; i < maxCycles && isInStableState == false; i++)
            {

                cycle++;

                Debug.WriteLine($"-------------- Newborn Cycle {cycle} ---------------");

                foreach (var inputs in sequences)
                {
                    foreach (var input in inputs.Value)
                    {
                        Debug.WriteLine($" -- {inputs.Key} - {input} --");

                        var lyrOut = layer1.Compute(input, true);

                        if (isInStableState)
                            break;
                    }

                    if (isInStableState)
                        break;
                }
            }

            // We activate here the Temporal Memory algorithm.
            layer1.HtmModules.Add("tm", tm);
            // Process each sequence
            foreach (var sequenceKeyPair in sequences)
            {
                Debug.WriteLine($"Processing sequence: {sequenceKeyPair.Key}");
                numStableCycles = 0; // Reset for each sequence
                // Set on true if the system has learned the sequence with a maximum acurracy.
                bool isLearningCompleted = false;
                for (int i = 0; i < maxCycles; i++) // Loop until stability is reached
                {
                    foreach (var input in sequenceKeyPair.Value)
                    {
                        Debug.WriteLine($"-------------- {input} ---------------");

                        var lyrOut = layer1.Compute(input, true) as ComputeCycle;

                        var activeColumns = layer1.GetResult("sp") as int[];

                        // Determine whether to use ActiveCells or WinnerCells
                        List<Cell> actCells;

                        if (lyrOut.ActiveCells.Count == lyrOut.WinnerCells.Count)
                        {
                            actCells = lyrOut.ActiveCells;
                        }
                        else
                        {
                            actCells = lyrOut.WinnerCells;
                        }

                        int[] cellSDR = actCells.Select(c => c.Index).ToArray();
                        Debug.WriteLine($"Col  SDR: {Helpers.StringifyVector(lyrOut.ActivColumnIndicies)}");
                        Debug.WriteLine($"Cell SDR: {Helpers.StringifyVector(actCells.Select(c => c.Index).ToArray())}");

                        // Save the SDR for this token/input
                        sdrStorage.SaveSDR(input.ToString(), activeColumns, cellSDR);

                    }

                    // Check for stability
                    if (isInStableState)
                    {
                        numStableCycles++;
                        Debug.WriteLine($"Stable cycle detected: {numStableCycles}");
                    }

                    // Exit condition after 5 stable cycles
                    if (numStableCycles > 30)
                    {
                        sw.Stop();
                        isLearningCompleted = true;
                        Debug.WriteLine($"Reached 30 stable cycles for sequence: {sequenceKeyPair.Key}");
                        break;
                    }
                }
            }

            sw.Stop();
            Debug.WriteLine($"Experiment completed. Elapsed time: {sw.Elapsed}");
        }


        /// <summary>
        /// Gets the number of all unique inputs.
        /// </summary>
        /// <param name="sequences">Alle sequences.</param>
        /// <returns></returns>
        private int GetNumberOfInputs(Dictionary<string, List<double>> sequences)
        {
            int num = 0;

            foreach (var inputs in sequences)
            {
                //num += inputs.Value.Distinct().Count();
                num += inputs.Value.Count;
            }

            return num;
        }
    }

}



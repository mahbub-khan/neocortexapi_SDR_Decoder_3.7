using NeoCortexApi;
using NeoCortexApi.Encoders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static NeoCortexApiSample.MultiSequenceLearning;

using System.IO;
using System.Collections;

namespace NeoCortexApiSample
{
    class Program
    {
        /// <summary>
        /// This sample shows a typical experiment code for SP and TM.
        /// You must start this code in debugger to follow the trace.
        /// and TM.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            
           
            //
            // Starts experiment that demonstrates how to learn spatial patterns.
            //SpatialPatternLearning experiment = new SpatialPatternLearning();
            //experiment.Run();

            // Starts experiment For the Image Inputs how to learn spatial patterns.
            // ImageBinarizerSpatialPattern experiment = new ImageBinarizerSpatialPattern();
            // experiment.Run();


            //
            // Starts experiment that demonstrates how to learn spatial patterns.
            //SequenceLearning experiment = new SequenceLearning();
            //experiment.Run();

            //GridCellSamples gridCells = new GridCellSamples();
            //gridCells.Run();

            //RunMultiSimpleSequenceLearningExperiment();


            RunMultiSequenceLearningExperiment();
        }

        private static void RunMultiSimpleSequenceLearningExperiment()
        {
            Dictionary<string, List<double>> sequences = new Dictionary<string, List<double>>();
            sequences.Add("S1", new List<double>(new double[] { 924.0, 220.0, 649.0, 990.0, 307.0, 220.0, 461.0, 211.0, 866.0, 788.0, 517.0, 1302.0 }));

            //sequences.Add("S1", new List<double>(new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, }));
            //sequences.Add("S2", new List<double>(new double[] { 10.0, 11.0, 12.0, 13.0, 14.0, 15.0, 16.0 }));

            //
            // Prototype for building the prediction engine.
            MultiSequenceLearning experiment = new MultiSequenceLearning();
            var predictor = experiment.Run(sequences);
        }


        /// <summary>
        /// This example demonstrates how to learn two sequences and how to use the prediction mechanism.
        /// First, two sequences are learned.
        /// Second, three short sequences with three elements each are created und used for prediction. The predictor used by experiment privides to the HTM every element of every predicting sequence.
        /// The predictor tries to predict the next element.
        /// </summary>
        private static void RunMultiSequenceLearningExperiment()
        {
            string textInput = "input.txt";  // Example input
            if (!File.Exists(textInput))
            {
                Console.WriteLine("File not found!");
                return;
            }

            // Read the text file
            string text = File.ReadAllText(textInput);
            List_Generation list_Generation = new List_Generation();

            // tokenId generation
            List<int> tokenIds = list_Generation.TokenizeText(text);

            // Convert to a list of doubles
            List<double> doubleList = tokenIds.Select(i => (double)i).ToList();

            // Store in a dictionary with a key like "S1"
            Dictionary<string, List<double>> sequences = new Dictionary<string, List<double>>();
            sequences.Add("S1", new List<double>(doubleList));

            // Print to verify the sequence
            // Console.WriteLine("Generated Sequence:");
            //Console.WriteLine($"sequences.Add(\"S1\", new List<double>(new double[] {{ {string.Join(", ", doubleList)} }}));");



           // sequences.Add("S1", new List<double>(new double[] { 1.0, 2.0, 3.0, 4.0 }));
           // sequences.Add("S2", new List<double>(new double[] { 5.0, 6.0, 7.0, 8.0 }));

            //sequences.Add("S1", new List<double>(new double[] { 0.0, 1.0, 0.0, 2.0, 3.0, 4.0, 5.0, 6.0, 5.0, 4.0, 3.0, 7.0, 1.0, 9.0, 12.0, 11.0, 12.0, 13.0, 14.0, 11.0, 12.0, 14.0, 5.0, 7.0, 6.0, 9.0, 3.0, 4.0, 3.0, 4.0, 3.0, 4.0 }));
            //sequences.Add("S2", new List<double>(new double[] { 0.8, 2.0, 0.0, 3.0, 3.0, 4.0, 5.0, 6.0, 5.0, 7.0, 2.0, 7.0, 1.0, 9.0, 11.0, 11.0, 10.0, 13.0, 14.0, 11.0, 7.0, 6.0, 5.0, 7.0, 6.0, 5.0, 3.0, 2.0, 3.0, 4.0, 3.0, 4.0 }));

            //sequences.Add("S1", new List<double>(new double[] { 1045.0, 2572.0, 1037.0, 2388.0, 2021.0, 1045.0, 2572.0, 2036.0, 1037.0, 2684.0, 1998.0, 1037.0, 2905.0 }));
            //sequences.Add("S2", new List<double>(new double[] { 2036.0, 1037.0, 2684.0, 1998.0, 1037.0, 2905.0 }));


            //sequences.Add("S1", new List<double>(new double[] { 0.0, 1.0, 0.0, 2.0, 3.0, 4.0, 5.0, 6.0, 5.0, 2.0, 3.0, 7.0, 1.0, 9.0, 12.0, 11.0, 12.0, 13.0, 14.0, 11.0, 12.0 }));
            //sequences.Add("S2", new List<double>(new double[] { 0.8, 2.0, 0.0, 3.0, 3.0, 4.0, 5.0, 6.0, 5.0, 7.0, 2.0, 7.0, 1.0, 9.0, 11.0, 11.0, 10.0, 13.0, 14.0, 11.0, 7.0 }));

            // sequences.Add("S1", new List<double>(new double[] { 0.0, 11.0, 12.0, 13.0, 14.0, 15.0, 5.0, 16.0, 17.0, 1.0, 2.0, 3.0, 6.0 }));
            // sequences.Add("S2", new List<double>(new double[] { 8.0, 12.0, 1.0, 2.0, 9.0, 10.0, 17.0, 11.00, 3.0, 13.0, 5.0, 6.0, 18.0 }));

            //            string poem = @"First Citizen:
            //Before we proceed any further, hear me speak.
            //All:
            //Speak, speak.";

            //            List<double> poemSeq = new List<double>();
            //            foreach (var chr in poem)
            //            {
            //                poemSeq.Add((double)chr);
            //            }

            //            sequences.Add("Poem", poemSeq);

            //
            // Prototype for building the prediction engine.
            //MultiSequenceLearning experiment = new MultiSequenceLearning();
            //var predictor = experiment.Run(sequences);
            SemanticSimilarityLearning experiment = new SemanticSimilarityLearning();
            experiment.Run(sequences);

            Console.WriteLine("Enter first text sequence: ");
            string input1 = Console.ReadLine();

            Console.WriteLine("Enter second text sequence: ");
            string input2 = Console.ReadLine();

            CompareTextSequences(input1, input2, 1024,25);


            //
            // These list are used to see how the prediction works.
            // Predictor is traversing the list element by element. 
            // By providing more elements to the prediction, the predictor delivers more precise result


            //var list1 = new double[] { 1.0, 2.0, 3.0, 4.0, 2.0, 5.0 };
            //var list2 = new double[] { 2.0, 3.0, 4.0 };
            //var list3 = new double[] { 8.0, 1.0, 2.0 };

            //var list1 = new double[] { 4.0, 5.0, 6.0, 5.0, 2.0, 3.0, 7.0, 1.0, 9.0, 12.0 };
            //var list2 = new double[] { 2.0, 3.0, 7.0, 1.0, 9.0, 12.0 };
            //var list3 = new double[] { 3.0, 7.0, 1.0 };

            // var list1 = new double[] { 1.0, 12.0, 13.0, 14.0, 15.0, 16.0, 17.0, 1.0, 2.0, 3.0 };
            // var list2 = new double[] { 12.0, 13.0, 17.0, 1.0, 9.0, 2.0 };
            // var list3 = new double[] { 13.0, 17.0, 11.0 };

            //predictor.Reset();
            //PredictNextElement(predictor, list1);

            //predictor.Reset();
            //PredictNextElement(predictor, list2);

            //predictor.Reset();
            //PredictNextElement(predictor, list3);
        }

        private static void CompareTextSequences(string text1, string text2, int numColumns, int numCellsPerColumn)
        {
            SDRStorage sdrStorage = new SDRStorage();
            List_Generation listGeneration = new List_Generation();

            // Tokenize both text sequences
            List<int> tokens1 = listGeneration.TokenizeText(text1);
            List<int> tokens2 = listGeneration.TokenizeText(text2);

            // Lists to store binary SDRs for each subsequence
            List<int> binarySDR1 = new List<int>();
            List<int> binarySDR2 = new List<int>();

            // Retrieve stored SDRs for each token and convert to binary SDRs
            foreach (var token in tokens1)
            {
                var storedSDR = sdrStorage.LoadSDR(token.ToString());
                if (storedSDR.HasValue)
                {
                    // Convert column SDR to binary
                    int[] columnBinary = ConvertToBinarySDR(storedSDR.Value.columnSDR, numColumns);
                    binarySDR1.AddRange(columnBinary);

                    // Convert cell SDR to binary
                    int[] cellBinary = ConvertToBinarySDR(storedSDR.Value.cellSDR, numColumns * numCellsPerColumn);
                    binarySDR1.AddRange(cellBinary);
                }
            }

            foreach (var token in tokens2)
            {
                var storedSDR = sdrStorage.LoadSDR(token.ToString());
                if (storedSDR.HasValue)
                {
                    // Convert column SDR to binary
                    int[] columnBinary = ConvertToBinarySDR(storedSDR.Value.columnSDR, numColumns);
                    binarySDR2.AddRange(columnBinary);

                    // Convert cell SDR to binary
                    int[] cellBinary = ConvertToBinarySDR(storedSDR.Value.cellSDR, numColumns * numCellsPerColumn);
                    binarySDR2.AddRange(cellBinary);
                }
            }

            // Calculate cosine similarity
            if (binarySDR1.Count > 0 && binarySDR2.Count > 0)
            {
                double similarity = CosineSimilarity(binarySDR1.ToArray(), binarySDR2.ToArray());
                Console.WriteLine($"Cosine Similarity between '{text1}' and '{text2}': {similarity}");
            }
            else
            {
                Console.WriteLine("No matching SDRs found for comparison.");
            }
        }

        /// <summary>
        /// Converts a list of active indices into a binary SDR vector.
        /// </summary>
        /// <param name="activeIndices">List of active indices (e.g., [10, 12, 25]).</param>
        /// <param name="totalSize">Total size of the binary vector (e.g., number of columns or cells).</param>
        /// <returns>A binary vector with 1s at active indices and 0s elsewhere.</returns>
        private static int[] ConvertToBinarySDR(int[] activeIndices, int totalSize)
        {
            int[] binarySDR = new int[totalSize];
            foreach (int index in activeIndices)
            {
                if (index < totalSize)
                {
                    binarySDR[index] = 1;
                }
            }
            return binarySDR;
        }

        /// <summary>
        /// Calculates the cosine similarity between two binary SDR vectors.
        /// </summary>
        /// <param name="vector1">First binary SDR vector.</param>
        /// <param name="vector2">Second binary SDR vector.</param>
        /// <returns>The cosine similarity between the two vectors (ranges from -1 to 1).</returns>
        public static double CosineSimilarity(int[] vector1, int[] vector2)
        {
            double dotProduct = vector1.Zip(vector2, (v1, v2) => v1 * v2).Sum();
            double magnitude1 = Math.Sqrt(vector1.Sum(v => v * v));
            double magnitude2 = Math.Sqrt(vector2.Sum(v => v * v));
            return (magnitude1 == 0 || magnitude2 == 0) ? 0.0 : dotProduct / (magnitude1 * magnitude2);
        }


        private static void PredictNextElement(Predictor predictor, double[] list)
        {
            Debug.WriteLine("------------------------------");

            foreach (var item in list)
            {
                var res = predictor.Predict(item);

                if (res.Count > 0)
                {
                    foreach (var pred in res)
                    {
                        Debug.WriteLine($"{pred.PredictedInput} - {pred.Similarity}");
                    }

                    var tokens = res.First().PredictedInput.Split('_');
                    var tokens2 = res.First().PredictedInput.Split('-');
                    Debug.WriteLine($"Predicted Sequence: {tokens[0]}, predicted next element {tokens2.Last()}");
                }
                else
                    Debug.WriteLine("Nothing predicted :(");
            }

            Debug.WriteLine("------------------------------");
        }
    }
}

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

            // Print to verify
            Console.WriteLine("Generated Sequence:");
            Console.WriteLine($"sequences.Add(\"S1\", new List<double>(new double[] {{ {string.Join(", ", doubleList)} }}));");

            

            //sequences.Add("S1", new List<double>(new double[] { 1.0, 2.0, 3.0, 4.0 }));
            //sequences.Add("S2", new List<double>(new double[] { 5.0, 6.0, 7.0, 8.0 }));

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
            MultiSequenceLearning experiment = new MultiSequenceLearning();
            var predictor = experiment.Run(sequences);

            //
            // These list are used to see how the prediction works.
            // Predictor is traversing the list element by element. 
            // By providing more elements to the prediction, the predictor delivers more precise result

            //Prompt for taking user input as a text sequence
            Console.WriteLine("Enter some text: ");
            string input = Console.ReadLine(); // Take runtime input from the user

            // Pass the input to a method for processing
            List<int> processedText = list_Generation.TokenizeText(input);

            // Convert the processed list to an array of doubles
            var list1 = processedText.Select(x => (double)x).ToArray();

            // Print the array 
            Console.WriteLine($"var list1 = new double[] {{ {string.Join(", ", list1)} }};");

            //var list1 = new double[] { 1.0, 2.0, 3.0, 4.0, 2.0, 5.0 };
            //var list2 = new double[] { 2.0, 3.0, 4.0 };
            //var list3 = new double[] { 8.0, 1.0, 2.0 };

            //var list1 = new double[] { 4.0, 5.0, 6.0, 5.0, 2.0, 3.0, 7.0, 1.0, 9.0, 12.0 };
            //var list2 = new double[] { 2.0, 3.0, 7.0, 1.0, 9.0, 12.0 };
            //var list3 = new double[] { 3.0, 7.0, 1.0 };

            // var list1 = new double[] { 1.0, 12.0, 13.0, 14.0, 15.0, 16.0, 17.0, 1.0, 2.0, 3.0 };
            // var list2 = new double[] { 12.0, 13.0, 17.0, 1.0, 9.0, 2.0 };
            // var list3 = new double[] { 13.0, 17.0, 11.0 };

            predictor.Reset();
            PredictNextElement(predictor, list1);

            //predictor.Reset();
            //PredictNextElement(predictor, list2);

            //predictor.Reset();
            //PredictNextElement(predictor, list3);
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

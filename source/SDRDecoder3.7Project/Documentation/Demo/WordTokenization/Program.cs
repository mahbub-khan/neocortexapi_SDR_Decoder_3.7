namespace WordTokenization;
using NeoCortexApi;
using NeoCortexApi.Entities;
using NeoCortexApi.Network;
using NeoCortexApi.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


class Program
{
    static void Main()
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
        Console.WriteLine("\nGenerated Sequence:");
        Console.WriteLine($"sequences.Add(\"S1\", new List<double>(new double[] {{ {string.Join(", ", doubleList)} }}));");


        //visualizing SDRs
        Scaler_Encoder encoder = new Scaler_Encoder();
        encoder.ScalarEncoderTest(sequences);

        // Initialize SDR handler (loads SDRs from file)
        SDRRetrieval sdrretrieval = new("sdr_output.txt");

        
                // Prompt the user for input
                Console.WriteLine("\nEnter a sub-sequence of the source text: ");
                string userInput1 = Console.ReadLine(); // Take runtime input from the user

                // Pass the input to a method for processing
                List<int> processedText1 = list_Generation.TokenizeText(userInput1);

                // Convert to a list of doubles
                var list1 = processedText1.Select(i => (double)i).ToList();





                //// Retrieve SDRs for the given token IDs
                //Dictionary<double, string> retrievedSDRs = sdrretrieval.GetSDRs(list1);
                ////Display retrieved SDRs
                //foreach (var kvp in retrievedSDRs)
                //{
                //    Console.WriteLine($"Token ID: {kvp.Key} -> SDR: {kvp.Value}");
                //}

                // Prompt the user for another input
                Console.WriteLine("\nEnter text for comaprison: ");
                string userInput2 = Console.ReadLine(); // Take runtime input from the user

                // Pass the input to a method for processing
                List<int> processedText2 = list_Generation.TokenizeText(userInput2);

                // Convert to a list of doubles
                var list2 = processedText2.Select(i => (double)i).ToList();

                ////Retrieve SDRs for the given token IDs
                //Dictionary<double, int[]> retrievedSDRs2 = sdrretrieval.GetSDRs(list2);
                //// Display retrieved SDRs
                //foreach (var kvp in retrievedSDRs2)
                //{
                //    Console.WriteLine($"Token ID: {kvp.Key} -> SDR: [{string.Join(", ", kvp.Value)}]");
                //}

                //Retrieve SDRs from the stored data
                Dictionary<double, int[]> retrievedSDRs1 = sdrretrieval.GetSDRsAsVectors(list1);
              
                Dictionary<double, int[]> retrievedSDRs2 = sdrretrieval.GetSDRsAsVectors(list2);

        // Merge all SDRs into single binary vectors
        int[] sdrVector1 = retrievedSDRs1.Values.SelectMany(sdr => sdr).ToArray();
        int[] sdrVector2 = retrievedSDRs2.Values.SelectMany(sdr => sdr).ToArray();

        // Debug SDRs before calculating similarity
        DebugSDRs(sdrVector1, sdrVector2);

                // Compute Cosine Similarity between two SDR vectors
                double Cos_similarity = SDRProcessor.CosineSimilarity(sdrVector1, sdrVector2)*100;
                double Euclid_similarity = SDRProcessor.EuclideanSimilarity(sdrVector1, sdrVector2)*100;

                // Display the result
                Console.WriteLine($"\nSimilarity using Cosine Similarity function: {Cos_similarity:F2}% \nSimilarity using Euclidean Distance function: {Euclid_similarity:F2}%");

    }


    public static void DebugSDRs(int[] mergedSDR1, int[] mergedSDR2)
    {
        //Console.WriteLine("\nSparse Merged SDR of subsequenceText : " + string.Join(", ", mergedSDR1));
        //Console.WriteLine("Sparse Merged SDR of comparisonText: " + string.Join(", ", mergedSDR2));
        //Console.WriteLine("Common SDRs: " + string.Join(", ", mergedSDR1.Intersect(mergedSDR2)));
        //Console.WriteLine("Total Active Bits in subsequenceText SDR: " + mergedSDR1.Length);
        //Console.WriteLine("Total Active Bits in comparisonText: " + mergedSDR2.Length);
        //Console.WriteLine("Common Active Bits: " + mergedSDR1.Intersect(mergedSDR2).Count());

    }
}




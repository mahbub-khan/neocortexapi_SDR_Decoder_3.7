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
        ListGeneration listGeneration = new ListGeneration();

        // tokenId generation
        List<int> tokenIds = listGeneration.TokenizeText(text);

        // Convert to a list of doubles
        List<double> doubleList = tokenIds.Select(i => (double)i).ToList();

        // Store in a dictionary with a key like "S1"
        Dictionary<string, List<double>> sequences = new Dictionary<string, List<double>>();
        sequences.Add("S1", new List<double>(doubleList));

        // Print to verify
        Console.WriteLine("\nGenerated Sequence:");
        Console.WriteLine($"sequences.Add(\"S1\", new List<double>(new double[] {{ {string.Join(", ", doubleList)} }}));");


        //visualizing SDRs
        ScalerEncoder encoder = new ScalerEncoder();
        encoder.ScalarEncoderTest(sequences);

        // Initialize SDR handler (loads SDRs from file)
        SDRRetrieval sdrretrieval = new("sdr_output.txt");


        // Prompt the user for input
        Console.WriteLine("\nEnter a sub-sequence of the source text: ");
        string userInput1 = Console.ReadLine(); // Take runtime input from the user

        // Pass the input to a method for processing
        List<int> processedText1 = listGeneration.TokenizeText(userInput1);

        // Convert to a list of doubles
        var list1 = processedText1.Select(i => (double)i).ToList();


        // Prompt the user for another input
        Console.WriteLine("\nEnter text for comaprison: ");
        string userInput2 = Console.ReadLine(); // Take runtime input from the user

        // Pass the input to a method for processing
        List<int> processedText2 = listGeneration.TokenizeText(userInput2);

        // Convert to a list of doubles
        var list2 = processedText2.Select(i => (double)i).ToList();

        //Retrieve SDRs from the stored data
        Dictionary<double, int[]> retrievedSDRs1 = sdrretrieval.GetSDRsAsVectors(list1);
              
        Dictionary<double, int[]> retrievedSDRs2 = sdrretrieval.GetSDRsAsVectors(list2);

        // Merge all SDRs into single binary vectors
        int[] sdrVector1 = retrievedSDRs1.Values.SelectMany(sdr => sdr).ToArray();
        int[] sdrVector2 = retrievedSDRs2.Values.SelectMany(sdr => sdr).ToArray();

        // Compute Cosine Similarity between two SDR vectors
        double cosineSimilarity = SDRProcessor.CosineSimilarity(sdrVector1, sdrVector2)*100;
        double euclidSimilarity = SDRProcessor.EuclideanSimilarity(sdrVector1, sdrVector2)*100;

         // Display the result
         Console.WriteLine($"\nSimilarity using Cosine Similarity function: {cosineSimilarity:F2}% \nSimilarity using Euclidean Distance function: {euclidSimilarity:F2}%");

    }

}




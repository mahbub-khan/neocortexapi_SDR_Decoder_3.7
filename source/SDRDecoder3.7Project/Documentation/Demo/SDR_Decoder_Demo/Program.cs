//using System.Reflection.Metadata.Ecma335;
using System;
using System.Collections.Generic;
using System.IO;
using NeoCortexApi;
using NeoCortexApi.Entities;
using NeoCortexApi.Network;
using NeoCortexApi.Utility;

namespace SDR_Decoder
{

    class Program
    {
        static void Main(string[] args)
        {
            string inputFilePath = "sequence.txt"; // Input file path
            if (!File.Exists(inputFilePath))
            {
                Console.WriteLine($"File not found: {inputFilePath}");
                return;
            }

            string[] sequence = File.ReadAllText(inputFilePath).Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, int> tokenMap = new Dictionary<string, int>();
            int tokenId = 0;

            foreach (var word in sequence)
            {
                if (!tokenMap.ContainsKey(word))
                    tokenMap[word] = tokenId++;
            }

            List<SparseDistributedRepresentation> encodedSequence = new List<SparseDistributedRepresentation>();
            foreach (var word in sequence)
            {
                int token = tokenMap[word];
                int[] indices = new int[] { token }; // Simplified SDR for demonstration
                encodedSequence.Add(new SparseDistributedRepresentation(indices));
            }

            Console.WriteLine("Sequence encoded into SDRs.\nEnter test subsequences (comma-separated):");
            string[] testSubsequences = Console.ReadLine()?.Split(',');

            if (testSubsequences == null || testSubsequences.Length == 0)
            {
                Console.WriteLine("No test subsequences provided.");
                return;
            }

            List<SparseDistributedRepresentation> testSDRs = new List<SparseDistributedRepresentation>();
            foreach (var subsequence in testSubsequences)
            {
                foreach (var word in subsequence.Trim().Split(' '))
                {
                    if (tokenMap.ContainsKey(word))
                    {
                        int[] indices = new int[] { tokenMap[word] };
                        testSDRs.Add(new SparseDistributedRepresentation(indices));
                    }
                }
            }

            for (int i = 0; i < testSDRs.Count; i++)
            {
                for (int j = i + 1; j < testSDRs.Count; j++)
                {
                    double similarity = SDRMetric.Compare(testSDRs[i], testSDRs[j]);
                    Console.WriteLine($"Similarity between SDR {i + 1} and SDR {j + 1}: {similarity:F2}");
                }
            }
        }
    }
}



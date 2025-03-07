using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WordTokenization
{
    class SDRRetrieval
    {
        private Dictionary<double, List<int>> sdrDictionary;  // Store Token ID -> SDR (as list of indices)

        public SDRRetrieval(string filePath)
        {
            sdrDictionary = LoadSDRsFromFile(filePath);
        }

        private Dictionary<double, List<int>> LoadSDRsFromFile(string filePath)
        {
            Dictionary<double, List<int>> sdrDict = new Dictionary<double, List<int>>();

            if (!File.Exists(filePath))
            {
                Console.WriteLine("SDR file not found!");
                return sdrDict;
            }

            string[] lines = File.ReadAllLines(filePath);
            double tokenId = -1;
            List<int> sdrIndices = null;

            foreach (string line in lines)
            {
                if (line.StartsWith("Input = "))
                {
                    tokenId = double.Parse(line.Replace("Input = ", "").Trim());
                }
                else if (line.StartsWith("SDR As Text = "))
                {
                    sdrIndices = line.Replace("SDR As Text = ", "")
                                     .Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                     .Select(int.Parse)
                                     .ToList();

                    if (tokenId != -1 && sdrIndices != null)
                    {
                        sdrDict[tokenId] = sdrIndices;
                    }
                }
            }
            return sdrDict;
        }

        public Dictionary<double, List<int>> GetSDRs(List<double> tokenIds)
        {
            Dictionary<double, List<int>> resultSDRs = new Dictionary<double, List<int>>();

            foreach (var tokenId in tokenIds)
            {
                if (sdrDictionary.TryGetValue(tokenId, out List<int> sdr))
                {
                    resultSDRs[tokenId] = sdr;
                }
                else
                {
                    Console.WriteLine($"Warning: SDR not found for Token ID {tokenId}");
                }
            }
            return resultSDRs;
        }

        public static double ComputeCosineSimilarity(List<int> sdr1, List<int> sdr2)
        {
            HashSet<int> set1 = new HashSet<int>(sdr1);
            HashSet<int> set2 = new HashSet<int>(sdr2);

            int intersection = set1.Intersect(set2).Count();
            double magnitude1 = Math.Sqrt(set1.Count);
            double magnitude2 = Math.Sqrt(set2.Count);

            if (magnitude1 == 0 || magnitude2 == 0) return 0.0;
            return intersection / (magnitude1 * magnitude2);
        }

        public static void DebugSDRs(List<int> mergedSDR1, List<int> mergedSDR2)
        {
            Console.WriteLine("\n Merged SDR 1: " + string.Join(", ", mergedSDR1));
            Console.WriteLine("\nMerged SDR 2: " + string.Join(", ", mergedSDR2));

            Console.WriteLine($"\n Active bits count in SDR 1: {mergedSDR1.Count}");
            Console.WriteLine($" Active bits count in SDR 2: {mergedSDR2.Count}");

            var commonBits = mergedSDR1.Intersect(mergedSDR2).ToList();
            Console.WriteLine($" Common active bits count: {commonBits.Count}");
            Console.WriteLine(" Common active bits: " + string.Join(", ", commonBits));
        }
    }
}

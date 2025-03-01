using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordTokenization
{
    class SDRRetrieval
    {
        private Dictionary<double, string> sdrDictionary;  // Dictionary to store Token ID -> SDR mappings

        // Constructor: Load SDRs from the file when an object is created
        public SDRRetrieval(string filePath)
        {
            sdrDictionary = LoadSDRsFromFile(filePath);
        }

        // Method to load SDR values from file into Dictionary<TokenID, SDR>
        private Dictionary<double, string> LoadSDRsFromFile(string filePath)
        {
            Dictionary<double, string> sdrDict = new Dictionary<double, string>(); // Dictionary to store SDRs

            // Check if the file exists
            if (!File.Exists(filePath))
            {
                Console.WriteLine("SDR file not found!");  // Print warning if the file is missing
                return sdrDict;  // Return an empty dictionary
            }

            string[] lines = File.ReadAllLines(filePath);  // Read all lines from the SDR file
            int tokenId = -1;  // Variable to store Token ID
            string sdr = null;  // Variable to store SDR values

            // Process each line in the file
            foreach (string line in lines)
            {
                if (line.StartsWith("Input = "))  // Check if the line contains a Token ID
                {
                    tokenId = int.Parse(line.Replace("Input = ", "").Trim());  // Extract Token ID
                }
                else if (line.StartsWith("SDRs Generated = "))  // Check if the line contains SDR values
                {
                    sdr = line.Replace("SDRs Generated = ", "").Trim();  // Extract SDR string

                    // Store Token ID and corresponding SDR in the dictionary
                    if (tokenId != -1 && sdr != null)
                    {
                        sdrDict[tokenId] = sdr;
                    }
                }
            }
            return sdrDict;  // Return the dictionary containing SDR mappings
        }

        //  Method to retrieve SDRs for given token IDs
        public Dictionary<double, string> GetSDRs(List<double> tokenIds)
        {
            Dictionary<double, string> resultSDRs = new Dictionary<double, string>();  // Dictionary to store retrieved SDRs

            // Loop through the given token IDs
            foreach (var tokenId in tokenIds)
            {
                // Check if the SDR exists for this Token ID
                if (sdrDictionary.TryGetValue(tokenId, out string sdr))
                {
                    resultSDRs[tokenId] = sdr;  // Store the SDR for this Token ID
                }
                else
                {
                    Console.WriteLine($"Warning: SDR not found for Token ID {tokenId}");  // Print warning if SDR is missing
                }
            }
            return resultSDRs;  // Return dictionary containing retrieved SDRs
        }
    }
}


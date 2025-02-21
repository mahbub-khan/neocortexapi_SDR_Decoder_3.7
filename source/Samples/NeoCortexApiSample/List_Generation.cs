using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoCortexApiSample
{
    internal class List_Generation
    {
        List<int> result = new List<int>();
        public List<int> TokenizeText(string inputText)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "python",
                Arguments = $"tokenizer.py \"{inputText}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process process = new Process { StartInfo = psi };
            process.Start();

            string tokens = process.StandardOutput.ReadLine();
            string tokenIds = process.StandardOutput.ReadLine();

            string output = process.StandardOutput.ReadToEnd().Trim();
            process.WaitForExit();


            // Display results
            Console.WriteLine("Input Text: " + inputText);
            Console.WriteLine("Tokens: " + tokens);
            Console.WriteLine("Token IDs: " + tokenIds);
            //Console.WriteLine("Output:" +output);

            // Convert tokenized string output into List<int>

            if (string.IsNullOrWhiteSpace(tokenIds))
            {
                Console.WriteLine("Warning: Tokenizer returned empty output.");
                return new List<int>();  // Return empty list instead of causing an error
            }

            // Ensure all parts are valid integers
            try
            {
                return tokenIds.Split(' ')
                             .Where(token => !string.IsNullOrWhiteSpace(token)) // Ignore empty tokens
                             .Select(int.Parse)
                             .ToList();
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Error: Tokenizer output contains invalid values. {ex.Message}");
                return new List<int>();  // Return an empty list to prevent crashes
            }
        }
    }
}


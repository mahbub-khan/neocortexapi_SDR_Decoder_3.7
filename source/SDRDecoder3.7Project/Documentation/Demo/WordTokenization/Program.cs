namespace WordTokenization;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using NeoCortexApi;
using NeoCortexApi.Encoders;


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
        List<int> tokenIds = TokenizeText(text);

        Console.WriteLine(string.Join(" ",tokenIds));

        // ScalarEncoder initialization
        ScalarEncoder encoder = new ScalarEncoder(0, 30522, 21, false);  // minVal, maxVal, w

        Console.WriteLine("Encoding Token IDs into SDRs...");
        foreach (int tokenId in tokenIds)
        {
            //Encode Token ID into SDR
            int[] encodedArray = encoder.Encode(tokenId);

            //Convert int[] to BitArray
            BitArray sdr = new BitArray(encodedArray.Select(x => x == 1).ToArray());

            Console.WriteLine($"Token ID: {tokenId} -> SDR: {BitArrayToBinaryString(sdr)}");
        }


        static List<int> TokenizeText(string inputText)
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

        static string BitArrayToBinaryString(BitArray bitArray)
        {
            return string.Join("", bitArray.Cast<bool>().Select(bit => bit ? "1" : "0"));
        }
    }
}


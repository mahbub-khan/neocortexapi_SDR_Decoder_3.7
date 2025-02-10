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
        List<int> tokenIds = TokenizeText(textInput);

        // ScalarEncoder initialization
        ScalarEncoder encoder = new ScalarEncoder(0, 30522, 21, false);  // minVal, maxVal, w

        Console.WriteLine("Encoding Token IDs into SDRs...");
        foreach (int tokenId in tokenIds)
        {
            // Encode Token ID into SDR
            int[] encodedArray = encoder.Encode(tokenId);

            // Convert int[] to BitArray
            BitArray sdr = new BitArray(encodedArray.Select(x => x == 1).ToArray());

            Console.WriteLine($"Token ID: {tokenId} -> SDR: {BitArrayToBinaryString(sdr)}");
        }
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
       // Console.WriteLine("Input Text: " + inputText);
       // Console.WriteLine("Tokens: " + tokens);
        //Console.WriteLine("Token IDs: " + tokenIds);

        // Convert tokenized string output into List<int>
        return output.Split(' ').Select(int.Parse).ToList();
    }

    static string BitArrayToBinaryString(BitArray bitArray)
    {
        return string.Join("", bitArray.Cast<bool>().Select(bit => bit ? "1" : "0"));
    }
}



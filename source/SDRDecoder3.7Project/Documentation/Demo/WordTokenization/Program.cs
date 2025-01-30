namespace WordTokenization;
using System;
using System.Diagnostics;
using System.IO;

class Program
{
    static void Main()
    {
        // Read input text from a file
        string filePath = "input.txt"; // Ensure this file exists with some text
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Error: input.txt not found!");
            return;
        }

        string inputText = File.ReadAllText(filePath).Trim();

        // Call the Python tokenizer script
        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = "python3", // Make sure Python is in system PATH
            Arguments = $"tokenizer.py \"{inputText}\"",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        Process process = new Process { StartInfo = psi };
        process.Start();

        // Read Python script output
        string tokens = process.StandardOutput.ReadLine();
        string tokenIds = process.StandardOutput.ReadLine();

        process.WaitForExit();

        // Display results
        Console.WriteLine("Input Text: " + inputText);
        Console.WriteLine("Tokens: " + tokens);
        Console.WriteLine("Token IDs: " + tokenIds);
    }
}

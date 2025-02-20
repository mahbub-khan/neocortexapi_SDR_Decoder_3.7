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

        List_Generation list_Generation = new List_Generation();
        List<int> tokenIds = list_Generation.TokenizeText(text);

        // Convert to a list of doubles
        List<double> doubleList = tokenIds.Select(i => (double)i).ToList();


        Dictionary<string, List<double>> sequences = new Dictionary<string, List<double>>();

        Console.WriteLine(string.Join(" ",doubleList));
        sequences.Add("S1", doubleList);

        // Print the values
        Console.WriteLine("Key: S1, Values: " + string.Join(", ", sequences["S1"]));

        // Prompt the user for input
        Console.WriteLine("Enter some text: ");
        string userInput = Console.ReadLine(); // Take runtime input from the user

        // Pass the input to a method for processing
        List<int> processedText = list_Generation.TokenizeText(userInput);

        // Convert to a list of doubles
        var list1 = processedText.Select(i => (double)i).ToList();

        
    }
}


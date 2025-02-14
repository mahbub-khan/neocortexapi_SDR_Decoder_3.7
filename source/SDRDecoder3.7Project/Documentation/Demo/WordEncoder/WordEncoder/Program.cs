using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.Write("Enter a sentence: ");
        string input = Console.ReadLine();
        StringBuilder encodedString = new StringBuilder();

        // Ignoring spaces, commas, and full stops for encoding
        string[] words = input.Split(new char[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries);
        List<string> uniqueWords = words.Select(w => w.ToLower()).Distinct().ToList();
        Console.WriteLine("Number of unique words: " + uniqueWords.Count);

        int uniqueWordCount = 0;
        Dictionary<string, float> wordEncoding = new Dictionary<string, float>();

        foreach (string uniqueWord in uniqueWords)
        {
            uniqueWordCount++;
            float encodedValue = uniqueWordCount * 1.0f; // Assigning unique encoding as 1.0, 2.0, etc.
            wordEncoding[uniqueWord] = encodedValue;
            encodedString.Append(encodedValue.ToString("F1") + " ");
            Console.WriteLine($" #{uniqueWordCount}: {uniqueWord} -> Encoded Value: {encodedValue:F1}");
        }

        Console.WriteLine("Encoded Sentence: " + encodedString.ToString().Trim());
    }
}

using System;
using System.Text;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.Write("Enter a sentence: ");
        string input = Console.ReadLine();
        StringBuilder encodedString = new StringBuilder();

        //ignoring spaces, commas and full-stops for encoding
        string[] words = input.Split(new char[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries);
        Console.WriteLine("Number of words: " + words.Length);

        int maxAsciiSum = 0;
        string maxWord = string.Empty; // To store the word with the max ASCII sum

        foreach (string word in words)
        {
            int asciiSum = word.Sum(c => (int)c);
            encodedString.Append(asciiSum.ToString() + " ");
            Console.WriteLine($"Word: {word}\t Length: {word.Length}\t ASCII Sum: {asciiSum}");

            // Check if the current ASCII sum is greater than the max found so far
            if (asciiSum > maxAsciiSum)
            {
                maxAsciiSum = asciiSum;
                maxWord = word; // Store the corresponding word
            }

        }

        Console.WriteLine("Encoded Sentence: " + encodedString.ToString().Trim()); 
        Console.WriteLine($"Maximum ASCII Sum: {maxAsciiSum} for (Word: {maxWord})");
    }
}
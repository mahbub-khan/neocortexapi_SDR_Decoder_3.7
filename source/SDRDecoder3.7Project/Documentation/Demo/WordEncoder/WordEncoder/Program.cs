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

<<<<<<< HEAD
        float maxAsciiSum = 0;
=======
        int maxAsciiSum = 0;
>>>>>>> 5479b893e1c1c4133a338c9cce8d3543f54032ff
        string maxWord = string.Empty; // To store the word with the max ASCII sum

        foreach (string word in words)
        {
            int asciiSum = word.Sum(c => (int)c);
<<<<<<< HEAD
            float reducedAsciiSum = asciiSum / 10f;
            encodedString.Append(reducedAsciiSum.ToString() + " ");
            Console.WriteLine($"Word: {word}\t Length: {word.Length}\t Encoded Value(ASCII Sum): {reducedAsciiSum}");

            // Check if the current ASCII sum is greater than the max found so far
            if (reducedAsciiSum > maxAsciiSum)
            {
                maxAsciiSum = reducedAsciiSum;
=======
            encodedString.Append(asciiSum.ToString() + " ");
            Console.WriteLine($"Word: {word}\t Length: {word.Length}\t ASCII Sum: {asciiSum}");

            // Check if the current ASCII sum is greater than the max found so far
            if (asciiSum > maxAsciiSum)
            {
                maxAsciiSum = asciiSum;
>>>>>>> 5479b893e1c1c4133a338c9cce8d3543f54032ff
                maxWord = word; // Store the corresponding word
            }

        }

        Console.WriteLine("Encoded Sentence: " + encodedString.ToString().Trim()); 
        Console.WriteLine($"Maximum ASCII Sum: {maxAsciiSum} for (Word: {maxWord})");
    }
}
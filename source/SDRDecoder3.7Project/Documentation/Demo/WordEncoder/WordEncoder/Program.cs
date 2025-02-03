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

        foreach (string word in words)
        {
            int asciiSum = word.Sum(c => (int)c);
            encodedString.Append(asciiSum.ToString() + " ");
            Console.WriteLine($"Word: {word}\t Length: {word.Length}\t ASCII Sum: {asciiSum}");
        }

        Console.WriteLine("Encoded Sentence: " + encodedString.ToString().Trim());
    }
}

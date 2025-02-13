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
        string[] words = input.Split(new char[] { ' ', ',', '.', '?' }, StringSplitOptions.RemoveEmptyEntries);
        Console.WriteLine("Number of total words: " + words.Length);

        //finding out unique words
        List<string> uniqueWords = words.Select(w => w.ToLower()).Distinct().ToList();
        Console.WriteLine("Number of unique words: " + uniqueWords.Count);

        int uniqueWordCount = 0;


        foreach (string uniqueWord in uniqueWords)
        {
           uniqueWordCount++;
           Console.WriteLine($" #{uniqueWordCount}: {uniqueWord}");
        }

        //New Encoding technique will be implemented here

        
    }
}

/*
Current Ouput:
Enter a sentence: You? you. YOU
Number of total words: 3
Number of unique words: 1
 #1: you
*/

/*
Previous Output:
Enter a sentence: You, you, YOU.
Number of total words: 3
Number of unique words: 3
 #1: You
 #2: you
 #3: YOU
*/
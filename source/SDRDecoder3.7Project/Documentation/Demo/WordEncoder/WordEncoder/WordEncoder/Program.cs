using System.Text;

namespace WordEncoder
{
    internal class Program
    {
        static void Main()
        {
            Console.Write("Enter a sentence: ");
            string input = Console.ReadLine();
            Console.WriteLine("Number of characters: " + input.Length);

            StringBuilder encodedString = new StringBuilder();

            foreach (char c in input)
            {
                encodedString.Append(((float)c).ToString("F1") + ",");
            }

            Console.WriteLine("Encoded String: " + encodedString.ToString().Trim());
            Console.WriteLine("Number of outputs: " + input.Length);
        }
    }
}

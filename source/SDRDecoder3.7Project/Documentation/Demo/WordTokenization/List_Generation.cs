using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordTokenization
{
    internal class List_Generation
    {
        static List<double> GenerateList()
        {
            List<double> result = new List<double>();
            string[] lines = System.IO.File.ReadAllLines("output.txt"); // Read values from a file
            foreach (string line in lines)
            {
                result.Add(double.Parse(line)); // Convert each line to a double
            }
            return result;
        }
    }
}

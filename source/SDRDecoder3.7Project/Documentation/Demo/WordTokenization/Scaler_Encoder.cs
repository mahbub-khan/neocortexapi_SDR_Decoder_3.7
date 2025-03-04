using NeoCortexApi.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeoCortexApi.Encoders;

using NeoCortexApi.Entities;   // Common utilities
//using NeoCortex.Utils;  // For mathematical functions

//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeoCortex;

namespace WordTokenization
{
    public class Scaler_Encoder
    {
        public void ScalarEncoderTest(Dictionary<string, List<double>> sequences)
        {
            var outFolder = @"ScalarEncoderResults";

            //ScalarEncoder encoder = new ScalarEncoder(new Dictionary<string, object>()
            //{
            //    { "W", 3},       // 2% Approx 
            //    { "N", 100},
            //    { "MinVal", (double)0},
            //    { "MaxVal", (double)99},
            //    { "Periodic", true},
            //    { "Name", "Scalar Sequence"},
            //    { "ClipInput", true},
            //});

            int inputBits = 100;
            int numColumns = 1024;

            double max = 14000;

            Dictionary<string, object> settings = new Dictionary<string, object>()
            {
                { "W", 15},
                { "N", inputBits},
                { "Radius", -1.0},
                { "MinVal", 0.0},
                { "Periodic", false},
                { "Name", "scalar"},
                { "ClipInput", false},
                { "MaxVal", max}
            };
            EncoderBase encoder = new ScalarEncoder(settings);
            //ScalarEncoder encoder = new ScalarEncoder(GetDefautEncoderSettings());
            Dictionary<double, int[]> sdrs = new Dictionary<double, int[]>();

            List<double> sequenceValue = sequences["S1"];
           // Console.WriteLine("Values from S1: " + string.Join(", ", sequenceValue));

            foreach (double input in sequenceValue)
            {
                int[] result = encoder.Encode(input);

                Console.WriteLine($"Input = {input}");
                Console.WriteLine($"SDRs Generated = {NeoCortexApi.Helpers.StringifyVector(result)}");
                Console.WriteLine($"SDR As Text = {NeoCortexApi.Helpers.StringifyVector(ArrayUtils.IndexWhere(result, k => k == 1))}");

                //saving the SDR values
                string sdrGenerated = NeoCortexApi.Helpers.StringifyVector(result);
                string sdrText = NeoCortexApi.Helpers.StringifyVector(ArrayUtils.IndexWhere(result, k => k == 1));

                string filePath = "sdr_output.txt";
                string output = $"Input = {input}\nSDRs Generated = {sdrGenerated}\nSDR As Text = {sdrText}\n";

                File.AppendAllText(filePath, output + "\n");

                Console.WriteLine($"SDR saved to {filePath}");

                int[,] twoDimenArray = ArrayUtils.Make2DArray<int>(result, (int)Math.Sqrt(result.Length), (int)Math.Sqrt(result.Length));
                int[,] twoDimArray = ArrayUtils.Transpose(twoDimenArray);
                NeoCortexUtils.DrawBitmap(twoDimArray, 1024, 1024, $"{outFolder}\\{input}.png", Color.PaleGreen, Color.Blue, text: input.ToString());

                if (!sdrs.ContainsKey(input))
                {
                    sdrs.Add(input, result);  // Add if key does not exist
                }
                else
                {
                    Console.WriteLine($"Key '{input}' already exists, skipping.");
                }
                //sdrs.Add(input, result);


            }

        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace NeoCortexApiSample
{
    class SDRStorage
    {
        private Dictionary<string, (int[], int[])> savedSDRs = new Dictionary<string, (int[], int[])>();
        private string filePath = "sdr_data.json";

        /// <summary>
        /// Saves the SDR for the given input, overwriting the JSON file with only the latest input and its SDR values.
        /// </summary>
        /// <param name="input">The input string (e.g., a token).</param>
        /// <param name="columnSDR">The column SDR associated with the input.</param>
        /// <param name="cellSDR">The cell SDR associated with the input.</param>

        public void SaveSDR(string input, int[] columnSDR, int[] cellSDR)
        {
            // Clear the dictionary to remove old values
            savedSDRs.Clear();

            savedSDRs[input] = (columnSDR, cellSDR);
            File.WriteAllText(filePath, JsonConvert.SerializeObject(savedSDRs, Formatting.Indented));
        }

        /// <summary>
        /// Loads the SDR for the given input from the JSON file.
        /// </summary>
        /// <param name="input">The input string (e.g., a token).</param>
        /// <returns>A tuple containing the column SDR and cell SDR, or null if the input is not found.</returns>

        public (int[] columnSDR, int[] cellSDR)? LoadSDR(string input)
        {
            if (!File.Exists(filePath)) return null;

            savedSDRs = JsonConvert.DeserializeObject<Dictionary<string, (int[], int[])>>(File.ReadAllText(filePath));
            return savedSDRs.ContainsKey(input) ? savedSDRs[input] : null;
        }
    }
}

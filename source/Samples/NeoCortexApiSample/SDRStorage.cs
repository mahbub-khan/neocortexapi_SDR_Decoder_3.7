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

        public void SaveSDR(string input, int[] columnSDR, int[] cellSDR)
        {
            if (File.Exists(filePath))
            {
                savedSDRs = JsonConvert.DeserializeObject<Dictionary<string, (int[], int[])>>(File.ReadAllText(filePath));
            }

            savedSDRs[input] = (columnSDR, cellSDR);
            File.WriteAllText(filePath, JsonConvert.SerializeObject(savedSDRs, Formatting.Indented));
        }

        public (int[] columnSDR, int[] cellSDR)? LoadSDR(string input)
        {
            if (!File.Exists(filePath)) return null;

            savedSDRs = JsonConvert.DeserializeObject<Dictionary<string, (int[], int[])>>(File.ReadAllText(filePath));
            return savedSDRs.ContainsKey(input) ? savedSDRs[input] : null;
        }
    }
}

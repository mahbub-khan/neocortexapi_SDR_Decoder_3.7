using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDR_Decoder
{
    public static class SDRMetric
    {
        public static double Compare(SparseDistributedRepresentation sdr1, SparseDistributedRepresentation sdr2)
        {
            int intersection = sdr1.Indices.Intersect(sdr2.Indices).Count();
            int union = sdr1.Indices.Union(sdr2.Indices).Count();
            return (double)intersection / union;
        }
    }
}

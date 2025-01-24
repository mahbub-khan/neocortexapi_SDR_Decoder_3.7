using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDR_Decoder
{
    public class SparseDistributedRepresentation
    {
        public int[] Indices { get; set; }
        public SparseDistributedRepresentation(int[] indices)
        {
            Indices = indices;
        }
    }
}

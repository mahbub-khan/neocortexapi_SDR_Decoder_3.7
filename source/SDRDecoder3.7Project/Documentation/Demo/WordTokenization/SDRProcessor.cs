using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordTokenization
{
    public static class SDRProcessor
    {
        /// <summary>
        /// Calculates the cosine similarity between two SDR vectors.
        /// </summary>
        /// <param name="vec1">First SDR vector.</param>
        /// <param name="vec2">Second SDR vector.</param>
        /// <returns>Similarity score between 0 and 1.</returns>
        public static double CosineSimilarity(int[] vec1, int[] vec2)
        {
            int dotProduct = 0; // Stores sum of element-wise multiplication
            double magnitude1 = 0, magnitude2 = 0; // Store vector magnitudes

            // Ensure both vectors have the same length
            int minLength = Math.Min(vec1.Length, vec2.Length);
            for (int i = 0; i < minLength; i++)
            {
                dotProduct += vec1[i] * vec2[i];  // Compute dot product
                magnitude1 += vec1[i] * vec1[i];  // Compute magnitude of first vector
                magnitude2 += vec2[i] * vec2[i];  // Compute magnitude of second vector
            }

            // Compute square roots of magnitudes
            magnitude1 = Math.Sqrt(magnitude1);
            magnitude2 = Math.Sqrt(magnitude2);

            // Avoid division by zero
            return (magnitude1 == 0 || magnitude2 == 0) ? 0 : (dotProduct / (magnitude1 * magnitude2));
        }
    }

}

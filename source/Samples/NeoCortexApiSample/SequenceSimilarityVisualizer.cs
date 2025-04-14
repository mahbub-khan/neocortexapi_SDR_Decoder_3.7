using ScottPlot.Renderable;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XPlot.Plotly;

namespace NeoCortexApiSample
{
    public class SequenceSimilarityVisualizer
    {
        private static List<SimilarityRecord> comparisonHistory = new List<SimilarityRecord>();

        public class SimilarityRecord
        {
            public string Text1 { get; set; }
            public string Text2 { get; set; }
            public double CosineSimilarity { get; set; }
            public double EuclideanSimilarity { get; set; }
            public DateTime Timestamp { get; set; } = DateTime.Now;
        }

        /// <summary>
        /// Records comparison results and triggers visualization when enough data is collected
        /// </summary>
        public static void AddComparison(string text1, string text2, double cosine, double euclidean)
        {
            comparisonHistory.Add(new SimilarityRecord
            {
                Text1 = TruncateText(text1, 15),
                Text2 = TruncateText(text2, 15),
                CosineSimilarity = cosine,
                EuclideanSimilarity = euclidean
            });

            // Show chart after every 2 comparisons
            if (comparisonHistory.Count >= 2)
            {
                ShowComparisonChart();
            }
        }

        /// <summary>
        /// Creates an interactive bar chart comparing all recorded pairs
        /// </summary>
        private static void ShowComparisonChart()
        {
            var cosineBar = new Bar
            {
                name = "Cosine Similarity",
                x = comparisonHistory.Select(r => $"{r.Text1} vs {r.Text2}"),
                y = comparisonHistory.Select(r => r.CosineSimilarity ),
                marker = new Marker { color = "#4285F4" }
            };

            var euclideanBar = new Bar
            {
                name = "Euclidean Similarity",
                x = comparisonHistory.Select(r => $"{r.Text1} vs {r.Text2}"),
                y = comparisonHistory.Select(r => r.EuclideanSimilarity ),
                marker = new Marker { color = "#EA4335" }
            };

            var chart = Chart.Plot(new[] { cosineBar, euclideanBar });
            chart.WithTitle("Text Similarity Comparison");
            chart.WithXTitle("Text Pairs");
            chart.WithYTitle("Similarity Score (%)");
            chart.WithLegend(true);

            // Display in browser
            chart.Show();

            // Optional: Clear history after showing
            // _comparisonHistory.Clear();
        }

        private static string TruncateText(string text, int maxLength)
        {
            return text.Length > maxLength ? text.Substring(0, maxLength) + "..." : text;
        }
    }

}

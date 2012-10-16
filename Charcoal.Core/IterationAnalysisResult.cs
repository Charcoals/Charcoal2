using System.Collections.Generic;

namespace Charcoal.Core
{
    public class IterationAnalysisResult
    {
        public long ProjectId { get; set; }
        public string Name { get; set; }

        public List<IterationResultItem> Items { get; set; }
        public int? NeededAverageVelocity { get; set;}

        public IterationAnalysisResult()
        {
            Items = new List<IterationResultItem>();
        }
    }
}
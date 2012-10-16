using System;
using System.Collections.Generic;
using Charcoal.Core.Entities;

namespace Charcoal.Core
{
    public class IterationResultItem
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int FeaturesAccepted { get; set; }
        public int BugsFixed { get; set; }
        public int BugsAdded { get; set; }
        public int FeaturesAdded { get; set; }
        public int TotalPointsCompleted { get; set; }
        public int? Velocity { get; set; }
        public List<Story> CachedStories { get; set; }
    }
}
using System;
using Charcoal.Core.Entities;

namespace Charcoal.Core
{
    public interface IAnalyticsProvider
    {
        OverviewAnalysisResult AnalyzeProject(long projectId, Predicate<Story> unplannedStoriesPoints = null);
        OverviewAnalysisResult AnalyzeStoryTag(long projectId, string tag, Predicate<Story> unplannedStoriesPoints = null);
        IterationAnalysisResult CreateReleaseProjection(OverviewAnalysisResult overviewAnalysis, DateTime targetDate, int iterationlength, DateTime from);
        IterationAnalysisResult AnalyzeRecentIterations(long projectId);
    }
}
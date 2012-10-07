using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Charcoal.Core.Entities;

namespace Charcoal.Models
{
    public class StoryViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Estimate { get; set; }
        public IterationType IterationType { get; set; }
        public StoryType StoryType { get; set; }
        public StoryStatus Status { get; set; }
        public IEnumerable<TaskViewModel> Tasks { get; private set; }
        public long ProjectId { get; set; }

        public StoryViewModel()
        {
            
        }

        public StoryViewModel(long projectId)
        {
            ProjectId = projectId;
            Tasks = new List<TaskViewModel>();
        }

        public StoryViewModel(Story story)
        {
            Id = story.Id;
            Title = story.Title;
            Description = story.Description;
            Estimate = story.Estimate;
            IterationType = story.IterationType;
            StoryType = story.StoryType;
            Status = story.Status;
            ProjectId = story.ProjectId;
            Tasks = story.Tasks != null 
                ? story.Tasks.Select(e => new TaskViewModel(e))
                : new List<TaskViewModel>();
        }

        public Story ToStory()
        {
            return new Story
                       {
                           Id = Id,
                           ProjectId = ProjectId,
                           Title = Title,
                           Description = Description,
                           Estimate = Estimate,
                           IterationType = IterationType,
                           StoryType = StoryType,
                           Status = Status
                       };
        }

        public string FormattedEstimate
        {
            get
            {
                return Estimate.HasValue ? Estimate + " pts" : "Unestimated";
            }
        }


        public List<SelectListItem> Statuses
        {
            get { return Status.GetEnumItems(); }
        }

        public List<SelectListItem> IterationTypes
        {
            get { return IterationType.GetEnumItems(); }
        }

        public List<SelectListItem> StoryTypes
        {
            get { return StoryType.GetEnumItems(); }
        }

        public string GetHeader()
        {
            return StoryType.ToString();
        }

        public string GetCardStyle()
        {
            return "story-card " + Status.ToString().ToLower();
        }
    }
}
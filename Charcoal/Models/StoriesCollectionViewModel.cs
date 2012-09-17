using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Charcoal.Core.Entities;

namespace Charcoal.Models
{
    public class StoriesCollectionViewModel
    {
        public string CollectionName { get; private set; }
        public IEnumerable<StoryViewModel> Stories { get; private set; }
        public long ProjectId { get; private set; }
        public StoriesCollectionViewModel(string name, IEnumerable<Story> stories, long projectId)
        {
            CollectionName = name;
            Stories = stories.Select(e => new StoryViewModel(e));
            ProjectId = projectId;
        }
    }


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
        public long ProjectId { get; private set; }

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
            if (story.Tasks != null)
            {
                Tasks = story.Tasks.Select(e => new TaskViewModel(e));
            }
            else
            {
                Tasks=new List<TaskViewModel>();
            }
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

    public class TaskViewModel
    {
        public string Description { get; private set; }
        public string Assignees { get; private set; }
        public long Id { get; private set; }
        public int Position { get; private set; }
        public bool IsCompleted { get; private set; }

        public TaskViewModel(Task task)
        {
            Id = task.Id;
            Description = task.Description;
            Position = task.Position;
            IsCompleted = task.IsCompleted;
            Assignees = task.Assignees;
        }

        public string GetStyle()
        {
            return this.IsCompleted ? "task complete" : "task";
        }
    }
}
using System.Collections.Generic;
using System.Linq;
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
        public long Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Estimate { get; private set; }
        public IterationType IterationType { get; private set; }
        public StoryType StoryType { get; private set; }
        public StoryStatus Status { get; private set; }
        public IEnumerable<TaskViewModel> Tasks { get; private set; }

        public StoryViewModel(Story story)
        {
            Id = story.Id;
            Title = story.Title;
            Description = story.Description;
            Estimate = story.Estimate.HasValue ? story.Estimate + " pts" : "Unestimated";
            IterationType = story.IterationType;
            StoryType = story.StoryType;
            Status = story.Status;

            if (story.Tasks != null)
                Tasks = story.Tasks.Select(e => new TaskViewModel(e));
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
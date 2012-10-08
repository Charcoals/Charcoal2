using Charcoal.Core.Entities;

namespace Charcoal.Models
{
    public class TaskViewModel
    {
        public string Description { get; private set; }
        public string Assignees { get; private set; }
        public long Id { get; private set; }
        public long StoryId { get; private set; }
        public int Position { get; private set; }
        public bool IsCompleted { get; private set; }

        public TaskViewModel(Task task)
        {
            Id = task.Id;
            Description = task.Description;
            Position = task.Position;
            IsCompleted = task.IsCompleted;
            Assignees = task.Assignees;
            StoryId = task.StoryId;
        }

        public TaskViewModel(long storyId, string description, string assignees)
        {
            StoryId = storyId;
            Description = description;
            Assignees = assignees;
        }

        public TaskViewModel(long storyId)
        {
            StoryId = storyId;
        }

        public Task ToTask()
        {
            return new Task
                       {
                           Id = Id,
                           Position = Position,
                           StoryId = StoryId,
                           Description = Description,
                           Assignees = Assignees
                       };
        }

        public string GetStyle()
        {
            return this.IsCompleted ? "task complete" : "task";
        }
    }
}
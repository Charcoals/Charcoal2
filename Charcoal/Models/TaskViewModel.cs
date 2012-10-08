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

        public string GetStyle()
        {
            return this.IsCompleted ? "task complete" : "task";
        }
    }
}
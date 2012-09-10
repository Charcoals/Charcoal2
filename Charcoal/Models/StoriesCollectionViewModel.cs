using System.Collections.Generic;
using System.Linq;
using Charcoal.Core.Entities;

namespace Charcoal.Models
{
    public class StoriesCollectionViewModel
    {
        public string CollectionName { get; private set; }
        public IEnumerable<StoryViewModel> Stories { get; private set; }

        public StoriesCollectionViewModel(string name, IEnumerable<Story> stories )
        {
            CollectionName = name;
            Stories = stories.Select(e => new StoryViewModel(e));
        }
    }

    public class StoryViewModel
    {
        public long Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int Estimate { get; private set; }
        public IEnumerable<TaskViewModel> Tasks { get; private set; }

        public StoryViewModel(Story story)
        {
            Id = story.Id;
            Title = story.Title;
            Description = story.Description;
            
            if(story.Tasks != null)
                Tasks = story.Tasks.Select(e => new TaskViewModel(e));
        }
        
    }

    public class TaskViewModel
    {
        public string Description { get; private set; }
        public long Id { get; private set; }
        public int Position { get; private set; }

        public TaskViewModel(Task task)
        {
            Id = task.Id;
            Description = task.Description;
            Position = task.Position;
        }
    }
}
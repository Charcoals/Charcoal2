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
}
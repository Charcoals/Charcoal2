using Charcoal.Core.Entities;
using System.Linq;

namespace Charcoal.Models
{
    public class ProjectViewModel
    {

        public ProjectViewModel(Project project)
        {
            Title = project.Title;
            Description = project.Description;
            Velocity = project.Velocity;
            Id = project.Id;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public int Velocity { get; set; }
        public long Id { get; set; }
    }
}
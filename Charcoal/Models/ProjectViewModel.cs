using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

    public class ProjectEditModel
    {
        public ProjectEditModel(Project project,List<User> availableUsers  )
        {
            Title = project.Title;
            Description = project.Description;
            Id = project.Id;
            Members = project.Users != null ? project.Users.ConvertAll(e => new MemberViewModel(e)) : new List<MemberViewModel>();
            AvailableMembers = availableUsers != null ? availableUsers.ConvertAll(e => new MemberViewModel(e)) : new List<MemberViewModel>();
        }
        public long Id { get; set; }
        public List<MemberViewModel> Members { get; private set; }
        public List<MemberViewModel> AvailableMembers { get; private set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class ProjectCreateModel
    {
        [Required]
        [Display(Name = "Project Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Initial Velocity")]
        public int Velocity { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public Project Convert()
        {
            return new Project
                       {
                           Title = Title,
                           Description = Description,
                           Velocity = Velocity
                       };
        }
    }
}
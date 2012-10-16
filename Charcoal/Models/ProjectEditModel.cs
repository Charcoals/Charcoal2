using System.Collections.Generic;
using Charcoal.Core.Entities;

namespace Charcoal.Models
{
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
}
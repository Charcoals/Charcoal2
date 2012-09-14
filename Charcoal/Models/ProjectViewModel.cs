using System.Collections.Generic;
using Charcoal.Core.Entities;
using System.Linq;

namespace Charcoal.Models {
	public class ProjectViewModel {

		public ProjectViewModel(Project project) {
			Title = project.Title;
			Description = project.Description;
			Velocity = project.Velocity;
			Id = project.Id;
            
            if(project.Users != null)
            {
                Members = project.Users.Select(e => new MemberViewModel(e));
            }
		}
        
        public IEnumerable<MemberViewModel> Members {get;private set;}
		public string Title { get; set; }
		public string Description { get; set; }
		public int Velocity { get; set; }
		public long Id { get; set; }
	}

    public class MemberViewModel
    {
        public MemberViewModel(User user)
        {
            UserName = user.UserName;
            UserName = user.Email;
        }

        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
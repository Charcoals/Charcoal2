using System.Collections.Generic;
using System.Linq;
using Charcoal.Core.Entities;

namespace Charcoal.Models {
	public class DashboardViewModel {
		public List<ProjectViewModel> Projects { get; private set; }
		public List<ActivityViewModel> Activities { get; private set; }

		public DashboardViewModel() {
			Projects = new List<ProjectViewModel>();
			Activities = new List<ActivityViewModel>();
		}

		public DashboardViewModel(IEnumerable<Project> projects, IEnumerable<ActivityViewModel> activities) {
			Projects=new List<ProjectViewModel>(projects.Select(e=>new ProjectViewModel(e)));
			Activities = new List<ActivityViewModel>(activities);
		}

	}
}
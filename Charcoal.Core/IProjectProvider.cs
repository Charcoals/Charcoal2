using System.Collections.Generic;
using Charcoal.Core.Entities;

namespace Charcoal.Core
{
	public interface IProjectProvider {
		List<Project> GetProjectsByUser(string userName);
		List<Project> GetProjects();
	    OperationResponse CreateProject(Project project);
	}
}
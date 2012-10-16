using System.Collections.Generic;
using Charcoal.Core.Entities;

namespace Charcoal.Core
{
	public interface IProjectProvider {
		List<Project> GetProjectsByUser(string userName);
		List<Project> GetProjects();
	    Project GetProject(long id);
	    List<User> GetAvailableUsers();
        OperationResponse CreateProject(Project project);
	    OperationResponse AssociateUserToProject(long projectId, long userId);
        OperationResponse DisassociateUserToProject(long projectId, long userId);
	}
}
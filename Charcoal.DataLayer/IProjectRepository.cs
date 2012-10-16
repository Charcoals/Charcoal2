using System.Collections.Generic;

namespace Charcoal.DataLayer
{
    public interface IProjectRepository : IRepository
    {
        List<dynamic> GetProjectsByUseToken(string apiToken);
        DatabaseOperationResponse CreateProjectAssociatedWithKey(dynamic project, string apiToken);
        DatabaseOperationResponse AddUserToProject(long projectId, long userId);
        DatabaseOperationResponse RemoveUserFromProject(long projectId, long userId);
    }
}
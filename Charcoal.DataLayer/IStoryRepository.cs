using System.Collections.Generic;

namespace Charcoal.DataLayer
{
    public interface IStoryRepository:IRepository
    {
        List<dynamic> FindAllByIterationType(long projectId, int iterationType);
        List<dynamic> FindAllByProjectId(long projectId);
        dynamic UpdateStoryStatus(long storyId, int status);
    }
}
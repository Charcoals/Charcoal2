using System.Collections.Generic;
using Charcoal.Core.Entities;

namespace Charcoal.Core
{
	public interface IStoryProvider
	{
		Story AddNewStory(long projectId, Story toBeSaved);
        Task AddNewTask(Task task);
		List<Story> GetStories(long projectId,IterationType iterationType);
		List<Story> GetAllStories(long projectId);
        List<Story> GetAllStoriesByTag(long projectId, string tag);
		Story FinishStory(long projectId, long storyId, IterationType iterationType);
		Story StartStory(long projectId, long storyId, IterationType iterationType);
		Story GetStory(long projectId, long storyId, IterationType iterationType);
		bool RemoveStory(long storyId);
		Task GetTask(long taskId);
        Task ToggleTaskStatus(long taskId);
		bool RemoveTask(long taskId);
	    OperationResponse UpdateTask(Task task, long projectId);
		void ReorderTasks(long projectId, long storyId, List<Task> tasks);
		void AddComment(long projectId, long storyId, string comment);
	    List<Iteration> GetRecentIterations(long projectId, int number);
	    bool UpdateStory(Story toStory);
	}
}
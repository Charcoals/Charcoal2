using System.Collections.Generic;
using Charcoal.Common;
using Charcoal.Core.Entities;
using Charcoal.DataLayer;
using System;

namespace Charcoal.Core
{
    public class CharcoalStoryProvider : IStoryProvider
    {
        private readonly string m_apiKey;
        private readonly IUserRepository m_userRepo;
        private long m_userId = -1;
        private readonly ITaskRepository m_taskRepository;
        private readonly IStoryRepository m_storyRepository;


        public CharcoalStoryProvider(string apiKey, IStoryRepository storyRepository = null, ITaskRepository taskRepository = null, IUserRepository userRepo = null)
        {
            m_apiKey = apiKey;
            m_userRepo = userRepo ?? new UserRepository();
            m_taskRepository = taskRepository ?? new TaskRepository();
            m_storyRepository = storyRepository ?? new StoryRepository();
        }

        public long UserId
        {
            get
            {
                if (m_userId == -1)
                {
                    m_userId = (long)m_userRepo.FindByAPIKey(m_apiKey).Id;
                }
                return m_userId;
            }
        }

        public Story AddNewStory(long projectId, Story toBeSaved)
        {
            toBeSaved.ProjectId = projectId;
            toBeSaved.CreatedBy = UserId;
            var response = m_storyRepository.Save(toBeSaved);
            return response.HasSucceeded ? response.Object : null;
        }

        public Task AddNewTask(Task task)
        {
            var response = m_taskRepository.Save(task);
            return response.HasSucceeded ? response.Object : null;
        }

        public List<Story> GetStories(long projectId, IterationType iterationType)
        {
            return m_storyRepository.FindAllByIterationType(projectId, (int)iterationType).ConvertAll(e => (Story)e);
        }

        public List<Story> GetAllStories(long projectId)
        {
            return m_storyRepository.FindAllByProjectId(projectId).ConvertAll(e => (Story)e);
        }

        public List<Story> GetAllStoriesByTag(long projectId, string tag)
        {
            throw new System.NotImplementedException();
        }

        public Story FinishStory(long projectId, long storyId, IterationType iterationType)
        {
            return m_storyRepository.UpdateStoryStatus(storyId, (int)StoryStatus.Finished);
        }

        public Story StartStory(long projectId, long storyId, IterationType iterationType)
        {
            return m_storyRepository.UpdateStoryStatus(storyId, (int)StoryStatus.Started);
        }

        public Story GetStory(long projectId, long storyId, IterationType iterationType)
        {
            return m_storyRepository.Find(storyId);
        }

        public bool RemoveStory(long storyId)
        {
            return m_storyRepository.Delete(storyId).HasSucceeded;
        }

        public Task GetTask(long taskId)
        {
            return m_taskRepository.Find(taskId);
        }

        public Task ToggleTaskStatus(long taskId)
        {
            var originalTask =(Task) m_taskRepository.Find(taskId);
            originalTask.IsCompleted = !originalTask.IsCompleted;

            ITaskRepository taskRepository = m_taskRepository;
            var response = taskRepository.Update(originalTask);
            return response.HasSucceeded ? originalTask : null;
        }

        public bool RemoveTask(long taskId)
        {
            return m_taskRepository.Delete(taskId).HasSucceeded;
        }

        public OperationResponse UpdateTask(Task task, long projectId)
        {
            var res = m_taskRepository.Update(task);
            return new OperationResponse(res.HasSucceeded, res.Description);
        }

        public void ReorderTasks(long projectId, long storyId, List<Task> tasks)
        {
            m_taskRepository.Save(tasks);
        }

        public void AddComment(long projectId, long storyId, string comment)
        {
            throw new System.NotImplementedException();
        }

        public List<Iteration> GetRecentIterations(long projectId, int number)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateStory(Story toStory)
        {
            toStory.LastEditedOn = DateTime.UtcNow;
            toStory.CreatedBy = UserId;
            return m_storyRepository.Update(toStory).HasSucceeded;
        }
    }
}
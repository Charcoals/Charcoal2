﻿using System.Collections.Generic;
using System.Linq;
using Charcoal.Common;
using Charcoal.Common.Entities;
using Charcoal.Common.Providers;
using PivotalTrackerDotNet;
using Story = PivotalTrackerDotNet.Domain.Story;

namespace Charcoal.PivotalTracker
{
    public class PTStoryProvider :  IStoryProvider
    {
        private StoryService m_service;
        public PTStoryProvider(string token)
        {
            m_service = new StoryService(token.ParseToken());
        }

        public Charcoal.Common.Entities.Story AddNewStory(long projectId, Charcoal.Common.Entities.Story toBeSaved)
        {
            return m_service.AddNewStory((int)projectId, toBeSaved.ConvertTo(projectId)).ConvertTo(IterationType.Undefined);
        }

        public Charcoal.Common.Entities.Task AddNewTask(Charcoal.Common.Entities.Task task, long projectId)
        {
            return m_service.AddNewTask(task.ConvertTo(projectId)).ConvertTo();
        }

        public List<Charcoal.Common.Entities.Story> GetStories(long projectId, IterationType iterationType)
        {
            var stories = new List<Story>();
            switch (iterationType)
            {
                case IterationType.Current:
                    stories = m_service.GetCurrentStories((int)projectId);
                    break;
                case IterationType.Icebox:
                    stories = m_service.GetIceboxStories((int)projectId);
                    break;
                case IterationType.Backlog:
                    stories = m_service.GetBacklogStories((int)projectId);
                    break;
            }
            return stories.ConvertAll(e => e.ConvertTo(iterationType));
        }

        public List<Charcoal.Common.Entities.Story> GetAllStories(long projectId)
        {
            return m_service.GetAllStories((int)projectId,false).ConvertAll(e => e.ConvertTo(IterationType.Undefined));
        }

        public List<Common.Entities.Story> GetAllStoriesByTag(long projectId, string tag)
        {
            return m_service.GetAllStoriesMatchingFilter((int)projectId, string.Format("label:\"{0}\" includedone:true",tag),false).ConvertAll(e => e.ConvertTo(IterationType.Undefined));
        }

        public Charcoal.Common.Entities.Story FinishStory(long projectId, long storyId, IterationType iterationType)
        {
            return m_service.FinishStory((int)projectId, (int)storyId).ConvertTo(iterationType);
        }

        public Charcoal.Common.Entities.Story StartStory(long projectId, long storyId, IterationType iterationType)
        {
            return m_service.StartStory((int)projectId, (int)storyId).ConvertTo(iterationType);
        }

        public Charcoal.Common.Entities.Story GetStory(long projectId, long storyId, IterationType iterationType)
        {
            return m_service.GetStory((int)projectId, (int)storyId).ConvertTo(iterationType);
        }

        public bool RemoveStory(long projectId, long storyId)
        {
            return m_service.RemoveStory((int) projectId, (int) storyId).ConvertTo(IterationType.Undefined) != null;
        }

        public Charcoal.Common.Entities.Task GetTask(long projectId, long storyId, long taskId)
        {
            return m_service.GetTask((int)projectId, (int)storyId, (int)taskId).ConvertTo();
        }

        public bool RemoveTask(long projectId, long storyId, long taskId)
        {
            return m_service.RemoveTask((int)projectId, (int)storyId, (int)taskId);
        }

        public OperationResponse UpdateTask(Charcoal.Common.Entities.Task task, long projectId)
        {
            m_service.SaveTask(task.ConvertTo(projectId));
            return new OperationResponse(true);
        }

        public void ReorderTasks(long projectId, long storyId, List<Charcoal.Common.Entities.Task> tasks)
        {
            m_service.ReorderTasks((int)projectId, (int)storyId, tasks.Select(e=> e.ConvertTo(projectId)).ToList());
        }

        public void AddComment(long projectId, long storyId, string comment)
        {
            m_service.AddComment((int)projectId, (int)storyId, comment);
        }

        public List<Iteration> GetRecentIterations(long projectId, int number)
        {
            return m_service.GetLastIterations((long) projectId, number).ConvertAll(e => e.ConvertTo(IterationType.Done));
        }
    }
}

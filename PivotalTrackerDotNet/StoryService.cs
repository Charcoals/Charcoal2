﻿using System.Collections.Generic;
using PivotalTrackerDotNet.Domain;
using RestSharp.Contrib;

namespace PivotalTrackerDotNet {
	public class StoryService : AAuthenticatedService {
		private const string StoryEndpoint = "projects/{0}/iterations/current";
		const string TaskEndpoint = "projects/{0}/stories/{1}/tasks";
		public List<Story> CachedStories { get; private set; }

		public StoryService(AuthenticationToken token)
			: base(token) {
			CachedStories = new List<Story>();
		}

		public Story GetStory(int projectId, int storyId) {
			var request = BuildGetRequest();
			request.Resource = string.Format(StoryEndpoint + "/{1}", projectId, storyId);

			var response = RestClient.Execute<Story>(request);
			var story = response.Data;

			return GetStoryWithTasks(projectId, story);
		}

		public List<Story> GetStories(int projectId) {
			var request = BuildGetRequest();
			request.Resource = string.Format(StoryEndpoint, projectId);

			var response = RestClient.Execute<List<Story>>(request);
			var stories = response.Data;
			foreach (var story in stories) {
				GetStoryWithTasks(projectId, story);
			}
			return stories;
		}

		Story GetStoryWithTasks(int projectId, Story story) {
			var request = BuildGetRequest();
			request.Resource = string.Format(TaskEndpoint, projectId, story.Id);
			var taskResponse = RestClient.Execute<List<Task>>(request);
			story.Tasks = taskResponse.Data;
			if (story.Tasks != null) {
				story.Tasks.ForEach(e => {
					e.ParentStoryId = story.Id;
					e.ProjectId = projectId;
				});
			}
			return story;
		}

		public void SaveTask(Task task) {
			var request = BuildPutRequest();
			request.Resource = string.Format(TaskEndpoint + "/{2}?task[description]={3}&task[complete]={4}", task.ProjectId, task.ParentStoryId, task.Id, HttpUtility.UrlEncode(task.Description), task.Complete);
			RestClient.Execute(request);
		}
	}
}

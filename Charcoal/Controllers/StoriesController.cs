using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Charcoal.Core;
using Charcoal.Core.Entities;
using Charcoal.Models;

namespace Charcoal.Controllers
{
    public class StoriesController : BaseController
    {
        private readonly IStoryProvider m_storyProvider;
        //TODO: find out why the project Id keeps getting lost on story edit, and fix it

        public StoriesController(IStoryProvider storyProvider)
        {
            m_storyProvider = storyProvider;
        }

        public ActionResult CurrentTab(long projectId)
        {
            var stories = m_storyProvider.GetStories(projectId, IterationType.Current);
            return View("DeveloperView", new StoriesCollectionViewModel("Current", stories, projectId));
        }

        public ActionResult BacklogTab(long projectId)
        {
            var stories = m_storyProvider.GetStories(projectId, IterationType.Backlog);
            return View("DeveloperView", new StoriesCollectionViewModel("Back", stories, projectId));
        }

        public ActionResult IceboxTab(long projectId)
        {
            var stories = m_storyProvider.GetStories(projectId, IterationType.Icebox);
            return View("DeveloperView", new StoriesCollectionViewModel("Ice", stories, projectId));
        }

        [HttpPost]
        public string CreateStory(long projectId, string title, string description, string estimate, string iterationType, string storytype, string status)
        {
            var createdStory = m_storyProvider.AddNewStory(projectId, CreateStory(title, description, estimate, iterationType, storytype, status));
            return RenderPartialViewToString("StoryRow", new StoryViewModel(createdStory));

        }

        [HttpDelete]
        public string DeleteStory(long storyId)
        {
            bool isSucess = m_storyProvider.RemoveStory(storyId);
            if (isSucess)
            {
                return "success";
            }
            return "Could not delete the story";
        }

        [HttpPost]
        public RedirectResult EditStory(StoryViewModel story)
        {
            bool isUpdate = m_storyProvider.UpdateStory(story.ToStory());
            return Redirect(Request.UrlReferrer.ToString());

        }

        static Story CreateStory(string title, string description, string estimate, string iterationType, string storytype, string status)
        {
            return new Story
                       {
                           Title = title,
                           Description = description,
                           Estimate = estimate.Equals("undefined") ? null : (int?)int.Parse(estimate),
                           IterationType = iterationType.Equals("undefined") ? IterationType.Icebox : (IterationType)Enum.Parse(typeof(IterationType), iterationType),
                           StoryType = storytype.Equals("undefined") ? StoryType.Feature : (StoryType)Enum.Parse(typeof(StoryType), storytype),
                           Status = status.Equals("undefined") ? StoryStatus.UnScheduled : (StoryStatus)Enum.Parse(typeof(StoryStatus), status),
                       };
        }

    }
}

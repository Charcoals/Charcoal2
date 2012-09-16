using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Charcoal.Core.Entities;
using Charcoal.Models;

namespace Charcoal.Controllers
{
    public class StoriesController : AuthenticatedController
    {
        //
        // GET: /Stories/
        private List<Story> stories = new List<Story>
                                          {
                                              new Story
                                                  {
                                                      Id = 1,
                                                      Description = "Story 1 desc",
                                                      Title = "Story 1",
                                                      Estimate = 11,
                                                      Tasks = new List<Task>{ new Task{Description = "task1", Position = 1},new Task{Description = "task2", Position = 2}}
                                                  },
                                              new Story
                                                  {
                                                      Id = 2,
                                                      Description = "Story 2 desc",
                                                      Title = "Story 2",
                                                      Estimate = 12,
                                                      Tasks = new List<Task>{ new Task{Description = "task1", Position = 1},new Task{Description = "task2", Position = 2}}
                                                  },
                                              new Story
                                                  {
                                                      Id = 3,
                                                      Description = "Story 3 desc",
                                                      Title = "Story 3",
                                                      Estimate = 13,
                                                      Tasks = new List<Task>{ new Task{Description = "task1", Position = 1},new Task{Description = "task2", Position = 2}}
                                                  },
                                              new Story
                                                  {
                                                      Id = 4,
                                                      Description = "Story 4 desc",
                                                      Title = "Story 4",
                                                      Estimate = 14,
                                                      Tasks = new List<Task>{ new Task{Description = "task1", Position = 1},new Task{Description = "task2", Position = 2}}
                                                  }
                                          };

        public ActionResult CurrentTab(long projectId)
        {
            return View("DeveloperView", new StoriesCollectionViewModel("Current", stories, projectId));
        }

        public ActionResult BacklogTab(long projectId)
        {
            return View("DeveloperView", new StoriesCollectionViewModel("Back", stories, projectId));
        }

        public ActionResult IceboxTab(long projectId)
        {
            return View("DeveloperView", new StoriesCollectionViewModel("Ice", stories, projectId));
        }

        [HttpPost]
        public string CreateStory(long projectId, string title, string description, string estimate, string iterationType, string storytype, string status)
        {
            var story = new StoryViewModel(new Story
                                               {
                                                   Id = 4,
                                                   Description = "Story 4 desc",
                                                   Title = "Story 10",
                                                   Estimate = 14,
                                                   Tasks =
                                                       new List<Task>
                                                           {
                                                               new Task {Description = "task1", Position = 1},
                                                               new Task {Description = "task2", Position = 2}
                                                           },
                                                   Status = StoryStatus.Started,
                                                   StoryType = StoryType.Bug,
                                                   IterationType = IterationType.Current
                                               });
            return RenderPartialViewToString("StoryRow", story);

        }

        string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

    }
}

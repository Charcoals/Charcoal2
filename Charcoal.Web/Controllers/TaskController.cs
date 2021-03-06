﻿using System;
using System.Web.Mvc;
using Charcoal.Common.Entities;
using Charcoal.Web.Models;
using Charcoal.Common.Providers;

namespace Charcoal.Web.Controllers
{
    public class TaskController : BaseController
    {
        readonly IStoryProvider service;

        public TaskController() : this(null) { }

        public TaskController(IStoryProvider service)
            : base()
        {
            this.service = service;
        }

        [HttpPut]
        public ActionResult SignUp(string initials, string[] fullIds)
        {
            int projectId = 0, storyId = 0, iterationType = 0;

            foreach (var s in fullIds)
            {
                int taskId = 0;
                var parts = s.Split('-');
                if (parts.Length != 4)
                {
                    throw new InvalidOperationException("Must pass ids in the format projectId-storyId-taskId-iterationType");
                }
                if (!int.TryParse(parts[0], out projectId))
                {
                    throw new InvalidOperationException("Invalid project id");
                }
                if (!int.TryParse(parts[1], out storyId))
                {
                    throw new InvalidOperationException("Invalid story id");
                }
                if (!int.TryParse(parts[2], out taskId))
                {
                    throw new InvalidOperationException("Invalid task id");
                }
                if (!int.TryParse(parts[3], out iterationType))
                {
                    throw new InvalidOperationException("Invalid task id");
                }

                var task = service.GetTask(projectId, storyId, taskId);
                task.Description = AddInitialsToDescription(task, initials, projectId, iterationType);
                service.UpdateTask(task, projectId);
            }
            return RedirectToAction("Get", "Stories", new { projectId = projectId, storyId = storyId, iterationType = iterationType });
        }

        private string AddInitialsToDescription(Task task, string initials, long projectId, int iterationType)
        {
            return new TaskViewModel(task, projectId, (IterationType)iterationType).GetDescriptionWithoutOwners() + (string.IsNullOrEmpty(initials) ? "" : (" (" + initials.ToUpper() + ")"));
        }

        [HttpPut]
        public ActionResult Complete(int id, int storyId, int projectId, bool completed, int iteration)
        {
            var task = service.GetTask(projectId, storyId, id);
            if (task.IsCompleted != completed)
            {
                task.IsCompleted = completed;
                service.UpdateTask(task, projectId);
            }
            return PartialView("TaskDetails", new TaskViewModel(task, projectId, (IterationType)iteration));
        }

        [HttpGet]
        public ActionResult Add(long storyId, long projectId, IterationType iterationType)
        {
            return PartialView(new TaskViewModel(new Task { StoryId = storyId }, projectId, iterationType));
        }

        [HttpPut]
        public ActionResult ReOrder(string taskArray)
        {
            //    var arr = taskArray.Split(',');
            //    var tasks = arr.Select(ExtractTask).ToList();
            //    var firstTask = tasks.First();
            //    Service.ReorderTasks(firstTask.ProjectId, firstTask.ParentStoryId, tasks);
            return new EmptyResult();
        }

        //private Task ExtractTask(string taskIds, int position) {
        //    var ids = taskIds.Split('-');
        //    return new Task { ProjectId = int.Parse(ids[0]), ParentStoryId = int.Parse(ids[1]), Id = int.Parse(ids[2]), Position = position + 1 };
        //}
    }
}

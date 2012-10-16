using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Charcoal.Core;
using System.Linq;
using Charcoal.Core.Entities;
using Charcoal.Models;

namespace Charcoal.Controllers
{
    public class ProjectController : BaseController
    {
        private readonly IProjectProvider m_projectProvider;

        public ProjectController(IProjectProvider projectProvider)
        {
            m_projectProvider = projectProvider;
        }

        public ActionResult Index()
        {
            return View(GetUserProjects());
        }

        private IEnumerable<ProjectViewModel> GetUserProjects()
        {
            var projects = m_projectProvider.GetProjects();
            var projectViewModels = projects.Select(e => new ProjectViewModel(e));
            return projectViewModels;
        }

        public ActionResult Edit(long id)
        {
            var project = m_projectProvider.GetProject(id);
            var availableUsers = m_projectProvider.GetAvailableUsers();
            return View(new ProjectEditModel(project, availableUsers));
            //return Json(new ProjectEditModel(project, availableUsers), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string AddUserToProject(long projectId, long userId)
        {
            bool isSucess = m_projectProvider.AssociateUserToProject(projectId, userId).HasSucceeded;
            if (isSucess)
            {
                return "success";
            }
            return "Could not delete the task";
        }

        [HttpPost]
        public string RemoveUserFromProject(long projectId, long userId)
        {
            bool isSucess = m_projectProvider.DisassociateUserToProject(projectId, userId).HasSucceeded;
            if (isSucess)
            {
                return "success";
            }
            return "Could not delete the task";
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProjectCreateModel project)
        {
            if (ModelState.IsValid)
            {
                var response = m_projectProvider.CreateProject(project.Convert());

                if (response.HasSucceeded)
                {
                    return View("Index", GetUserProjects());
                }

                ModelState.AddModelError("Title", "Could not create project: "+response.Message);
            }
            return View(project);
        }
    }
}
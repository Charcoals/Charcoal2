using System;
using System.Web.Mvc;
using Charcoal.Core;
using System.Linq;
using Charcoal.Models;

namespace Charcoal.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectProvider m_projectProvider;

        public ProjectController(IProjectProvider projectProvider)
        {
            m_projectProvider = projectProvider;
        }

        public ActionResult Index()
        {
            var projects = m_projectProvider.GetProjects();
            return View(projects.Select(e=>new ProjectViewModel(e)));
        }


        public string Edit(long id)
        {
            return "llll";
        }

        public ActionResult Create()
        {
            throw new NotImplementedException();
        }
    }
}
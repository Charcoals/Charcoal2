using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Charcoal.Core.Entities;
using Charcoal.Models;
using Charcoal.Core;

namespace Charcoal.Controllers
{
    public class DashboardController : AuthenticatedController
    {
        private readonly IProjectProvider m_projectProvider;

        public DashboardController(IProjectProvider projectProvider)
        {
            m_projectProvider = projectProvider;
        }

        public ActionResult Index()
        {
            var projects = m_projectProvider.GetProjects();
            return View(new DashboardViewModel(projects, new List<ActivityViewModel>()));
        }

        public ActionResult Help()
        {
            throw new NotImplementedException();
        }
    }
}

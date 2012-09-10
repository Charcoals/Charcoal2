using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Charcoal.Core.Entities;
using Charcoal.Models;

namespace Charcoal.Controllers
{
    public class DashboardController : Controller
    {
        private List<Project> projects = new List<Project>
                                             {
                                                 new Project
                                                     {
                                                         Description = "project 1 description",
                                                         Title = "Project 1 title",
                                                         Id = 1,
                                                         Velocity = 10
                                                     },
                                                 new Project
                                                     {
                                                         Description =
                                                             "project 2 description",
                                                         Title = "Project 2 title",
                                                         Id = 2,
                                                         Velocity = 15
                                                     },
                                                 new Project
                                                     {
                                                         Description =
                                                             "project 3 description",
                                                         Title = "Project 3 title",
                                                         Id = 3,
                                                         Velocity = 25
                                                     }
                                             };

        private List<ActivityViewModel> activities = new List<ActivityViewModel>
                                                         {
                                                             new ActivityViewModel
                                                                 {
                                                                     Description = "Update stufff 1",
                                                                     OccuredAt = new DateTime(2011, 2, 2)
                                                                 },
                                                             new ActivityViewModel
                                                                 {
                                                                     Description = "Update stufff 2",
                                                                     OccuredAt = new DateTime(2011, 2, 3)
                                                                 },
                                                             new ActivityViewModel
                                                                 {
                                                                     Description = "Update stufff 3",
                                                                     OccuredAt = new DateTime(2011, 2, 4)
                                                                 }
                                                         };
        public ActionResult Index()
        {
            return View(new DashboardViewModel(projects, activities));
        }

        public ActionResult Help()
        {
            throw new NotImplementedException();
        }
    }
}

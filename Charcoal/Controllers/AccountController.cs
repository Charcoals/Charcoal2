using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Charcoal.Models;

namespace Charcoal.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Index()
        {
            return View();
        }

				public string Login(LogOnModel logOnModel) {
					return "in";
				}

    }
}

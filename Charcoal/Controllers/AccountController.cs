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

        public ActionResult Login(LogOnModel logOnModel)
        {

            return RedirectToAction("Index", "Dashboard");
            //return "in";
        }

        public ActionResult Profile()
        {
            throw new NotImplementedException();
        }

        public ActionResult LogOff()
        {
            throw new NotImplementedException();
        }
    }
}

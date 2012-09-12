using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Charcoal.Models;

namespace Charcoal.Controllers
{
    [SessionState(System.Web.SessionState.SessionStateBehavior.Required)]
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel logOnModel)
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

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel user)
        {
            return View();
        }
    }


}

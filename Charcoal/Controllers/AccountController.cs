using System;
using System.Web.Mvc;
using System.Web.Security;
using Charcoal.Core;
using Charcoal.Models;

namespace Charcoal.Controllers
{
    [SessionState(System.Web.SessionState.SessionStateBehavior.Required)]
    public class AccountController : BaseController
    {
        readonly IAccountProvider m_accountProvider;

        public AccountController(IAccountProvider accountProvider)
        {
            m_accountProvider = accountProvider;
        }

        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var token = m_accountProvider.Authenticate(model.UserName, model.Password);
                if (!string.IsNullOrWhiteSpace(token))
                {
                    Token = token;
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Dashboard");
                }
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }

            return View(model);
        }

        public ActionResult Profile()
        {
            throw new NotImplementedException();
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Token = null;
            return RedirectToAction("LogOn");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel user)
        {
            if (ModelState.IsValid)
            {
                MembershipCreateStatus createStatus;
                Membership.CreateUser(user.UserName, user.Password, user.Email, user.FirstName, user.LastName, false, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    return RedirectToAction("LogOn");
                }
            }
            return View(user);
        }
    }


}

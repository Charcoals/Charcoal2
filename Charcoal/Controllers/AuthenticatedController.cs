using System;
using System.Web.Mvc;
using System.Web.Security;

namespace Charcoal.Controllers
{
    [SessionState(System.Web.SessionState.SessionStateBehavior.ReadOnly)]
    public class AuthenticatedController : Controller {
        protected string Token {
            get {
                if (Session != null) {
                    var token = Session["token"];
                    if (token != null && token is string) {
                        return token as string;
                    }
                }
                throw new NotAuthenticatedException();
            }
            set { Session.Add("token", value); }
        }

        protected override void OnException(ExceptionContext filterContext) {
            if (filterContext.Exception is NotAuthenticatedException) {
                FormsAuthentication.SignOut();
                Response.Redirect("~/Account/LogOn?returnUrl=" + filterContext.HttpContext.Request.Url);
            }
            base.OnException(filterContext);
        }
    }
    public class NotAuthenticatedException : Exception { }

}
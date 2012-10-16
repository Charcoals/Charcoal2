using System.IO;
using System.Web.Mvc;
using System.Web.Security;

namespace Charcoal.Controllers
{
    [SessionState(System.Web.SessionState.SessionStateBehavior.ReadOnly)]
    public class BaseController : Controller
    {
        protected string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        protected string Token
        {
            get
            {
                if (Session != null)
                {
                    var token = Session["token"];
                    if (token != null && token is string)
                    {
                        return token as string;
                    }
                }
                throw new NotAuthenticatedException();
            }
            set { Session.Add("token", value); }
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is NotAuthenticatedException)
            {
                FormsAuthentication.SignOut();
                Response.Redirect("~/Account/LogOn?returnUrl=" + filterContext.HttpContext.Request.Url);
            }
            base.OnException(filterContext);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Charcoal.Common.Config;
using Charcoal.Core;
using Charcoal.DataLayer;
using StructureMap;

namespace Charcoal {
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication {
		public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes) {
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
					"Default", // Route name
					"{controller}/{action}/{id}", // URL with parameters
                    new { controller = "Account", action = "LogOn", id = UrlParameter.Optional } // Parameter defaults
			);

		}

		protected void Application_Start() {
			AreaRegistration.RegisterAllAreas();

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);

            var tokenRetrieval = new Func<string>(() =>
            {
                if (HttpContext.Current != null)
                {
                    var token = HttpContext.Current.Session["token"];
                    if (token is string)
                    {
                        return token as string;
                    }
                }
                return null;
            });



            var storyProvider = new Func<IStoryProvider>(() => new CharcoalStoryProvider());
            var userRepo = new Func<IUserRepository>(() => new UserRepository());
            ObjectFactory.Initialize(context =>
            {
                context.For<IAccountProvider>().Use(() => new CharcoalAccountProvider(userRepo()));
                context.For<IProjectProvider>().Use(() => new CharcoalProjectProvider(tokenRetrieval()));
                context.For<IStoryProvider>().Use(storyProvider);
                context.For<IAnalyticsProvider>().Use(() => new AnalyticsProvider(storyProvider()));
                context.Scan(ias =>
                {
                    ias.TheCallingAssembly();
                    ias.AddAllTypesOf<Controller>();
                });
            });

            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());
		}
	}
}
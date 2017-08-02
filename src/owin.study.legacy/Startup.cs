using Owin.Study.Legacy.Formatters;
using System;
using System.Collections.Generic;
using System.Web.Http;

[assembly: Microsoft.Owin.OwinStartup(typeof(Owin.Study.Legacy.Startup))]

namespace Owin.Study.Legacy
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.Use(new Func<object, object>(
            //   x => new Middleware(new List<Route>
            //   {
            // new Route("Demo", typeof (DemoController))
            //   })
            //));
            //app.UseWelcomePage();
            var config = new System.Web.Http.HttpConfiguration();
            config.MapHttpAttributeRoutes();
            //config.Formatters.Remove(config.Formatters.JsonFormatter);
            config.Formatters.Insert(0, new PersonMediaTypeJsonFormatter());
    //        config.Routes.MapHttpRoute("defaultApi",
    //routeTemplate: "api/{controller}/{category}/{id}",
    //defaults: new { category = "all", id = RouteParameter.Optional });
            app.UseWebApi(config);
        }
    }
}

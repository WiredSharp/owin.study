using System;
using System.Collections.Generic;

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
            app.UseWelcomePage();
            app.Run(context => context.Response.WriteAsync("I finally did it !!"));
        }
    }
}

using System.Web.Http;
using Microsoft.Owin.Logging;
using Serilog;

[assembly: Microsoft.Owin.OwinStartup(typeof(Owin.Study.Legacy.Startup))]

namespace Owin.Study.Legacy
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Serilog.ILogger logger = new LoggerConfiguration()
                                        .Enrich.FromLogContext()
                                        .WriteTo.ColoredConsole()
                                        .CreateLogger();
            app.SetLoggerFactory(new SeriLoggerFactory(logger));
            app.Use<SerilogRequestContext>(logger);
            app.Use<LoggingMiddleware>(app.CreateLogger<LoggingMiddleware>());
            var config = new HttpConfiguration();
            config.EnableSystemDiagnosticsTracing();
            config.MapHttpAttributeRoutes();
            app.UseWebApi(config);
            logger.Information("startup completed");
        }
    }
}

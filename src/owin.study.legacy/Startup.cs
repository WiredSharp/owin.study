using System.Web.Http;
using Microsoft.Owin.Logging;
using Serilog;
using Owin.Study.Legacy.Logging;
using SimpleInjector.Integration.WebApi;

[assembly: Microsoft.Owin.OwinStartup(typeof(Owin.Study.Legacy.Startup))]

namespace Owin.Study.Legacy
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = new SimpleInjector.Container();
            var logRepository = new LogRepository();
            container.RegisterSingleton<ILogRepository>(logRepository);
            Serilog.ILogger logger = new LoggerConfiguration()
                                        .Enrich.FromLogContext()
                                        .WriteTo.ColoredConsole()
                                        .WriteTo.LogWindow(logRepository)
                                        .CreateLogger();
            app.SetLoggerFactory(new SeriLoggerFactory(logger));
            app.Use<SerilogRequestContext>(logger);
            app.Use<LoggingMiddleware>(app.CreateLogger<LoggingMiddleware>());
            var config = new HttpConfiguration()
            {
                DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container)
                ,IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always
            };
            config.EnableSystemDiagnosticsTracing();
            config.MapHttpAttributeRoutes();
            app.UseWebApi(config);
            logger.Information("startup completed");
        }
    }
}

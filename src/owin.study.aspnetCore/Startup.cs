using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace owin.study.aspnetCore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug(LogLevel.Debug);
            app.UseMiddleware<MyMiddleware>("titi");
            app.UseMvc();
        }
    }

    internal class MyMiddleware
    {
        private readonly string _name;
        private readonly RequestDelegate _next;
        private readonly ILogger<MyMiddleware> _logger;

        public MyMiddleware(RequestDelegate next, ILogger<MyMiddleware> logger, string name)
        {
            _logger = logger;
            _next = next;
            _name = name;
            if (_next == null)
            {
                _logger.LogDebug($"no next middleware");
            }
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.RequestAborted.IsCancellationRequested)
            {
                _logger.LogDebug($"request #{context.TraceIdentifier} cancelled");
                return;
            }
            _logger.LogDebug($"request #{context.TraceIdentifier} started...");
            if (_next != null)
            {
                await _next(context);
            }
            _logger.LogDebug($"...request #{context.TraceIdentifier} completed");
        }
    }
}

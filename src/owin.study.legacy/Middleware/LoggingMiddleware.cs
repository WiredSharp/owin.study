using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Logging;
using System.Net;

namespace Owin.Study.Legacy
{
    internal class LoggingMiddleware : OwinMiddleware
    {
        private readonly ILogger _logger;

        public LoggingMiddleware(OwinMiddleware next, ILogger logger) : base(next)
        {
            if (logger == null) throw new ArgumentNullException(nameof(logger));
            _logger = logger;
        }

        public async override Task Invoke(IOwinContext context)
        {
            _logger.WriteInformation($"request: {context.Request.Uri}");
            try
            {
                await Next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.WriteError("request processing failure", ex);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.Body.Close();
            }
        }
    }
}
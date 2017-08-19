using Microsoft.Owin;
using Serilog;
using Serilog.Context;
using System;
using System.Threading.Tasks;

namespace Owin.Study.Legacy
{
    internal class SerilogRequestContext : OwinMiddleware
    {
        private readonly ILogger _logger;

        public SerilogRequestContext(OwinMiddleware next, ILogger logger) : base(next)
        {
            if (logger == null) throw new ArgumentNullException(nameof(logger));
            _logger = logger;
        }

        public async override Task Invoke(IOwinContext context)
        {
            using (LogContext.PushProperty("RequestId", Guid.NewGuid()))
            {
                await Next.Invoke(context);
            }
        }
    }
}
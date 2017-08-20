using Serilog;
using System;
using System.Web.Http;

namespace Owin.Study.Legacy.Logging
{
    [RoutePrefix("logs")]
    public class LogController : ApiController
    {
        private readonly ILogRepository _logRepository;

        private ILogger Logger = Log.ForContext<LogController>();

        public LogController(ILogRepository logRepository)
        {
            if (logRepository == null) throw new ArgumentNullException(nameof(logRepository));
            _logRepository = logRepository;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetLogs()
        {
            LogEntry[] logs = _logRepository.GetLastLogs();
            return Ok(logs);
            //return new HtmlResult(System.Net.HttpStatusCode.OK,"<html><head/><body><h1>Hello World !!</h1></body></<html>");
        }

        //[Route("")]
        //public ActionResult GetLogs()
        //{
        //    LogEntry[] logs = _logRepository.GetLastLogs();
        //    return View(logs);
        //}
    }
}

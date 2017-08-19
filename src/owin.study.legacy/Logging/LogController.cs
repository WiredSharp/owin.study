using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Owin.Study.Legacy.Logging
{
    [RoutePrefix("logs")]
    public class LogController : ApiController
    {
        private readonly ILogRepository _logRepository;

        //public LogController(ILogRepository logRepository)
        //{
        //    if (logRepository == null) throw new ArgumentNullException(nameof(logRepository));
        //    _logRepository = logRepository;
        //}

        [HttpGet]
        [Route("")]
        public string GetLogs()
        {
            return "hello world";
        }

        //[Route("")]
        //public ActionResult GetLogs()
        //{
        //    LogEntry[] logs = _logRepository.GetLastLogs();
        //    return View(logs);
        //}
    }
}

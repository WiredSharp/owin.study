using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace owin.study.aspnetCore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [Route("api/v1/[controller]")]
    public class ValuesController : Controller
    {
        private readonly ILogger<ValuesController> _logger;

        public ValuesController(ILogger<ValuesController> logger)
        {
            if (logger == null) throw new ArgumentNullException(nameof(logger));
            _logger = logger;
        }

        // GET: api/values
        [HttpGet]
        [Route("all")]
        [Route("")]
        public ActionResult GetAll()
        {
            _logger.LogDebug("getAll");
            return Ok(new String[] { "value1", "value2" });
        }

        // GET api/values/5
        [HttpGet]
        [Route("{id}")]
        [Route("byId/{id}")]
        public ActionResult Get(int id)
        {
            _logger.LogDebug("get");
            return Ok($"value{id}");
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
            _logger.LogDebug("post");
            // For more information on protecting this API from Cross Site Request Forgery (CSRF) attacks, see https://go.microsoft.com/fwlink/?LinkID=717803
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            _logger.LogDebug("put");
            // For more information on protecting this API from Cross Site Request Forgery (CSRF) attacks, see https://go.microsoft.com/fwlink/?LinkID=717803
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _logger.LogDebug("delete");
            // For more information on protecting this API from Cross Site Request Forgery (CSRF) attacks, see https://go.microsoft.com/fwlink/?LinkID=717803
        }

        [HttpHead]
        public void Head()
        {
            _logger.LogDebug("head");
        }

        [HttpOptions]
        public void Options()
        {
            _logger.LogDebug("options");
        }
    }
}

using System;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Owin.Study.Legacy.Logging
{
    internal class HtmlResult : IHttpActionResult
    {
        private HttpStatusCode _statusCode;
        private string _content;

        public HtmlResult(HttpStatusCode statusCode, string content)
        {
            _statusCode = statusCode;
            _content = content;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var response = new HttpResponseMessage(_statusCode)
            {
                Content = new StringContent(_content, Encoding.UTF8, MediaTypeNames.Text.Html)
            };
            return Task.FromResult(response);
        }
    }
}
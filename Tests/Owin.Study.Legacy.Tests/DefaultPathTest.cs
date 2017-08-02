using Microsoft.Owin.Testing;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Owin.Study.Legacy.Tests
{
    [TestFixture]
    public class DefaultPathTest
    {
        [Test]
        public async Task default_route_returns_success_status_code()
        {
            using (var server = TestServer.Create(app => new Startup().Configuration(app)))
            {
                HttpResponseMessage response = await server.HttpClient.GetAsync("/");
                // TODO: Validate response
                Assert.IsTrue(response.IsSuccessStatusCode, $"HTTP failure status code: {response.StatusCode} ({response.ReasonPhrase})");
            }
        }

        [Test]
        public async Task default_route_returns_expected_content()
        {
            using (var server = TestServer.Create(app => new Startup().Configuration(app)))
            {
                HttpResponseMessage response = await server.HttpClient.GetAsync("/");
                // TODO: Validate response
                string content = await response.Content.ReadAsStringAsync();
                Assert.AreEqual("I finally did it !!", content, "unexpected response content for '/'");
            }
        }

        [Test]
        public async Task persons_api_get_all_returns_expected_content()
        {
            using (var server = TestServer.Create(app => new Startup().Configuration(app)))
            {
                HttpResponseMessage response = await server.HttpClient.GetAsync("/api/persons");
                // TODO: Validate response
                string content = await response.Content.ReadAsStringAsync();
                Assert.AreEqual("I finally did it !!", content, "unexpected response content for '/'");
            }
        }
    }
}

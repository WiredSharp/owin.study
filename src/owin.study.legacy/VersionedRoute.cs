using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Routing;

namespace Owin.Study.Legacy
{
    /// <summary>
    /// Provides an attribute route that's restricted to a specific version of the api.
    /// </summary>
    internal class VersionedRoute : RouteFactoryAttribute
    {
        public VersionedRoute(string template, int allowedVersion)
            : base(template)
        {
            AllowedVersion = allowedVersion;
        }

        public int AllowedVersion
        {
            get;
            private set;
        }

        public override IDictionary<string, object> Constraints
        {
            get
            {
                return new HttpRouteValueDictionary
                {
                    { "version", new VersionConstraint(AllowedVersion) }
                };
            }
        }
    }
}

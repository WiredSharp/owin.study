using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Owin.Study.Legacy.Controllers
{
    /// <summary>
    /// A Customer controller for version 2 of the api.
    /// </summary>
    [VersionedRoute("api/Customer", 2)]
    public class CustomerVersion2Controller : ApiController
    {
        private static readonly CustomerVersion2[] _customers = new CustomerVersion2[]
        {
            new CustomerVersion2()
            {
                Id = 1,
                Name = "Contoso LLC",
                DateCreated = new DateTime(1970, 4, 1),
                Address  = new AddressVersion2()
                {
                    StreetAddress = "1 Microsoft Way",
                    City = "Redmond",
                    State = "WA",
                    ZipCode = 98053
                }
            }
        };

        public IHttpActionResult Get()
        {
            return Json(_customers);
        }

        public IHttpActionResult Get(int id)
        {
            var customer = _customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            else
            {
                return Json(customer);
            }
        }
    }
}

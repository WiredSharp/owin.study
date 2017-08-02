using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Owin.Study.Legacy.Persons
{
    //[Route("api/v1/persons")]
    [RoutePrefix("api/persons")]
    public class PersonApiController : ApiController
    {
        private readonly IRepository<Person> _persons;

        public PersonApiController()
            : this(Default())
        {
        }

        private static IRepository<Person> Default()
        {
            var repository = new DictionaryRepository<Person>();
            repository.AddRangeAsync(Enumerable.Range(1, 10)
                .Select(i => new Person() { Firstname = $"Bob {i:000}", LastName = $"Doe {i:000}" })).Wait(TimeSpan.FromSeconds(1));
            return repository;
        }

        public PersonApiController(IRepository<Person> persons)
        {
            if (persons == null) throw new ArgumentNullException(nameof(persons));
            _persons = persons;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAll()
        {
            var persons = await _persons.GetAllAsync();
            return Ok(persons);
        }
    }
}

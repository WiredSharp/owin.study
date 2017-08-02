using NUnit.Framework;
using Owin.Study.Legacy.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Owin.Study.Legacy.Tests
{
    [TestFixture]
    public class DictionaryRepositoryTest
    {
        [Test]
        public async Task i_can_add_entities()
        {
            var repository = new DictionaryRepository<Person>();
            await repository.AddRangeAsync(Enumerable.Range(1, 100).Select(i => new Person() { Firstname = $"Bob {i:000}", LastName = $"Doe {i:000}" }));
        }

        [Test]
        public async Task i_can_retrieve_added_entities()
        {
            var repository = new DictionaryRepository<Person>();
            Person[] persons = Enumerable.Range(1, 100).Select(i => new Person() { Firstname = $"Bob {i:000}", LastName = $"Doe {i:000}" }).ToArray();
            await repository.AddRangeAsync(persons);
            Assert.AreEqual(persons.Length, repository.GetAllAsync().Result.Count(), "unexpected count returned");

        }
    }
}

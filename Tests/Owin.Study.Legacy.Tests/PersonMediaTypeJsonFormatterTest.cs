using Newtonsoft.Json;
using NUnit.Framework;
using Owin.Study.Legacy.Formatters;
using Owin.Study.Legacy.Persons;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Owin.Study.Legacy.Tests
{
    [TestFixture]
    public class PersonMediaTypeJsonFormatterTest
    {
        [Test]
        public void properties_are_filtered_on_serialization()
        {
            Person person = new Person() { Firstname = "John", LastName = "Doe" };
            Assert.That(Serialize(false, person), Does.Contain("LastName"), "LastName property should be serialized by default");
            Assert.That(Serialize(true, person), Does.Not.Contain("LastName"), "LastName property should not be serialized");
        }

        private static string Serialize(bool useContract, Person person)
        {
            JsonSerializer serializer = new JsonSerializer();
            if (useContract)
            {
                serializer.ContractResolver = ShouldSerializeContractResolver.Instance;
            }
            var sb = new StringBuilder();
            using (var writer = new JsonTextWriter(new StringWriter(sb)))
                serializer.Serialize(writer, person);
            return sb.ToString();
        }
    }
}

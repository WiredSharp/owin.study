using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Owin.Study.Legacy.Persons
{
    public class PersonController
    {
        public IView Index()
        {
            return new View("Index", new Person() { Firstname = "Max" });
        }

        public IView All()
        {
            var result = new List<Person>();

            for (var x = 0; x < 150; x++)
            {
                result.Add(new Person() { Firstname = "Max " + x });
            }

            return new View("All", result);
        }
    }
}

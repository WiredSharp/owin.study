using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Owin.Study.Legacy
{
    class Program
    {
        public static void Main(string[] args)
        {
            string uri = "http://localhost:7990";

            try
            {
                using (WebApp.Start<Startup>(uri))
                {
                    Console.WriteLine("Web server on {0} starting.", uri);
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.WriteLine("Web server on {0} stopping.", uri);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}

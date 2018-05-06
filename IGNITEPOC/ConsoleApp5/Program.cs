using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apache.Ignite.Core;
using Apache.Ignite.Core.Cache;

namespace ConsoleApp5
{
    class Program
    {
        static void Main(string[] args)
        {
            var ignite=Ignition.Start();        
            ICache<Int32, String> cache = ignite.GetOrCreateCache<int, string>("myCache");

            // Store keys in cache (values will end up on different cache nodes).
            for (int i = 0; i < 10; i++)
                cache.Put(0, "Hello");

            for (int i = 0; i < 10; i++)
               Console.WriteLine("Got [key=" + i + ", val=" + cache.Get(0) + ']');

            Console.ReadLine();
        }
    }
}

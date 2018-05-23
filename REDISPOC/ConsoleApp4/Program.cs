using System;
using System.Collections.Generic;
using System.Collections;
using ServiceStack.Redis;
using StackExchange.Redis;
using System.Diagnostics;
using GemStone.GemFire.Cache.Generic;
namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            TestHashTable();
            TestRedis();
            TestGemfire();
        }

        private static void TestRedis()
        {
            Stopwatch serializationStopWatch;
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
            IDatabase db = redis.GetDatabase();
            string value = GenerateString();
            List<string> collection = new List<string>();
            for (int i = 0; i < 100000; i++)
            {
                collection.Add(i.ToString());
            }
            serializationStopWatch = new Stopwatch();
            for (int i = 0; i < 100000; i++)
            {
                string keyValue = collection[i];
                serializationStopWatch.Start();
                db.StringSet(keyValue, value);
                serializationStopWatch.Stop();

            }
            Console.WriteLine(serializationStopWatch.ElapsedMilliseconds);
            serializationStopWatch = new Stopwatch();
            serializationStopWatch.Start();
            string returnValue = db.StringGet(collection[99999]);
            serializationStopWatch.Stop();
            Console.WriteLine(serializationStopWatch.ElapsedMilliseconds);
            Console.WriteLine("REDIS We got value so test is true" + returnValue);
            Console.WriteLine("Enter a key");
            Console.ReadLine();
        }

        private static void TestGemfire()
        {
            CacheFactory cacheFactory = CacheFactory.CreateCacheFactory();
            Cache cache = cacheFactory.Create();
            Console.WriteLine("Created the GemFire Cache");
            RegionFactory regionFactory = cache.CreateRegionFactory(RegionShortcut.CACHING_PROXY);
            IRegion<int, string> region = regionFactory.Create<int, string>("exampleRegion");
            Console.WriteLine("Created the Region with generics support programmatically.");

            Stopwatch serializationStopWatch;

            string value = GenerateString();
            Console.WriteLine("Lenght of String " + value.Length );
            serializationStopWatch = new Stopwatch();
            for (int i = 0; i < 100000; i++)
            {

                serializationStopWatch.Start();
                region[i] = value+i;
                serializationStopWatch.Stop();

            }            
            Console.WriteLine(serializationStopWatch.ElapsedMilliseconds);
            serializationStopWatch = new Stopwatch();
            serializationStopWatch.Start();
            string returnValue = region[99999];
            serializationStopWatch.Stop();          
            Console.WriteLine(serializationStopWatch.ElapsedMilliseconds);
            Console.WriteLine("We got value so test is true " + returnValue); 
            Console.WriteLine("Lenght of String " + value.Length + "and" + " string is " + value);
            Console.WriteLine("Enter a key");
            Console.ReadLine();
        }

        private static void TestHashTable()
        {
            Stopwatch serializationStopWatch;
            Hashtable hashTable = new Hashtable();
            string value = "abcdefg";
            List<string> collection = new List<string>();
            for (int i = 0; i < 100000; i++)
            {
                collection.Add(i.ToString());
            }
            serializationStopWatch = new Stopwatch();
            for (int i = 0; i < 100000; i++)
            {

                serializationStopWatch.Start();
                hashTable.Add(collection[i], value);
                serializationStopWatch.Stop();

            }
            Console.WriteLine(serializationStopWatch.ElapsedMilliseconds);
            serializationStopWatch = new Stopwatch();
            serializationStopWatch.Start();
            var returnValue = hashTable[collection[500]];
            serializationStopWatch.Stop();
            Console.WriteLine(serializationStopWatch.ElapsedMilliseconds);
            Console.WriteLine("We got value so test is true" + returnValue); 
            
           
        }


        private static string GenerateString()
        {
            string value = string.Empty;
            for (int i = 0; i < 200; i++)
            {
                value = value + i.ToString();
            }

            return value;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections;
using ServiceStack.Redis;
using StackExchange.Redis;
using System.Diagnostics;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            TestHashTable();
            TestRedis();
        }

        private static void TestRedis()
        {
            Stopwatch serializationStopWatch;
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
            IDatabase db = redis.GetDatabase();
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
                db.StringSet(collection[i], value);
                serializationStopWatch.Stop();

            }
            Console.WriteLine(serializationStopWatch.ElapsedMilliseconds);
            serializationStopWatch = new Stopwatch();
            serializationStopWatch.Start();
            string returnValue = db.StringGet(collection[500]);
            serializationStopWatch.Stop();
            Console.WriteLine(serializationStopWatch.ElapsedMilliseconds);
            Console.WriteLine("We got value so test is true" + returnValue); // writes: "abcdefg
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
            Console.WriteLine("We got value so test is true" + returnValue); // writes: "abcdefg
            //Console.ReadLine();
           
        }

        
    }
}

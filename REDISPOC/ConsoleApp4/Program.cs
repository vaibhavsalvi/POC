using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using ServiceStack.Redis;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            string host = "localhost";

            string key = "IDG";

            // Store data in the cache

            bool success = Save(host, key, "Hello World!");

            // Retrieve data from the cache using the key

            Console.WriteLine("Data retrieved from Redis Cache: " + Get(host, key));

            Console.Read();
        }
        private static bool Save(string host, string key, string value)

        {

            bool isSuccess = false;

            using (RedisClient redisClient = new RedisClient(host))

            {

                if (redisClient.Get<string>(key) == null)

                {

                    isSuccess = redisClient.Set(key, value);

                }

            }

            return isSuccess;

        }

        private static string Get(string host, string key)

        {

            using (RedisClient redisClient = new RedisClient(host))

            {

                return redisClient.Get<string>(key);

            }

        }
    }
}

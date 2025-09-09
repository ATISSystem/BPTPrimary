using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayanehClassLibrary.TerminalsWebService;
using R2Core.CachHelper;
using R2Core.Caching;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace UnitTestR2Core
{
    [TestClass]
    public class UnitTest6
    {
        [TestMethod]
        public void RedisTester()
        {
            var redis = RedisConnectorHelper.Connection; 
            var db = redis.GetDatabase(0);

            var server = RedisConnectorHelper.GetServer;
            var keys = server.Keys();

            foreach (var key in keys)
            {
                var value = db.StringGetAsync(key);
                Console.WriteLine($"Key: {key}, Value: {value}");
            }

            var keysx = server.Keys(database :0, pageSize: 1000);

            foreach (var key in keysx)
            {
                var value = db.StringGetAsync(key);
                Console.WriteLine($"Key: {key}, Value: {value}");
            }

        }

    }
}

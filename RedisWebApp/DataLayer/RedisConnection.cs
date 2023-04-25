using StackExchange.Redis;
using System.Collections.Concurrent;

namespace RedisWebApp.DataLayer
{
    public class RedisConnection
    {
        private static readonly ConcurrentDictionary<string, Lazy<ConnectionMultiplexer>> singleConnectionPool = new();

        public IDatabase Connect(string setting = "localhost")
        {
            var connMultiplexer = singleConnectionPool.GetOrAdd(setting,
                new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(setting)));

            return connMultiplexer.Value.GetDatabase();
        }

        public IDatabase GetDatabase(string setting = "localhost")
        {
            if (singleConnectionPool.TryGetValue(setting, out var connMultiplexer))
            {
                return connMultiplexer.Value.GetDatabase();
            }

            return default;
        }
    }
}

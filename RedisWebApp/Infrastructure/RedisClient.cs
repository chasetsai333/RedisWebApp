using StackExchange.Redis;

namespace RedisWebApp.Infrastructure
{
    public class RedisClient
    {
        private static readonly Lazy<ConnectionMultiplexer> s_connectionLazy;
        private static string _setting;

        private static ConnectionMultiplexer Instance => s_connectionLazy.Value;

        public IDatabase Database => Instance.GetDatabase();

        static RedisClient()
        {
            s_connectionLazy = new Lazy<ConnectionMultiplexer>(() =>
            {
                if (string.IsNullOrWhiteSpace(_setting))
                {
                    return ConnectionMultiplexer.Connect("localhost");
                }

                return ConnectionMultiplexer.Connect(_setting);
            });
        }

        public static void Init(string setting)
        {
            _setting = setting;
        }
    }
}

using StackExchange.Redis;

namespace SecilStore.Library
{
    public class RedisHelper
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _db;

        public RedisHelper(string connectionString)
        {
            // Connection string yerine ConfigurationOptions kullan
            var config = new ConfigurationOptions
            {
                EndPoints = { "redis-19114.c55.eu-central-1-1.ec2.redns.redis-cloud.com:19114" },
                User = "default",
                Password = "4Ek8OndTAJxfK8wauUad7mYKc7bkHx8G",
                Ssl = true,
                AbortOnConnectFail = false
            };

            _redis = ConnectionMultiplexer.Connect(config);
            _db = _redis.GetDatabase();
        }

        public void SetConfig(string appName, string key, string value)
        {
            _db.HashSet(appName, key, value);
        }

        public string? GetConfig(string appName, string key)
        {
            return _db.HashGet(appName, key);
        }

        public Dictionary<string, string> GetAllConfigs(string appName)
        {
            var entries = _db.HashGetAll(appName);
            return entries.ToDictionary(x => x.Name.ToString(), x => x.Value.ToString());
        }

        public void Test()
        {
            _db.StringSet("deneme", "selam");
            var val = _db.StringGet("deneme");
            Console.WriteLine($"Redis test okuma: {val}");
        }
    }
}

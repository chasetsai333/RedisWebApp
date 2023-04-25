using RedisWebApp.DataLayer;
using RedisWebApp.Infrastructure;
using StackExchange.Redis;

namespace TestRedisProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SetDTO()
        {
            var connection = new RedisConnection();

            // var database = connection.Connect("localhost:6379");
            var config = ConfigurationOptions.Parse("127.0.0.1:6379");
            var database = connection.Connect(config.ToString());
            var model = new Model
            {
                Name = "Chase",
                Age = 29
            };

            database.Set("dto", model);
            var actual = database.Get<Model>("dto");
            Assert.AreEqual(model, actual);
        }

        [Serializable]
        record Model
        {
            public string Name { get; set; }

            public int Age { get; set; }
        }
    }
}
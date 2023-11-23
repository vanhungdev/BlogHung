using BlogHung.Infrastructure.Database;
using BlogHung.Infrastructure.Hosting.Middlewares;
using BlogHung.Infrastructure.Kafka;
using BlogHung.Infrastructure.Kafka.Producer;
using BlogHung.Infrastructure.Models;
using BlogHung.Infrastructure.Utilities;
using BlogHung.Models;
using Confluent.Kafka;
using EasyNetQ;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MongoDB.Driver;
using Nest;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BlogHung.Controllers
{
    [ServiceFilter(typeof(LogModelDataAttribute))]
    public class HomeController : Controller
    {
        private readonly Infrastructure.Database.IQuery _query;
        private readonly IMongoDbContext _mongoDbContext;
        private readonly IKafkaProducer _messageBroker;
        public HomeController(IMongoDbContext mongoDbContext, Infrastructure.Database.IQuery query, IKafkaProducer messageBroker)
        {
            _mongoDbContext = mongoDbContext;
            _query = query;
            _messageBroker = messageBroker;
        }

        public async Task<IActionResult> IndexAsync(int id = 1)
        {

            var config1 = new ProducerConfig
            {
                BootstrapServers = "34.171.40.194:9092"
            };

            // Tạo list các topic cần ghi
            var topics = new List<string>() { "events1", "events2", "events3", "events4", "events5" };

            Parallel.For(0, topics.Count, i =>
            {
                var topic = topics[i];
                // Số lượng message cố định 
                var numMessages = 200;

                for (int j = 1; j <= numMessages; j++)
                {
                    var message = new Message<Null, string>
                    {
                        Value = $"message {j} for {topic}"
                    };

                    // Gọi hàm produce message theo từng topic
                    _messageBroker.ProducePushMessage(topic, config1, message, message.Value);
                }
            });

            /*   LoggingHelper.SetProperty("ResponseData", "123!");*/
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PrivacyAsync(Users users)
        {

            //SQL
            var checkExist = await _query.QueryAsync<object>(SqlConnectionString.DatabaseRead,
                "SELECT top 10 * FROM SalePlatform..SPF_Account(nolock)");

            //REDIS
            var key = "key";
            var dataRedis = await RedisConnection.GetDatabase().StringGetAsync(key);
            if (string.IsNullOrWhiteSpace(dataRedis))
            {
                _ = await RedisConnection.GetDatabase().StringSetAsync(key, "DepTrai");
            }


            // MONGODB
            var filter = Builders<dynamic>.Filter.Empty;
            var ops = new List<FilterDefinition<dynamic>>();
          /*  ops.Add(Builders<dynamic>.Filter.Where(x => x.ExtractText.Equals("123")));
            filter = Builders<dynamic>.Filter.And(ops);*/

            var condition = _mongoDbContext.GetCollectionRead<dynamic>("saleplatform_policy_bot_flow");
            var result = await condition.FindAsync(filter);
            var ddss3 = result.ToList();


            var s = Infrastructure.Utilities.Helper.Settings.ConnectionStringSettings.SqlServerConnect;

            var user = new Users();
            user.Id = 1;
            user.Name = "Hung";
            return View(user);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
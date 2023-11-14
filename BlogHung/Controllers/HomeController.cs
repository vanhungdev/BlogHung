using BlogHung.Infrastructure.Database;
using BlogHung.Infrastructure.Hosting.Middlewares;
using BlogHung.Models;
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
        public HomeController(IMongoDbContext mongoDbContext, Infrastructure.Database.IQuery query)
        {
            _mongoDbContext = mongoDbContext;
            _query = query;
        }

        public IActionResult Index(int id = 1)
        {
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
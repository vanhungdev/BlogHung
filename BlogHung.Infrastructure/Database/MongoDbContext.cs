using BlogHung.Infrastructure.Utilities;
using MongoDB.Driver;

namespace BlogHung.Infrastructure.Database
{
    /// <summary>
    /// MongoDbContext
    /// </summary>
    public class MongoDbContext : IMongoDbContext
    {
        private const string DbName = "saleplatform_db";

        private IMongoDatabase _dbRead { get; set; }
        private static MongoClient _mongoClientRead { get; set; }

        private IMongoDatabase _dbWrite { get; set; }
        private static MongoClient _mongoClientWrite { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public MongoDbContext()
        {
            _mongoClientRead = new MongoClient(CoreUtility.CoreSettings.MongoSettings.ServerRead);
            _dbRead = _mongoClientRead.GetDatabase(DbName);

            _mongoClientWrite = new MongoClient(CoreUtility.CoreSettings.MongoSettings.ServerWrite);
            _dbWrite = _mongoClientWrite.GetDatabase(DbName);
        }

        /// <summary>
        /// Read
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public IMongoCollection<T> GetCollectionRead<T>(string name)
        {
            return _dbRead.GetCollection<T>(name);
        }

        /// <summary>
        /// Write
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public IMongoCollection<T> GetCollectionWrite<T>(string name)
        {
            return _dbWrite.GetCollection<T>(name);
        }
    }
}

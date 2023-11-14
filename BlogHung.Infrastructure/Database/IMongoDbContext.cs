using MongoDB.Driver;

namespace BlogHung.Infrastructure.Database
{
    public interface IMongoDbContext
    {
        /// <summary>
        /// Dùng để đọc
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        IMongoCollection<T> GetCollectionRead<T>(string name);

        /// <summary>
        /// Dùng để ghi
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        IMongoCollection<T> GetCollectionWrite<T>(string name);
    }
}

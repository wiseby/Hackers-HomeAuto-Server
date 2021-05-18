using MongoDB.Driver;

namespace DataAccess 
{
    public interface IMongoConnection
    {
        public MongoClient GetConnection();
    }
}
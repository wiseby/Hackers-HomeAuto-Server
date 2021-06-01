using MongoDB.Driver;

namespace DataAccess
{
    public class MongoConnection : IMongoConnection
    {
        private readonly MongoClient mongoClient;
        public MongoConnection(string connectionString)
        {
            this.mongoClient = new MongoClient(connectionString);
        }

        public MongoClient GetConnection()
        {
            return this.mongoClient;
        }
    }
}
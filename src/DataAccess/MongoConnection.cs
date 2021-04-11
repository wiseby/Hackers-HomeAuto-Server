using MongoDB.Driver;

namespace DataAccess 
{
    public class MongoConnection
    {
        public readonly MongoClient MongoClient;
        public MongoConnection(string connectionString)
        {
            this.MongoClient = new MongoClient(connectionString);
        }
    }
}
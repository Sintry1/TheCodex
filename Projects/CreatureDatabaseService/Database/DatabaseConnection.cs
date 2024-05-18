using DotNetEnv;
using MongoDB.Driver;

namespace CreatureDatabaseService
{
    public class MongoDBConnection : IDisposable
    {
        private MongoClient client;
        private IMongoDatabase database;
        private string connectionString;
        private string databaseName;

        // Constructor for MongoDBConnection
        public MongoDBConnection()
        {
            Env.Load();
            string username = Env.GetString("DB_USER");
            string password = Env.GetString("DB_PASS");
            string dbHost = Env.GetString("DB_HOST");
            string dbName = Env.GetString("DB_NAME");
            SetConnectionCredentials(username, password, dbHost, dbName);
        }

        // Set the connection credentials dynamically
        public void SetConnectionCredentials(string username, string password, string dbHost, string dbName)
        {
            connectionString = $"mongodb://{username}:{password}@{dbHost}/?retryWrites=true&w=majority&{dbName}";
            client = new MongoClient(connectionString);
            database = client.GetDatabase(databaseName);
        }

        public IMongoDatabase GetDatabase()
        {
            return database;
        }

        public void Dispose()
        {
            
        }
    }
}

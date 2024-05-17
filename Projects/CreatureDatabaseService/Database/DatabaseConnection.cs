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
            string dbPort = Env.GetString("DB_PORT");
            SetConnectionCredentials(username, password, dbHost, dbPort);
        }

        // Set the connection credentials dynamically
        public void SetConnectionCredentials(string username, string password, string dbHost, string dbPort)
        {
            connectionString = $"mongodb://{username}:{password}@{dbHost}:{dbPort}";
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

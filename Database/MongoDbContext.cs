using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Portfolio.Configuration;
using Portfolio.Models;

namespace Portfolio.Database
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database = null;

        public MongoDbContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);

            var builder = Builders<User>.IndexKeys.Ascending(u => u.UserName);
            var options = new CreateIndexOptions { Unique = true };
            Users.Indexes.CreateOne(new CreateIndexModel<User>(builder, options));
        }

        public IMongoCollection<User> Users
        {
            get
            {
                return _database.GetCollection<User>("Users");
            }
        }
        public IMongoCollection<Project> Projects
        {
            get
            {
                return _database.GetCollection<Project>("Projects");
            }
        }
        public IMongoCollection<Skill> Skills
        {
            get
            {
                return _database.GetCollection<Skill>("Skills");
            }
        }
        public IMongoCollection<AboutMe> AboutMes
        {
            get
            {
                return _database.GetCollection<AboutMe>("AboutMes");
            }
        }
    }
}

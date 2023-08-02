using Portfolio.Database;
using Portfolio.Models;
using MongoDB.Driver;

namespace Portfolio.Services
{
    public class AboutMeService
    {
        private readonly MongoDbContext _dbContext;

        public AboutMeService(MongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AboutMe> GetAboutMeAsync(string language)
        {
            return await _dbContext.AboutMes.Find(a => a.Language == language).FirstOrDefaultAsync();
        }

        public async Task AddAboutMeAsync(AboutMe aboutMe)
        {
            await _dbContext.AboutMes.InsertOneAsync(aboutMe);
        }

        public async Task UpdateAboutMeAsync(AboutMe aboutMe)
        {
            var filter = Builders<AboutMe>.Filter.Eq(a => a.Id, aboutMe.Id);
            await _dbContext.AboutMes.ReplaceOneAsync(filter, aboutMe);
        }

        public async Task DeleteAboutMeAsync(string id)
        {
            var filter = Builders<AboutMe>.Filter.Eq(a => a.Id, id);
            await _dbContext.AboutMes.DeleteOneAsync(filter);
        }
    }
}

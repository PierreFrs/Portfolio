using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task AddAboutMeAsync(AboutMe aboutMe)
        {
            await _dbContext.AboutMes.InsertOneAsync(aboutMe);
        }

        // Implement methods to retrieve and update the AboutMe data here.
    }
}

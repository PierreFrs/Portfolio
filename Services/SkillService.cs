using System.Collections.Generic;
using System.Threading.Tasks;
using Portfolio.Database;
using Portfolio.Models;
using MongoDB.Driver;

namespace Portfolio.Services
{
    public class SkillService
    {
        private readonly MongoDbContext _dbContext;

        public SkillService(MongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddSkillAsync(Skill skill)
        {
            await _dbContext.Skills.InsertOneAsync(skill);
        }

        public async Task<List<Skill>> GetAllSkillsAsync()
        {
            return await _dbContext.Skills.Find(_ => true).ToListAsync();
        }

        // Implement update and delete methods for Skills here, similar to the ones in ProjectService.
    }
}

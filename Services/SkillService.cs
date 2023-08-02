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

        public async Task<Skill> GetSkillByIdAsync(string id)
        {
            return await _dbContext.Skills.Find(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Skill> DeleteSkillAsync(string id)
        {
            var filter = Builders<Skill>.Filter.Eq(s => s.Id, id);
            var deletedSkill = await _dbContext.Skills.FindOneAndDeleteAsync(filter);
            return deletedSkill;
        }
    }
}

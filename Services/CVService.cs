using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MongoDB.Driver;
using Portfolio.Database;
using Portfolio.Models;

namespace Portfolio.Services
{
    public class CVService
    {
        private readonly MongoDbContext _dbContext;
        public CVService(MongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddCVAsync(CV cv)
        {
            await _dbContext.CVs.InsertOneAsync(cv);
        }

        public async Task<CV> GetCVByIdAsync(string id)
        {
            return await _dbContext.CVs.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateCVAsync(string id, CV cv)
        {
            var filter = Builders<CV>.Filter.Eq(c => c.Id, id);
            var update = Builders<CV>.Update.Set(c => c.FilePath, cv.FilePath);

            await _dbContext.CVs.UpdateOneAsync(filter, update);
        }

        public async Task<CV> DeleteCVAsync(string id)
        {
            var filter = Builders<CV>.Filter.Eq(c => c.Id, id);
            return await _dbContext.CVs.FindOneAndDeleteAsync(filter);
        }
    }
}

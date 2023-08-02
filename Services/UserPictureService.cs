using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using Portfolio.Database;
using Portfolio.Models;

namespace Portfolio.Services
{
    public class UserPictureService
    {
        private readonly MongoDbContext _dbContext;
        public UserPictureService(MongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserPicture> GetUserPictureAsync()
        {
            // Here, we're assuming that there will be only one user picture document in the collection.
            // This might not be a valid assumption depending on your application's requirements.
            return await _dbContext.UserPictures.Find(_ => true).FirstOrDefaultAsync();
        }

        public async Task UpdateUserPictureAsync(string newUrl)
        {
            var filter = Builders<UserPicture>.Filter.Empty;
            var update = Builders<UserPicture>.Update.Set(p => p.Url, newUrl);

            await _dbContext.UserPictures.UpdateOneAsync(filter, update);
        }
    }
}

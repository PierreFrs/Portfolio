using System.Collections.Generic;
using System.Threading.Tasks;
using Portfolio.Database;
using Portfolio.Models;
using MongoDB.Driver;

namespace Portfolio.Services
{
    public class ProjectService
    {
        private readonly MongoDbContext _dbContext;

        public ProjectService(MongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddProjectAsync(Project project)
        {
            await _dbContext.Projects.InsertOneAsync(project);
        }

        public async Task<List<Project>> GetAllProjectsAsync()
        {
            return await _dbContext.Projects.Find(_ => true).ToListAsync();
        }

        public async Task<Project> GetProjectByIdAsync(string id)
        {
            return await _dbContext.Projects.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Project> UpdateProjectAsync(string id, Project project)
        {
            var filter = Builders<Project>.Filter.Eq(p => p.Id, id);
            var update = Builders<Project>.Update
                .Set(p => p.Title, project.Title)
                .Set(p => p.Description, project.Description)
                .Set(p => p.ImageUrl, project.ImageUrl)
                .Set(p => p.ProjectUrl, project.ProjectUrl)
                .Set(p => p.Technologies, project.Technologies);

            await _dbContext.Projects.UpdateOneAsync(filter, update);

            // Return the updated project after the update operation
            return await GetProjectByIdAsync(id);
        }


        public async Task<Project> DeleteProjectAsync(string id)
        {
            var filter = Builders<Project>.Filter.Eq(p => p.Id, id);
            var deletedProject = await _dbContext.Projects.FindOneAndDeleteAsync(filter);
            return deletedProject;
        }
    }
}

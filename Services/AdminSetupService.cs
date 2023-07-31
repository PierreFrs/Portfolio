using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;
using Portfolio.Database;
using Portfolio.Models;
using Microsoft.Extensions.Options;
using Portfolio.Configuration;

namespace Portfolio.Services
{
    public class AdminSetupService
    {
        private readonly MongoDbContext _dbContext;
        private readonly Settings _settings;
        private readonly TokenService _tokenService;

        public AdminSetupService(MongoDbContext dbContext, IOptions<Settings> settings, TokenService tokenService)
        {
            _dbContext = dbContext;
            _settings = settings.Value;
            _tokenService = tokenService;
        }

        public async Task SetupAdminUser()
        {
            bool shouldSetupAdmin = bool.Parse(_settings.ShouldSetupAdminUser);
            if (!shouldSetupAdmin)
            {
                return;
            }
            
            // Define your user credentials
            var userName = _settings.AdminUserName;
            var password = _settings.AdminPassword;

            // Check if a user with the given username already exists
            var existingUser = _dbContext.Users.Find(u => u.UserName == userName).FirstOrDefault();
            if(existingUser != null)
            {
                Console.WriteLine("Admin user already exists. No action was performed.");
                return;
            }

            // If not, create a new user
            var user = new User { UserName = userName };

            // Hash the password
            var passwordHasher = new PasswordHasher<User>();
            user.PasswordHash = passwordHasher.HashPassword(user, password);

            // Store the user in the MongoDB database
            await _dbContext.Users.InsertOneAsync(user);

            Console.WriteLine("Admin user was created successfully.");
        }

        public async Task<string> Login(string userName, string password)
        {
            var user = await _dbContext.Users.Find(u => u.UserName == userName).FirstOrDefaultAsync();
            if (user == null)
                return null;

            var passwordHasher = new PasswordHasher<User>();
            var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (passwordVerificationResult == PasswordVerificationResult.Failed)
                return null;

            return _tokenService.GenerateToken(user);
        }
    }
}

using BossServer.Data;
using BossServer.Models;
using MongoDB.Driver;
using BCrypt.Net;

namespace BossServer.Services
{
    public class UserService
    {
        private readonly IMongoCollection<ApplicationUser> _users;

        public UserService(IMongoDbContext context)
        {
            _users = context.Users;
        }

        public async Task<ApplicationUser> AuthenticateAsync(string mail, string password)
        {
            var user = await _users.Find(u => u.Email == mail).FirstOrDefaultAsync();

            if (user == null || !VerifyPasswordHash(password, user.PasswordHash))
            {
                return null;
            }

            return user;
        }

        private bool VerifyPasswordHash(string password, string storedHash)
        {
            // Verify password against stored hash
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }

        public async Task<bool> RegisterAsync(ApplicationUser newUser)
        {
            // Check if user already exists
            var existingUser = await _users.Find(u => u.Email == newUser.Email).FirstOrDefaultAsync();
            if (existingUser != null)
            {
                return false; // User already exists
            }

            // Hash the password
            newUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newUser.PasswordHash);

            // Insert the new user
            await _users.InsertOneAsync(newUser);
            return true;
        }
    }
}

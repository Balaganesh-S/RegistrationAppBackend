using MongoDB.Driver;
using WebApplication3.Data;
using WebApplication3.DTOs;
using WebApplication3.Models;

namespace WebApplication3.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MongoDbContext _mongoDbContext;

        public UserRepository(MongoDbContext mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
        }
        public async Task<User> addUser(User user)
        {
            await _mongoDbContext.Users.InsertOneAsync(user);
            return user;
        }

        public async Task<bool> isEmailExist(string email)
        {
         return await _mongoDbContext.Users
                .Find(u => u.email == email)
                .AnyAsync();
        }
    }
    
}

using Microsoft.EntityFrameworkCore;
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

        public async Task<User> findUserByEmail(string email)
        {
            var filter = Builders<User>.Filter.Eq(u => u.email, email);
            var user = await _mongoDbContext.Users.Find(filter).FirstOrDefaultAsync();
            return user;
        }

        public async Task<List<User>> findUserByQuery(FilterDto filter)
        {
            var builder = Builders<User>.Filter;
            var filters = new List<FilterDefinition<User>>();

            // Age Filter
            if (filter.Age != null)
            {
                if (filter.Age.startAge > 0)
                    filters.Add(builder.Gte(u => u.Age, filter.Age.startAge));

                if (filter.Age.endAge > 0)
                    filters.Add(builder.Lte(u => u.Age, filter.Age.endAge));
            }

            // Date Filter
            if (filter.Date != null)
            {
                if (filter.Date.startDate.HasValue)
                    filters.Add(builder.Gte(u => u.CreatedDate, filter.Date.startDate.Value));

                if (filter.Date.endDate.HasValue)
                    filters.Add(builder.Lte(u => u.CreatedDate, filter.Date.endDate.Value));
            }

            // Gender Filter
            if (filter.Gender != null && filter.Gender.Any())
            {
                filters.Add(builder.In(u => u.Gender.ToLower(), filter.Gender.Select(g => g.ToLower())));
            }

            // Plan Filter
            if (filter.Plan != null && filter.Plan.Any())
            {
                filters.Add(builder.In(u => u.Plan.ToLower(), filter.Plan.Select(p => p.ToLower())));
            }

            // Combine all filters (AND)
            var finalFilter = filters.Any() ? builder.And(filters) : builder.Empty;

            return await _userCollection.Find(finalFilter).ToListAsync();
        }

        public async Task<bool> isEmailExist(string email)
        {
         return await _mongoDbContext.Users
                .Find(u => u.email == email)
                .AnyAsync();
        }
    }
    
}

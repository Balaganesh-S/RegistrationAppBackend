using WebApplication3.DTOs;
using WebApplication3.Models;

namespace WebApplication3.Repositories
{
    public interface IUserRepository
    {
        Task<User> addUser(User user);
        Task<bool> isEmailExist(string email);
    }
}

using TheRestaurant.Models;

namespace TheRestaurant.Repositories.IRepositories;

public interface IUserRepository
{
    Task<User?> GetUserByUserNameAsync(string userName);
}
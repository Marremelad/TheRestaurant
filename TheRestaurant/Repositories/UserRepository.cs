using Microsoft.EntityFrameworkCore;
using TheRestaurant.Data;
using TheRestaurant.Models;
using TheRestaurant.Repositories.IRepositories;

namespace TheRestaurant.Repositories;

public class UserRepository(TheRestaurantDbContext context) :IUserRepository
{
    // There will only be one user in this application. Still good to have this though if I want to scale in the future.
    public async Task<User?> GetUserByUserNameAsync(string userName) =>
        await context.Users.FirstOrDefaultAsync(user => user.UserName == userName);
}
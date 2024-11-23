using Microsoft.EntityFrameworkCore;
using TechTrader.Models;
using TechTrader.Interfaces;

namespace TechTrader.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TechTraderDbContext dbContext;

        public UserRepository(TechTraderDbContext context)
        { 
            dbContext = context;
        }

        // get a single user by id
        public async Task<User> GetUserByIdAsync(int userId)
        {
            User selectedUser = await dbContext.Users
                .Include(user => user.PaymentTypes)
                .Include(user => user.SavedListings)
                .FirstOrDefaultAsync(user => user.Id == userId);

            return selectedUser;
        }

        // create a user
        public async Task<User> CreateUserAsync(User user)
        {
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return user;
        }

        // update a user
        public async Task<User> UpdateUserAsync(int userId, User updatedUser)
        {
            var userToUpdate = await dbContext.Users.FirstOrDefaultAsync(user => user.Id == userId);

            if (userToUpdate == null)
            {
                return null;
            }

            userToUpdate.FirstName = updatedUser.FirstName;
            userToUpdate.LastName = updatedUser.LastName;
            userToUpdate.Email = updatedUser.Email;
            userToUpdate.ImageUrl = updatedUser.ImageUrl;
            userToUpdate.City = updatedUser.City;
            userToUpdate.State = updatedUser.State;
            userToUpdate.Zip = updatedUser.Zip;
            userToUpdate.IsSeller = updatedUser.IsSeller;

            await dbContext.SaveChangesAsync();
            return updatedUser;
        }
    }
}
﻿using TechTrader.Models;

namespace TechTrader.Interfaces
{
    public interface IUserService
    {
        Task<User?> CheckUserAsync(string uid);
        Task<User> CreateUserAsync(User User);
        Task<User> UpdateUserAsync(int UserId, User User);
        Task<User> GetUserByIdAsync(int UserId);
    }
}
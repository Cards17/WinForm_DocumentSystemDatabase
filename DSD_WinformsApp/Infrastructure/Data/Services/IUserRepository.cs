﻿using DSD_WinformsApp.Core.DTOs;

namespace DSD_WinformsApp.Infrastructure.Data.Services
{
    public interface IUserRepository
    {
        Task<List<UserCredentialsDto>> GetAllUsers();
        void RegisterUser(UserCredentialsDto userCredentials);
        Task<UserCredentialsDto?> GetUserByEmail(string emailAddress);
        Task<bool> EditUser(int userId, UserCredentialsDto userCredentials);
        Task<bool> DeleteUser(int userId);


    }
}
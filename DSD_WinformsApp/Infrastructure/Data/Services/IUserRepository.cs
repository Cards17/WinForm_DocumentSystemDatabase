using DSD_WinformsApp.Core.DTOs;

namespace DSD_WinformsApp.Infrastructure.Data.Services
{
    public interface IUserRepository
    {
        void RegisterUser(UserCredentialsDto userCredentials);
        Task<UserCredentialsDto?> GetUserByEmail(string emailAddress);
        Task<UserCredentialsDto?> GetUserByFullName(string firstName, string lastName);
    }
}
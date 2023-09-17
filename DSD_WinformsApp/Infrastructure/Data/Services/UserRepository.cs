using AutoMapper;
using DSD_WinformsApp.Core.DTOs;
using DSD_WinformsApp.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DSD_WinformsApp.Infrastructure.Data.Services
{
    public class UserRepository: IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly IDocumentDbContext _dbContext;
        public UserRepository(IMapper mapper, IDocumentDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            
        }

        //Add method to gett all items from the database
        public async Task<List<UserCredentialsDto>> GetAllUsers()
        {
            return await GetAllUsers(UserRole.All);
        }

        public async Task<List<UserCredentialsDto>> GetAllUsers(UserRole role)
        {
            if (role == UserRole.All)
            {
                var allUsers = await _dbContext.UserCredentials.ToListAsync();
                return _mapper.Map<List<UserCredentialsModel>, List<UserCredentialsDto>>(allUsers);
            }
            else
            {
                var filteredUsers = await _dbContext.UserCredentials.Where(u => u.UserRole == role).ToListAsync();
                return _mapper.Map<List<UserCredentialsModel>, List<UserCredentialsDto>>(filteredUsers);
            }
        }



        public void RegisterUser(UserCredentialsDto userCredentials)
        {
            var user = _mapper.Map<UserCredentialsDto, UserCredentialsModel>(userCredentials);
            _dbContext.UserCredentials.Add(user);
            _dbContext.SaveChanges();
        }
       
        // Add method to Get a user by email address
        public async Task<UserCredentialsDto?> GetUserByEmail(string emailAddress)
        {
            return await Task.Run(() =>
            {
                return _dbContext.UserCredentials
                    .Where(user => user.EmailAddress == emailAddress)
                    .Select(user => new UserCredentialsDto
                    {
                        Firstname = user.Firstname,
                        Lastname = user.Lastname,
                        EmailAddress = user.EmailAddress,
                        Password = user.Password
                    })
                    .FirstOrDefault();
            });
        }

        public string GetUserRole(string emailAddress)
        {
            try
            {
                var user = _dbContext.UserCredentials
                    .FirstOrDefault(u => u.EmailAddress == emailAddress);

                if (user != null)
                {
                    return user.UserRole.ToString();
                }
            }
            catch (Exception)
            {
                // Handle exceptions, log, or return a default role as needed
            }

            // Return a default role if user not found or an exception occurred
            return "User"; // Replace with your default role
        }


        // Add method to Edit selected user
        public async Task<bool> EditUser(int userId, UserCredentialsDto userCredentials)
        {
            var user = await _dbContext.UserCredentials.FindAsync(userId);
            if (user == null)
            {
                return false; // User not found, cannot edit.
            }

            // Update the user
            user.Firstname = userCredentials.Firstname;
            user.Lastname = userCredentials.Lastname;
            user.EmailAddress = userCredentials.EmailAddress;
            user.Password = userCredentials.Password;
            user.JobTitle = userCredentials.JobTitle;

            _dbContext.SaveChanges();
            return true;

        }


        // Add method to delete selected user
        public async Task<bool> DeleteUser(int userId)
        {
            var user = await _dbContext.UserCredentials.FindAsync(userId);
            if (user == null)
            {
                return false; // User not found, cannot delete.
            }

            _dbContext.UserCredentials.Remove(user);
            _dbContext.SaveChanges();
            return true;
        }


    }

   
}

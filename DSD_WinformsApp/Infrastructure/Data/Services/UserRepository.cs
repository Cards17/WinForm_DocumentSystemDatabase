using AutoMapper;
using DSD_WinformsApp.Core.DTOs;
using DSD_WinformsApp.Model;
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

        // Add method to gett all items from the database
        public async Task<List<UserCredentialsDto>> GetAllUsers()
        {
            return await Task.Run(() =>
            {
                return _dbContext.UserCredentials
                    .Select(user => new UserCredentialsDto
                    {
                        UserId = user.UserId,
                        Firstname = user.Firstname,
                        Lastname = user.Lastname,
                        EmailAddress = user.EmailAddress,
                        CreatedDate = user.CreatedDate,
                    })
                    .ToList();
            });
        }

        public void RegisterUser(UserCredentialsDto userCredentials)
        {
            var user = _mapper.Map<UserCredentialsDto, UserCredentialsModel>(userCredentials);
            _dbContext.UserCredentials.Add(user);
            _dbContext.SaveChanges();
        }
       

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
    }

   
}

using Application.Common;
using Application.Interface;
using Application.InterfaceRepository;
using Application.Util;
using Application.ViewModel;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;
        private readonly ICurrentTime _currentTime;
        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, ICurrentTime currentTime, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _currentTime = currentTime;
            _configuration = configuration;
        }

        public async Task<List<User>> GetAllUser()
        {
            List<User> users = new List<User>();    
            users= await _userRepository.GetAllAsync();
            return users;
        }

        public async Task<Token> LoginAsync(string username, string password)
        {
            bool checkEmail =await _userRepository.CheckEmail(username);
            User user = await _userRepository.FindUserByEmail(username);
            if(!checkEmail)
            {
                throw new Exception("Email not existed");
            }
            if (password.CheckPassword(user.Password)==false)
            {
                throw new Exception("Password incorrect");
            }
            var refreshToken = RefreshTokenString.GetRefreshToken();
            var accessToken = user.GenerateJsonWebToken(_configuration.GetSection("AppSetting:Token").Value!, _currentTime.GetCurrentTime());
            var expireRefreshTokenTime = DateTime.Now.AddHours(24);
         return   new Token
            {
                emai = user.Email,
                accessToken = accessToken,
                refreshToken = refreshToken,
            };
            
        }

        public async Task<bool> RegisterAsync(string username, string password)
        {
            bool user = await _userRepository.CheckEmail(username);
            if (user)
            {
                throw new Exception("Account already existed");
            }
            User newUser = new User
            {
                Email = username,
                Password = password.Hash(),
                RoleId=3
            };
            await _userRepository.AddAsync(newUser);
            return await _unitOfWork.SaveChangeAsync() > 0;

        }
       
    }
}


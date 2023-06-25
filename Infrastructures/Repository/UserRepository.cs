using Application.Interface;
using Application.InterfaceRepository;
using Application.Util;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IClaimService _claimService;
        private readonly ICurrentTime _currentTime;
        public UserRepository(AppDbContext dbContext, ICurrentTime timeService, IClaimService claimsService) : base(dbContext, timeService, claimsService)
        {
            _appDbContext = dbContext;
            _currentTime = timeService;
            _claimService = claimsService;
        }

        public async Task<bool> CheckEmail(string email)
        {
            return await _appDbContext.User.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> CheckPassword(string password)
        {
            return await _appDbContext.User.AnyAsync(u => u.Password == password);
        }

        public async Task<User> FindUserByEmail(string email)
        {
            return await _appDbContext.User.FirstAsync(u => u.Email.Equals(email));
        }
       
    }
}

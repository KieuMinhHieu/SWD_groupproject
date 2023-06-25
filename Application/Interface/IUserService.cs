using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Security.Claims;
using Application.ViewModel;

namespace Application.Interface
{
    public  interface IUserService
    {
        Task<bool> RegisterAsync(string username,string password);
        Task<Token> LoginAsync(string username,string password);
    }
}

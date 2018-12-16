﻿using System.Threading.Tasks;
using Cmt.Bll.DTOs.Users;
using Cmt.Dal.Identities;

namespace Cmt.Bll.Services.Interfaces
{
    public interface IAuthService
    {
        Task<UserDto> SignInAsync(string email, string password);
        Task SignOutAsync();
        Task<int> CreateAsync(UserDto user, string password);
    }
}
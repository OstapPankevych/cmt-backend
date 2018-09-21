using System;
using System.Threading.Tasks;
using Cmt.Common.DTOs.Users;
using Cmt.Common.Identity;

namespace Cmt.Bll.Services.Interfaces
{
    public interface IAuthService
    {
        Task<UserDto> SignInAsync(string email, string password);
        Task SignOutAsync();
        Task<int> CreateAsync(CmtIdentityUser user, string password);
    }
}

using System;
using System.Threading.Tasks;
using Cmt.Common.Identity;
using Microsoft.AspNetCore.Identity;

namespace Cmt.Bll.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> SignInAsync(string email, string password);
        Task SignOutAsync();
        Task<int> CreateAsync(CmtIdentityUser user, string password);
    }
}

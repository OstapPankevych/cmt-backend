using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Cmt.Bll.Services.Exceptions;
using Cmt.Bll.Services.Interfaces;
using Cmt.Common.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using Cmt.Common.Settings;
using System.Linq;
using Cmt.Bll.Services.Exceptions.Auth;
using Cmt.Common.Helpers;

namespace Cmt.Bll.Services
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<CmtIdentityUser> _signInManager;
        private readonly UserManager<CmtIdentityUser> _userManager;
        private readonly RoleManager<CmtIdentityRole> _roleManager;
        private readonly AuthSettings _authSettings;

        public AuthService(
            SignInManager<CmtIdentityUser> signInManager,
            UserManager<CmtIdentityUser> userManager,
            RoleManager<CmtIdentityRole> roleManager,
            AuthSettings authSettings)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _authSettings = authSettings;
        }

        public async Task<string> SignInAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new AuthException { Errors = new[] { new ErrorResult(AuthErrorCode.WrongLoginOrPassword) } };
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                {
                    throw new AuthException { Errors = new[] { new ErrorResult(AuthErrorCode.LockedOut) } };
                }

                if (result.IsNotAllowed)
                {
                    throw new AuthException { Errors = new[] { new ErrorResult(AuthErrorCode.NotAllowed) } };
                }

                if (result.RequiresTwoFactor)
                {
                    throw new AuthException { Errors = new[] { new ErrorResult(AuthErrorCode.RequiresTwoFactor) } };
                }

                throw new AuthException { Errors = new[] { new ErrorResult(AuthErrorCode.WrongLoginOrPassword) } };
            }

            var claims = await _userManager.GetClaimsAsync(user);
            var token = GetJwtSecurityToken(claims);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<int> CreateAsync(CmtIdentityUser user, string password)
        {
            //var saltPass = SaltPassword(password);
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                throw new AuthException { Errors = GetErrors(result.Errors) };
            }

            var roleResult = await _userManager.AddClaimAsync(user,
                new Claim(ClaimTypes.Role, UserRoles.User));

            if (!roleResult.Succeeded)
            {
                throw new AuthException { Errors = GetErrors(roleResult.Errors) };
            }

            return user.Id;
        }

        private JwtSecurityToken GetJwtSecurityToken(IList<Claim> claims)
        {
            var securityKey = JwtHelper.GetSecurityKey(_authSettings.JwtSecurityKey);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(_authSettings.JwtExpirationTimeMinutes),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }

        private string SaltPassword(string password)
        {
            return password + _authSettings.PasswordSalt;
        }

        private IEnumerable<ErrorResult> GetErrors(IEnumerable<IdentityError> errors)
        {
            return errors?.Select(x => new ErrorResult(x.Code));
        }
    }
}

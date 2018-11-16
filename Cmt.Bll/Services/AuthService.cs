using System;
using System.Collections.Generic;
using System.Security.Claims;
using Cmt.Bll.Services.Exceptions;
using Cmt.Bll.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Cmt.Common.Settings;
using System.Linq;
using Cmt.Bll.Services.Exceptions.Auth;
using Cmt.Common.Helpers;
using Cmt.Common.DTOs.Users;
using Cmt.Dal.Entities.Identities;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Cmt.Common.Constants;

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

        public async Task<UserDto> SignInAsync(string email, string password)
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

            var jwtToken = await GetJwtToken(user);

            return new UserDto
            {
                Name = user.UserName,
                Jwt = jwtToken
            };
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<int> CreateAsync(CmtIdentityUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                throw new AuthException { Errors = GetErrors(result.Errors) };
            }

            await AddInRoleAsync(UserRoles.User, user);

            return user.Id;
        }

        private IEnumerable<ErrorResult> GetErrors(IEnumerable<IdentityError> errors)
        {
            return errors?.Select(x => new ErrorResult(x.Code));
        }

        private async Task<JwtDto> GetJwtToken(CmtIdentityUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = JwtHelper.GetSecurityKey(_authSettings.JwtSecurityKey);
            var claims = await _userManager.GetClaimsAsync(user);
            var expires = DateTime.Now.AddMinutes(_authSettings.JwtExpirationTimeMinutes);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expires,
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new JwtDto
            {
                AccessToken = tokenString,
                Expires = expires
            };
        }

        private async Task AddInRoleAsync(string roleName, CmtIdentityUser user)
        {
            var role = await _roleManager.FindByNameAsync(roleName)
                       ?? await CreateRole(roleName);


            var claims = await _roleManager.GetClaimsAsync(role)
                         ?? new List<Claim> { await CreateClaim(role, ClaimTypes.Role, roleName) };
                                           
            var userClaimsResult = await _userManager.AddClaimsAsync(user, claims);

            if (!userClaimsResult.Succeeded)
            {
                throw new AuthException { Errors = GetErrors(userClaimsResult.Errors) };
            }
        }

        private async Task<CmtIdentityRole> CreateRole(string roleName)
        {
            var role = new CmtIdentityRole { Name = roleName };
            var roleResult = await _roleManager.CreateAsync(role);
            if (!roleResult.Succeeded)
            {
                throw new AuthException { Errors = GetErrors(roleResult.Errors) };
            }

            return role;
        }

        private async Task<Claim> CreateClaim(CmtIdentityRole role, string type, string value)
        {
            var claim = new Claim(type, value);
            var claimResult = await _roleManager.AddClaimAsync(role, claim);

            if (!claimResult.Succeeded)
            {
                throw new AuthException { Errors = GetErrors(claimResult.Errors) };
            }

            return claim;
        }
    }
}

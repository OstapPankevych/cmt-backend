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
using Cmt.Bll.DTOs.Users;
using Microsoft.IdentityModel.Tokens;
using Cmt.Common.Constants;
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Cmt.Dal.Identities;
using Cmt.Dal.Interfaces;

namespace Cmt.Bll.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<CmtIdentityUser> _signInManager;
        private readonly UserManager<CmtIdentityUser> _userManager;
        private readonly RoleManager<CmtIdentityRole> _roleManager;
        private readonly AuthSettings _authSettings;

        public AuthService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            SignInManager<CmtIdentityUser> signInManager,
            UserManager<CmtIdentityUser> userManager,
            RoleManager<CmtIdentityRole> roleManager,
            AuthSettings authSettings)
        {
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _authSettings = authSettings;
        }

        public async Task<UserDto> SignInAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new AuthException { Errors = new[] { new ErrorResult(AuthErrorCodes.WrongLoginOrPassword) } };
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            if (result.Succeeded)
            {
                var res = _mapper.Map<UserDto>(user);
                res.Jwt = await GetJwtToken(user);

                return res;
            }

            if (result.IsLockedOut)
            {
                throw new AuthException { Errors = new[] { new ErrorResult(AuthErrorCodes.LockedOut) } };
            }

            if (result.IsNotAllowed)
            {
                throw new AuthException { Errors = new[] { new ErrorResult(AuthErrorCodes.NotAllowed) } };
            }

            if (result.RequiresTwoFactor)
            {
                throw new AuthException { Errors = new[] { new ErrorResult(AuthErrorCodes.RequiresTwoFactor) } };
            }
            
            throw new AuthException { Errors = new[] { new ErrorResult(CmtErrorCodes.Unknown) } };
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<int> CreateAsync(UserDto userDto, string password)
        {
            var user = _mapper.Map<CmtIdentityUser>(userDto);
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var result = await _userManager.CreateAsync(user, password);
                    if (!result.Succeeded)
                    {
                        throw new AuthException { Errors = GetErrors(result.Errors) };
                    }

                    var role = await CreateRoleIfNotExists(UserRoles.User);

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Role, role.Name),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName)
                    };

                    var userClaimsResult = await _userManager.AddClaimsAsync(user, claims);
                    if (!userClaimsResult.Succeeded)
                    {
                        throw new AuthException { Errors = GetErrors(userClaimsResult.Errors) };
                    }

                    transaction.Commit();

                    return user.Id;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
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

        private async Task<CmtIdentityRole> CreateRoleIfNotExists(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role != null)
            {
                return role;
            }

            var newRole = new CmtIdentityRole { Name = roleName };
            var roleResult = await _roleManager.CreateAsync(newRole);
            if (!roleResult.Succeeded)
            {
                throw new AuthException { Errors = GetErrors(roleResult.Errors) };
            }

            return newRole;
        }
    }
}

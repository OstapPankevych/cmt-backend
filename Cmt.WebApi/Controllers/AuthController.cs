using System.Threading.Tasks;
using AutoMapper;
using Cmt.Bll.Services.Interfaces;
using Cmt.Bll.DTOs.Users;
using Cmt.WebApi.Infrastructure.Attributes;
using Cmt.WebApi.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace Cmt.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class AuthController: CmtController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("SignIn")]
        public async Task<IActionResult> SignInAsync([FromBody] SignInUser model)
        {
            var userDto = await _authService.SignInAsync(model.Email, model.Password);
            var user = Mapper.Map<User>(userDto);
            var jwt = Mapper.Map<Jwt>(userDto.Jwt);

            var result = new
            {
                user,
                jwt
            };

            return Ok(result);
        }

        [HttpPost]
        [Route("Registry")]
        public async Task<IActionResult> RegistryAsync([FromBody] NewUser model)
        {
            var user = Mapper.Map<UserDto>(model);
            var id = await _authService.CreateAsync(user, model.Password);

            var result = new
            {
                user = new User { Id = id }
            };

            return Created(result);
        }

        [HttpGet]
        [Route("SignOut")]
        [JwtAuthorize]
        public async Task<IActionResult> SignOutAsync()
        {
            await _authService.SignOutAsync();

            return NoContent();
        }
    }
}

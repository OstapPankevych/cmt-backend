using System.Threading.Tasks;
using AutoMapper;
using Cmt.Bll.Services.Interfaces;
using Cmt.Bll.DTOs.Users;
using Cmt.WebApi.Infrastructure.Attributes;
using Cmt.WebApi.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cmt.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("SignIn")]
        public async Task<IActionResult> SignInAsync([FromBody] SignInUser model)
        {
            var userDto = await _authService.SignInAsync(model.Email, model.Password);
            var user = Mapper.Map<User>(userDto);

            return Ok(user);
        }

        [HttpPost]
        [Route("Registry")]
        public async Task<IActionResult> RegistryAsync([FromBody] NewUser model)
        {
            var user = Mapper.Map<UserDto>(model);
            var id = await _authService.CreateAsync(user, model.Password);

            return StatusCode(StatusCodes.Status201Created, new { id });
        }

        [HttpGet]
        [Route("SignOut")]
        [JwtAuthorize]
        public async Task<IActionResult> SignOutAsync()
        {
            await _authService.SignOutAsync();

            return StatusCode(StatusCodes.Status204NoContent); ;
        }
    }
}

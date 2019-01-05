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
            var jwt = Mapper.Map<Jwt>(userDto.Jwt);

            return Ok(CreateResponse(user, jwt));
        }

        [HttpPost]
        [Route("Registry")]
        public async Task<IActionResult> RegistryAsync([FromBody] NewUser model)
        {
            var user = Mapper.Map<UserDto>(model);
            var id = await _authService.CreateAsync(user, model.Password);
            var userModel = new User { Id = id };
            return Created(CreateResponse(userModel, null));
        }

        [HttpGet]
        [Route("SignOut")]
        [JwtAuthorize]
        public async Task<IActionResult> SignOutAsync()
        {
            await _authService.SignOutAsync();

            return NoContent();
        }

        private UserResponse CreateResponse(User user, Jwt jwt)
            => new UserResponse { User = user, JwtToken = jwt };
    }
}

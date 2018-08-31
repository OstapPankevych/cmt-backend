using System;
using System.Threading.Tasks;
using AutoMapper;
using Cmt.Bll.Services.Exceptions.Auth;
using Cmt.Bll.Services.Interfaces;
using Cmt.Common.Identity;
using Cmt.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Cmt.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class AuthController: Controller
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
            var token = await _authService.SignInAsync(model.Email, model.Password);
            return Ok(token);
        }

        [HttpPost]
        [Route("Registry")]
        public async Task<IActionResult> RegistryAsync([FromBody] NewUser model)
        {
            var user = Mapper.Map<CmtIdentityUser>(model);
            var id = await _authService.CreateAsync(user, model.Password);
            return Ok(id);
        }

        [HttpGet]
        [Route("SignOut")]
        public async Task<IActionResult> SignOutAsync()
        {
            await _authService.SignOutAsync();

            return Ok();
        }
    }
}

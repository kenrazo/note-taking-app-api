using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteTakingApi.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteTakingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthController : ControllerBase
    {
        private readonly IUserAuthService _userAuthService;

        public UserAuthController(Service.Services.IUserAuthService userAuthService)
        {
            _userAuthService = userAuthService;
        }

        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] Service.Dtos.LoginRequestDto request)
        {
            var response = await _userAuthService.Login(request);

            return Ok(response);
        }

        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] Service.Dtos.RegisterRequestDto requestDto)
        {
            var response = await _userAuthService.Register(requestDto);

            return Ok(response);
        }
    }
}

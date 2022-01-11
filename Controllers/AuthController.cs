using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSystem.Data.AuthenticationRepo;
using VotingSystem.Dtos.UserDtos;
using VotingSystem.Models;

namespace VotingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IAuthRepository _authRepository;
        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto user) 
        {
            ServiceResponse<string> response = await _authRepository.Login(user.Username, user.Password);
            if(response.Data == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDto user)
        {
            ServiceResponse<int> response = await _authRepository.Register(new User{Username = user.Username}, user.Password);
            if (response.Success == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}

﻿using Microsoft.AspNetCore.Mvc;
using RealEstateAPI.Repositories;

namespace RealEstateAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenHandler _tokenHandler;

        public AuthController(IUserRepository userRepository, ITokenHandler tokenHandler)
        {
            _userRepository = userRepository;
            _tokenHandler = tokenHandler;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginAsync(Models.DTO.LoginRequest loginRequest)
        {
           
            // Check if User is Authenticated
            var user = await _userRepository.AuthenticateAsync(
                loginRequest.Username, loginRequest.Password);

            if (user != null)
            {
                //Generate a Jwt Token
                var token = await _tokenHandler.CreateTokenAsync(user);

                return Ok(token);

            }

            return BadRequest("Username or Password is incorrect");
        }
    }
}

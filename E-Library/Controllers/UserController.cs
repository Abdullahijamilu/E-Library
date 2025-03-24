using Azure;
using E_Library.DTO;
using E_Library.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_Library.Services.Interface;
using E_Library.Services;

namespace E_Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ElibraryContext _Context;
        private readonly IAuthenticateServices _authenticateServices;
        public UserController(ElibraryContext context, IAuthenticateServices service)
        {
            _Context = context;
            _authenticateServices = service;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authenticateServices.ValidateUser(model);

            if (!result.IsSuccess)
            {
                return Unauthorized(new {Message = result.Message});
            }

            return Ok(new {Message = "Login Successful", user = result.Data}); 
        }

       
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authenticateServices.RegisterUser(model);

            if (result.Succeeded)
            {
                return BadRequest(new { Message = "Registration failed", errors = result.Errors });
            }

            return Ok(new { Message = "Registration successful" }); 
        }

        [HttpGet("GetUserProfile/{userId}")]
        public async Task<IActionResult> GetUserProfile(string userId)
        {
            var result = await _authenticateServices.GetUserProfile(userId);

            if (!result.IsSuccess)
            {
                return NotFound(new {Message=result.Message});
            }

            return Ok(result);
        }

    }
}


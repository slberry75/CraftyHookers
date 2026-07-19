using CraftyHookers.UserService.API.Models;
using CraftyHookers.UserService.Application.Commands;
using CraftyHookers.UserService.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace CraftyHookers.UserService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(ISender sender) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult> RegisterUserAsync([FromBody] RegistrationDto dto)
        {
            var user = new User
            {
                Email = dto.Email,
                DisplayName = dto.DisplayName ?? dto.Email
            };
            var result = await sender.Send(new AddUserCommand(user));
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginUserAsync([FromBody] LoginDto dto)
        {
            var token = await sender.Send(new LoginUserCommand(dto.Email, dto.Password));
            if (token == null)
            {
                return NotFound("User not found.");
            }

            // Implement login logic here
            return Ok(token);
        }

    }
}

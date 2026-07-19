using CraftyHookers.UserService.Application.Commands;
using CraftyHookers.UserService.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CraftyHookers.UserService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(ISender sender) : ControllerBase
    {
        [HttpPost("")]
        public async Task<ActionResult> AddUserAsync([FromBody] User user)
        {
            var result = await sender.Send(new AddUserCommand(user));
            return Ok(result);
        }

    }
}

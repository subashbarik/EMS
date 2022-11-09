using Application.AccountService.Command;
using Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation.Controllers
{
    public class AccountController:BaseApiController
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {   
            var jwtToken = Request.Headers["Authorization"];
            var email = User.FindFirstValue(ClaimTypes.Email);
            var userDto = await _mediator.Send(new GetCurrentUserCommand(email, jwtToken));
            return userDto;
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {   
            //ModelState.IsValid()
            var userDto = await _mediator.Send(new RegisterUserCommand(registerDto));
            if (userDto.ApiErrorResponse is not null && userDto.ApiErrorResponse.Errors is not null)
            {
                return BadRequest(userDto.ApiErrorResponse);
            }
            return Ok(userDto);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var userDto = await _mediator.Send(new LoginUserCommand(loginDto));
            if (userDto.ApiErrorResponse is not null && userDto.ApiErrorResponse.Errors is not null)
            {
                return Unauthorized(userDto.ApiErrorResponse);
            }
            return Ok(userDto);
        }
        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _mediator.Send(new CheckEmailExistsCommand(email));
        }
    }
}

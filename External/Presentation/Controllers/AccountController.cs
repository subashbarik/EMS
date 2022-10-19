using Application.AccountService.Command;
using Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class AccountController:BaseApiController
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {   
            var userDto = await _mediator.Send(new RegisterUserCommand(registerDto));
            if(userDto.apiErrorResponse is not null && userDto.apiErrorResponse.Errors is not null)
            {
                return BadRequest(userDto.apiErrorResponse);
            }
            return Ok(userDto);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var userDto = await _mediator.Send(new LoginUserCommand(loginDto));
            if (userDto.apiErrorResponse is not null && userDto.apiErrorResponse.Errors is not null)
            {
                return Unauthorized(userDto.apiErrorResponse);
            }
            return Ok(userDto);
        }
    }
}

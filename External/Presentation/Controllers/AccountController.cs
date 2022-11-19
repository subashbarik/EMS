using Application.AccountService.Command;
using Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        /// <summary>
        /// Gets the current user Infomation based on the bearer token passed in the request header.
        /// </summary>
        /// <returns>Returns a userDto</returns>
        /// <response code="201">Returns the newly created userDto</response>    
        /// <response code="400">If the userDto is null</response>
        /// <response code="401">If the bearer token is invalid</response>      
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {   
            var jwtToken = Request.Headers["Authorization"];
            var email = User.FindFirstValue(ClaimTypes.Email);
            var userDto = await _mediator.Send(new GetCurrentUserCommand(email, jwtToken));
            if (userDto.ApiErrorResponse is not null && userDto.ApiErrorResponse.Errors is not null)
            {
                return BadRequest(userDto.ApiErrorResponse);
            }
            return userDto;
        }
        /// <summary>
        /// Register's a new user
        /// </summary>
        /// <param name="registerDto"></param>
        /// <returns>Returns a userDto</returns>
        /// <response code="201">Returns the newly created userDto</response> 
        /// <response code="401">If the userDto is null</response>      
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        /// <summary>
        /// Login to the system
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns>Returns a userDto</returns>
        /// <response code="201">Returns the newly created userDto</response>    
        /// <response code="400">If the userDto is null</response>

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var userDto = await _mediator.Send(new LoginUserCommand(loginDto));
            if (userDto.ApiErrorResponse is not null && userDto.ApiErrorResponse.Errors is not null)
            {
                return Unauthorized(userDto.ApiErrorResponse);
            }
            return Ok(userDto);
        }
        /// <summary>
        /// Checks whether email exists in the system
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Returns true/false</returns>
        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _mediator.Send(new CheckEmailExistsCommand(email));
        }
    }
}

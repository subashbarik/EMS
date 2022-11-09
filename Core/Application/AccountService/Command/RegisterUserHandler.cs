using Application.Dtos;
using Application.Statics;
using Application.Errors;
using Application.Interfaces;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Application.Helpers;

namespace Application.AccountService.Command
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, UserDto>
    {
        private readonly IMediator _mediator;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenSertvice;

        public RegisterUserHandler(IMediator mediator, UserManager<AppUser> userManager, ITokenService tokenSertvice)
        {
            _mediator = mediator;
            _userManager = userManager;
            _tokenSertvice = tokenSertvice;
        }
        public async Task<UserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            UserDto userDto = new();
            if (await _mediator.Send(new CheckEmailExistsCommand(request.registerDto.Email), cancellationToken))
            {
                userDto.ApiErrorResponse = new ApiValidationErrorResponse { Errors = new[] { "Email address is in use" } };
                return userDto;
            }
            var user = new AppUser
            {
                Email = request.registerDto.Email,
                UserName = request.registerDto.Email,
                DisplayName = request.registerDto.DisplayName,
            };
            var result = await _userManager.CreateAsync(user, request.registerDto.Password);
            if (!result.Succeeded)
            {
                userDto.ApiErrorResponse = new ApiValidationErrorResponse { Errors = new[] { $"Unable to create user. Reason : {result.Errors}" } };
                return userDto;
            }
            var roleResult =  await _userManager.AddToRoleAsync(user, Role.User);
            if(!roleResult.Succeeded)
            {
                userDto.ApiErrorResponse = new ApiValidationErrorResponse { Errors = new[] { $"Unable to assign role to the user. Reason : {roleResult.Errors}" } };
                return userDto;
            }
            userDto.Email = user.Email;
            userDto.DisplayName = user.DisplayName;
            userDto.Roles = new List<string>();
            userDto.Roles.Add(Role.User);
            userDto.Token = _tokenSertvice.CreateToken(user, userDto.Roles);
            userDto.IsAdmin = AccountHelper.IsAdmin(userDto);
            return userDto;
        }
    }
}

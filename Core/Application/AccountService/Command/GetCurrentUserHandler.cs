using Application.Dtos;
using Application.Errors;
using Application.Helpers;
using Application.Interfaces;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.AccountService.Command
{
    public class GetCurrentUserHandler : IRequestHandler<GetCurrentUserCommand, UserDto>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public GetCurrentUserHandler(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }
        public async Task<UserDto> Handle(GetCurrentUserCommand request, CancellationToken cancellationToken)
        {
            UserDto userDto = new();
            var user = await _userManager.FindByEmailAsync(request.Email);
            if(user ==  null)
            {
                userDto.ApiErrorResponse = new ApiValidationErrorResponse { Errors = new[] { "Unable to find user" } };
                return userDto;
            }
            var roles = await _userManager.GetRolesAsync(user);
            if (roles == null)
            {
                userDto.ApiErrorResponse = new ApiValidationErrorResponse(401) { Errors = new[] { "No roles associated with the user." } };
                return userDto;
            }
            userDto.Email = user.Email;
            userDto.DisplayName = user.DisplayName;
            userDto.Token = _tokenService.CreateToken(user);
            userDto.Roles = roles;
            userDto.IsAdmin = AccountHelper.IsAdmin(userDto);
            return userDto;
        }
    }
}

using Application.Dtos;
using Application.Errors;
using Application.Helpers;
using Application.Interfaces;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.AccountService.Command
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, UserDto>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public LoginUserHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }
        public async Task<UserDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            UserDto userDto = new();
            var user = await _userManager.FindByEmailAsync(request.LoginDto.Email);
            if (user == null)
            {
                userDto.ApiErrorResponse = new ApiValidationErrorResponse(401) { Errors = new[] { "Unable to find User." } };
                return userDto;
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, request.LoginDto.Password, false);
            if (!result.Succeeded)
            {
                userDto.ApiErrorResponse = new ApiValidationErrorResponse(401) { Errors = new[] { "Wrong Password." } };
                return userDto;
            }
            var roles = await _userManager.GetRolesAsync(user);
            if(roles == null)
            {
                userDto.ApiErrorResponse = new ApiValidationErrorResponse(401) { Errors = new[] { "No roles associated with the user." } };
                return userDto;
            }
            userDto.Email = user.Email;
            userDto.DisplayName = user.DisplayName;
            userDto.Roles = roles;
            userDto.Token = _tokenService.CreateToken(user,userDto.Roles);
            userDto.IsAdmin = AccountHelper.IsAdmin(userDto);
            return userDto;
        }
    }
}

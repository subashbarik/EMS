using Application.Dtos;
using Application.Errors;
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
                userDto.apiErrorResponse = new ApiValidationErrorResponse(401) { Errors = new[] { "Unable to find User" } };
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, request.LoginDto.Password, false);
            if (!result.Succeeded)
            {
                userDto.apiErrorResponse = new ApiValidationErrorResponse(401) { Errors = new[] { "Wrong Password" } };
            }
            return new UserDto
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user)
            };
        }
    }
}

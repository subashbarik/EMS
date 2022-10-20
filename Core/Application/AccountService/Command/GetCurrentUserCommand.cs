using Application.Dtos;
using MediatR;
using System.Security.Claims;

namespace Application.AccountService.Command
{
    public sealed record GetCurrentUserCommand(string Email):IRequest<UserDto>;
    
}

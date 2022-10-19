using Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AccountService.Command
{
    public sealed record LoginUserCommand(LoginDto LoginDto):IRequest<UserDto>;
    
}

using MediatR;

namespace Application.AccountService.Command
{
    public sealed record CheckEmailExistsCommand(string Email):IRequest<bool>;
    
}

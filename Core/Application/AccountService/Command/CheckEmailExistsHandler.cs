using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.AccountService.Command
{
    public class CheckEmailExistsHandler : IRequestHandler<CheckEmailExistsCommand, bool>
    {
        private readonly UserManager<AppUser> _userManager;

        public CheckEmailExistsHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<bool> Handle(CheckEmailExistsCommand request, CancellationToken cancellationToken)
        {
            return await _userManager.FindByEmailAsync(request.Email) != null;
        }
    }
}

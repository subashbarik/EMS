using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Application.EmailService.Commands
{
    public class SendEmailHandler : IRequestHandler<SendEmailCommand, bool>
    {
        private readonly IEmailService _emailService;

        public SendEmailHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }
        public async Task<bool> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            return await _emailService.SendMail(request.Email);
        }
    }
}

using Application.Interfaces;
using MediatR;

namespace Application.SmsService.Commands
{
    public class SendSmsHandler : IRequestHandler<SendSmsCommand, bool>
    {
        private readonly ISmsService _smsService;

        public SendSmsHandler(ISmsService smsService)
        {
            _smsService = smsService;
        }
        public async Task<bool> Handle(SendSmsCommand request, CancellationToken cancellationToken)
        {
            await _smsService.SendSMS(request.Sms);
            return true;
        }
    }
}

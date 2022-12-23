using Application.Dtos;
using Application.Interfaces;
using Application.Options;
using Microsoft.Extensions.Options;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Infrastructure.Services.TwilioSms
{
    public class TwilioSmsService : ISmsService
    {
        private readonly ITwilioRestClient _client;
        private readonly IOptions<AppSmsOptions> _smsOptions;

        public TwilioSmsService(ITwilioRestClient client, IOptions<AppSmsOptions> smsOptions)
        {
            _client = client;
            _smsOptions = smsOptions;
        }
        public async Task<bool> SendSMS(SmsDto sms)
        {
            if(string.IsNullOrWhiteSpace(sms.From))
            {
                sms.From = _smsOptions.Value.From;
            }

            await MessageResource.CreateAsync(to: new PhoneNumber(sms.To),
                                           from: new PhoneNumber(sms.From),
                                           body: sms.Message,client: _client);
            return true;
        }
    }
}

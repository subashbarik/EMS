using Application.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Twilio.Clients;
using Twilio.Http;
using SystemHttpClient = System.Net.Http.HttpClient;

namespace Infrastructure.Services.TwilioSms
{
    public class TwilioClient : ITwilioRestClient
    {
        private readonly ITwilioRestClient _innerClient;

        private readonly IOptions<AppSmsOptions> _smsOptions;

        public string AccountSid => _innerClient.AccountSid;

        public string Region => _innerClient?.Region;

        public Twilio.Http.HttpClient HttpClient => _innerClient.HttpClient;

        public TwilioClient(SystemHttpClient httpClient, IOptions<AppSmsOptions> smsOptions)
        {
            _smsOptions = smsOptions;
            // customize the underlying HttpClient
            httpClient.DefaultRequestHeaders.Add("X-Custom-Header", "EMS");
            _innerClient = new TwilioRestClient(
                _smsOptions.Value.AccountSid,
                _smsOptions.Value.AuthToken,
                httpClient: new SystemNetHttpClient(httpClient));
        }
        public Response Request(Request request)
        {
            return _innerClient.Request(request);
        }

        public Task<Response> RequestAsync(Request request)
        {
            return _innerClient.RequestAsync(request);
        }
    }
}

using Application.Dtos;

namespace Application.Interfaces
{
    public interface ISmsService
    {
        Task<bool> SendSMS(SmsDto sms);
    }
}

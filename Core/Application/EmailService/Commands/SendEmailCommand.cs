using Application.Dtos;
using MediatR;

namespace Application.EmailService.Commands
{
    public sealed record SendEmailCommand(EmailDto Email):IRequest<bool>;
   
}

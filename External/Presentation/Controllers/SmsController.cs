using Application.Dtos;
using Application.EmailService.Commands;
using Application.SmsService.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class SmsController:BaseApiController
    {
        private readonly IMediator _mediator;

        public SmsController(IMediator mediator)
        {
            _mediator = mediator;
        }
       /// <summary>
       /// Send a SMS
       /// </summary>
       /// <param name="sms"></param>
       /// <returns></returns>
        [HttpPost("sendsms")]
        public async Task<bool> SendSms(SmsDto sms)
        {
            return await _mediator.Send(new SendSmsCommand(sms));
        }
    }
}

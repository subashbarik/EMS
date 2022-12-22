using Application.Dtos;
using Application.EmailService.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class EmailController:BaseApiController
    {
        private readonly IMediator _mediator;

        public EmailController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Sends an Email with the provided inputs in the EmailDto object
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost("sendemail")]
        public async Task<bool> SendEMail(EmailDto email)
        {
            return await _mediator.Send(new SendEmailCommand(email));
        }

    }
}

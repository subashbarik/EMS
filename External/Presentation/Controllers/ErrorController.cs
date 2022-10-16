
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Presentation.Errors;
using System.Net;
using Domain.Errors;

namespace Presentation.Controllers
{
    public class ErrorController : BaseApiController
    {
        private readonly ILogger<ErrorController> _logger;
        private readonly IHostEnvironment _env;

        public ErrorController(ILogger<ErrorController> logger,IHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Error()
        {
            int httpStatusCode;
            
            Exception exception = HttpContext.Features.Get<IExceptionHandlerFeature>().Error;
            if( exception is ItemNotFoundException)
            {
                //var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                //{
                //    Content = new StringContent(context.Exception.Message),
                //    ReasonPhrase = "ItemNotFound"
                //};
                httpStatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                httpStatusCode = (int)HttpStatusCode.InternalServerError;
            }

            _logger.LogError(exception, exception.Message);
            var response = _env.IsDevelopment() ?
                                new ApiException(httpStatusCode, exception.Message, exception.StackTrace.ToString()) :
                                new ApiException(httpStatusCode);
            return Problem(title: response.Message,statusCode: httpStatusCode, detail: response.Details);
        }
    }
}
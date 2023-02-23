
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Presentation.Errors;
using System.Net;
using Domain.Errors;
using Serilog.Context;
using System.Security.Claims;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : BaseApiController
    {
        private readonly ILogger<ErrorController> _logger;
        private readonly IHostEnvironment _env;
        /// <summary>
        /// Constructor for the error controller
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="env"></param>
        public ErrorController(ILogger<ErrorController> logger,IHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }
        /// <summary>
        /// Catches all the exceptions of the application
        /// </summary>
        /// <returns></returns>
        [Route("appexception")]
        public IActionResult ApiException()
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

                
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "Guest";
            LogContext.PushProperty("UserId", userId);
            _logger.LogError(exception, exception.Message);
            // We can follow the below pattern to add additional structured logging .
            //_logger.LogError(exception, "Exception details {@error},{@DateTimeUtc}",exception.Message,DateTime.UtcNow);
            //_logger.LogError("{Message}{Messagetemplate}{Level}{TimeStamp}{Exception}", exception.Message, exception.Message,"Error", DateTime.Now, exception);
            var response = _env.IsDevelopment() ?
                                new ApiException(httpStatusCode, exception.Message, exception.StackTrace.ToString()) :
                                new ApiException(httpStatusCode);
            return Problem(title: response.Message,statusCode: httpStatusCode, detail: response.Details);
        }
        /// <summary>
        /// Gets executed when Api Endpoint is not found
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [Route("apiendpointnotfound/{code}")]
        public IActionResult ApiEndPointNotFoundError(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}
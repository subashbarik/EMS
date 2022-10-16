
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Presentation.Errors;

namespace Web.Filters
{
    public class ExceptionHandlingFilterAttribute:ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            int httpStatusCode = (int) HttpStatusCode.InternalServerError;
            var response = new ApiException(httpStatusCode,exception.Message,exception.StackTrace.ToString());
            context.Result = new ObjectResult(response);
            context.ExceptionHandled = true;                  
        }
        
    }
}
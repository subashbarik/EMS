using System.Net;
using System.Text.Json;
using Presentation.Errors;
namespace Web.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger,
            IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            int httpStatusCode;
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                httpStatusCode = (int) HttpStatusCode.InternalServerError;
                _logger.LogError(ex, ex.Message);
                context.Request.ContentType = "application/json";
                context.Response.StatusCode = httpStatusCode;

                var response = _env.IsDevelopment() ?
                                new ApiException(httpStatusCode,ex.Message,ex.StackTrace.ToString()) :
                                new ApiException(httpStatusCode);
                                
                var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
                var json = JsonSerializer.Serialize(response,options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
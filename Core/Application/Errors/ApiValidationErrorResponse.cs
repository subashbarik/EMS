namespace Application.Errors
{
    public class ApiValidationErrorResponse:ApiResponse
    {
        public IEnumerable<string> Errors {get;set;}
        public ApiValidationErrorResponse(int statusCode = 400) : base(statusCode)
        {
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Errors
{
    public class ApiException : ApiResponse
    {
        public string Details { get; set; }
        public ApiException(int statusCode, string message = null,string details = null) : base(statusCode, message)
        {
            Details = details ?? "An Error occurred while processing your request. Please contact your administrator.";
        }
    }
}
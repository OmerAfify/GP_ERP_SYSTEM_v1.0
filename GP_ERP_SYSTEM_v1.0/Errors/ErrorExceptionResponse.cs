using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GP_ERP_SYSTEM_v1._0.Errors
{
    public class ErrorExceptionResponse : ErrorApiResponse
    {

        public string ErrorDetails { get; set; }
        public ErrorExceptionResponse(int statusCode, string message=null, string errorDetails=null ):base(statusCode, message)
        {
            ErrorDetails = errorDetails;
        }

    }
}

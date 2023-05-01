using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GP_ERP_SYSTEM_v1._0.Errors
{
    public class ErrorApiResponse
    {

        public int StatusCode { get; set; }
        public string Message { get; set; }


        public ErrorApiResponse(int statusCode, string message = null )
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultStatusCodeMessage(statusCode);
        }



        private static string GetDefaultStatusCodeMessage(int statusCode)
        {
            return statusCode switch
            {
                500 => "Internal Server Error. Please try again later.",
                400 => "A BadRequest is sent. PLease check the kind of data being send and try again.",
                401 => "Sorry, You are not Authorized to perform this kind of Action.",
                404 => "Error 404. Not Found.",
                _ => null,
            };
        }


    }
}

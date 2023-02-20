using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GP_ERP_SYSTEM_v1._0.Errors
{
    public class ErrorValidationResponse : ErrorApiResponse
    {
        public IEnumerable<string> Errors { get; set; }
        public ErrorValidationResponse():base(400)
        {

        }


    }
}

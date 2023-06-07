using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GP_ERP_SYSTEM_v1._0.DTOs
{
    public class AddQuestionDTO
    {
        public string Question { get; set; }
    }
    public class QuestionDTO: AddQuestionDTO
    {

        public int QuestionId { get; set; }
    }
}

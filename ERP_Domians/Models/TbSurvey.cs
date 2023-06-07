using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_Domians.Models
{
    public partial class TbSurvey
    {
        public int SurveyId { get; set; }
        public string SurveyName { get; set; }
        public string SurveyDesc { get; set; }
        public int CustomerId { get; set; }
        public int QuestionId { get; set; }

        public virtual TbCustomer Customer { get; set; }
        public virtual TbQuestion Question { get; set; }
    }
}

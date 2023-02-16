using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_Domians.Models
{
    public partial class TbQuestion
    {
        public TbQuestion()
        {
            TbSurveys = new HashSet<TbSurvey>();
        }

        public int QuestionId { get; set; }
        public string Question { get; set; }

        public virtual ICollection<TbSurvey> TbSurveys { get; set; }
    }
}

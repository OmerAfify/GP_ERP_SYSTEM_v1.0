using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_Domians.Models
{
    public partial class TbInterviewDetail
    {
        public int InterviewId { get; set; }
        public DateTime? InterviewDate { get; set; }
        public bool InterviewResult { get; set; }
        public int RecuriementId { get; set; }

        public virtual TbRecuirement Recuriement { get; set; }
    }
}

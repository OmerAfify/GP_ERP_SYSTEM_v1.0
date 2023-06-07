using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_Domians.Models
{
    public partial class TbRecuirement
    {
        public TbRecuirement()
        {
            TbInterviewDetails = new HashSet<TbInterviewDetail>();
        }

        public int RecuirementId { get; set; }
        public int? RecuirementCode { get; set; }
        public string RecuirementPosition { get; set; }
        public string RecuirementDescription { get; set; }
        public DateTime? RecuirementDate { get; set; }
        public int EmployeeId { get; set; }
        public int HrmanagerId { get; set; }

        public virtual TbEmployeeDetail Employee { get; set; }
        public virtual TbHrmanagerDetail Hrmanager { get; set; }
        public virtual ICollection<TbInterviewDetail> TbInterviewDetails { get; set; }
    }
}

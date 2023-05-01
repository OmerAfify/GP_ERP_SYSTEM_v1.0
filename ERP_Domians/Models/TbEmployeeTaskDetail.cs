using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_Domians.Models
{
    public partial class TbEmployeeTaskDetail
    {
        public int TaskId { get; set; }
        public string TaskDescription { get; set; }
        public DateTime? TaskAssignedTime { get; set; }
        public DateTime? TaskDeadlineTime { get; set; }
        public int? BounsHours { get; set; }
        public int? EmplyeeId { get; set; }

        public virtual TbEmployeeDetail Emplyee { get; set; }
    }
}

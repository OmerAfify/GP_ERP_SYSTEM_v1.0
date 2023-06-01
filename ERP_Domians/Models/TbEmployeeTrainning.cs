using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_Domians.Models
{
    public partial class TbEmployeeTrainning
    {
        public int TrainnningId { get; set; }
        public string TrainningType { get; set; }
        public string TrainningDescription { get; set; }
        public int EmployeeId { get; set; }
        public int HrmangerId { get; set; }

        public virtual TbEmployeeDetail Employee { get; set; }
        public virtual TbHrmanagerDetail Hrmanger { get; set; }
    }
}

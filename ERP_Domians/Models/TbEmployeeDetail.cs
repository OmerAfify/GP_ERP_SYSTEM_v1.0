using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_Domians.Models
{
    public partial class TbEmployeeDetail
    {
        public TbEmployeeDetail()
        {
            TbEmployeeTaskDetails = new HashSet<TbEmployeeTaskDetail>();
            TbEmployeeTrainnings = new HashSet<TbEmployeeTrainning>();
            TbRecuirements = new HashSet<TbRecuirement>();
        }

        public int EmployeeId { get; set; }
        public string EmployeeFullName { get; set; }
        public decimal? TaxWithholding { get; set; }
        public int? HoursWorked { get; set; }
        public string PhotoFileName { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public int HrmanagerId { get; set; }
        public DateTime? AttendenceTime { get; set; }
        public DateTime? Holidays { get; set; }
        public decimal? EmployeeSalary { get; set; }

        public virtual TbHrmanagerDetail Hrmanager { get; set; }
        public virtual ICollection<TbEmployeeTaskDetail> TbEmployeeTaskDetails { get; set; }
        public virtual ICollection<TbEmployeeTrainning> TbEmployeeTrainnings { get; set; }
        public virtual ICollection<TbRecuirement> TbRecuirements { get; set; }
    }
}

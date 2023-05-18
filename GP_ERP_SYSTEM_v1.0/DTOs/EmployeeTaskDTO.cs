using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GP_ERP_SYSTEM_v1._0.DTOs
{
    public class AddEmployeeTaskDTO
    {
        [Required]
        public string TaskDescription { get; set; }

        public DateTime TaskAssignedTime { get; set; }

        public DateTime TaskDeadlineTime { get; set; }
        [Required]
        public int EmplyeeId { get; set; }


    }
    public class EmployeeTaskDTO: AddEmployeeTaskDTO
    {
        public int TaskId { get; set; }
        public string EmployeeFullName { get; set; }
        public int BounsHours { get; set; }

    }
}

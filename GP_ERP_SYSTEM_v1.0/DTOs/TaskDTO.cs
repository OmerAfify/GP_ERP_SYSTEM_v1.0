using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GP_ERP_SYSTEM_v1._0.DTOs
{
    public class AddTaskDTO
    {
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public string TaskName { get; set; }
        [Required]
        public DateTime? TaskDate { get; set; }
        public string TaskDesc { get; set; }
    }
    public class TaskDTO :AddTaskDTO
    {
        public int TaskId { get; set; }
        public string CustomerFullName { get; set; }
        public string CustomerPhone { get; set; }
        public decimal CustomerAge { get; set; }
    }
}

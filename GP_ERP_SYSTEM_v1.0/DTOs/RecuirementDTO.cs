using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GP_ERP_SYSTEM_v1._0.DTOs
{
    public class AddRecuirementDTO
    {
        [Required]
        public string RecuirementPosition { get; set; }
        [Required]
        public string RecuirementDescription { get; set; }
        [Required]
        public DateTime? RecuirementDate { get; set; }
        [Required]
        public int Hrid { get; set; }
    }
    public class RecuirementDTO :AddRecuirementDTO
    {
        public int RecuirementId { get; set; }
        public int EmployeeId { get; set; }
    }
}

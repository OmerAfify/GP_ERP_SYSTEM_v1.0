using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GP_ERP_SYSTEM_v1._0.DTOs
{
    public class AddinterviewDTO
    {
        [Required]
        public DateTime InterviewDate { get; set; }
        [Required]
        public bool InterviewResult { get; set; }
        [Required]
        public int RecuriementId { get; set; }

    }
    public class InterviewDTO: AddinterviewDTO
    {
        public int InterviewId { get; set; }
    }
}

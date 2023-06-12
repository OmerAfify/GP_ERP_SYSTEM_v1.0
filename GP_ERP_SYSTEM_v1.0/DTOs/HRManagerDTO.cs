using System;
using System.ComponentModel.DataAnnotations;

namespace GP_ERP_SYSTEM_v1._0.DTOs
{
    public class AddHRManagerDTO
    {
        [Required(ErrorMessage = "You should insert HR Name")]
        public string HrfullName { get; set; }
        [Required]
        public string Hremail { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Gender { get; set; }
        public decimal Salary { get; set; }
        [Required]
        public string Phone { get; set; }


    }

    public class HRManagerDTO: AddHRManagerDTO
    {
        public int Hrid { get; set; }
    }
}

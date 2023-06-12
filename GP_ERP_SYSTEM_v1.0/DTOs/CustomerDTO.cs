using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GP_ERP_SYSTEM_v1._0.DTOs
{
    public class AddCustomerDTO
    {
        [Required(ErrorMessage = "Please Enter Customer Full Name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Minimum Characters is 3")]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Address { get; set; }
        [Required]
        public string Sex { get; set; } // Enum field
        [Required]
        public decimal Age { get; set; }
       // public byte[] Image { get; set; }
    }

    public class CustomerDTO :AddCustomerDTO
    {
        public int CustomerId { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace GP_ERP_SYSTEM_v1._0.DTOs
{
    public class AddHRManagerDTO
    {
        [Required(ErrorMessage = "You should insert HR Name")]
        public string HrfullName { get; set; }

    }
      
    public class HRManagerDTO: AddHRManagerDTO
    {
        public string hrid { get; set; }
    }
}

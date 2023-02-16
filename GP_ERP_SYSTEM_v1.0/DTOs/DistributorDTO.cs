using System.ComponentModel.DataAnnotations;

namespace GP_ERP_SYSTEM_v1._0.DTOs
{

    public class AddDistributorDTO
    {

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        public string DistributorName { get; set; }

        public string Address { get; set; }

    }

    public class DistributorDTO : AddDistributorDTO
    {
        public int DistributorId { get; set; }

    }

}

using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_Domians.Models
{
    public partial class TbDistributor
    {
        public TbDistributor()
        {

        }

        public int DistributorId { get; set; }
        public string DistributorName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

    }
}

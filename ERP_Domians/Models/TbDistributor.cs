using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_BusinessLogic.Models
{
    public partial class TbDistributor
    {
        public TbDistributor()
        {
            TbDistributionOrders = new HashSet<TbDistributionOrder>();
        }

        public int DistributorId { get; set; }
        public string DistributorName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public virtual ICollection<TbDistributionOrder> TbDistributionOrders { get; set; }
    }
}

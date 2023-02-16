using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_Domians.Models
{
    public partial class TbDistributionOrder
    {
        public TbDistributionOrder()
        {
            TbDistributionOrderDetails = new HashSet<TbDistributionOrderDetail>();
        }

        public int DistributionOrderId { get; set; }
        public int DistributorId { get; set; }
        public int TotalQty { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderingDate { get; set; }
        public DateTime ExpectedArrivalDate { get; set; }
        public int OrderStatus { get; set; }

        public virtual TbDistributor Distributor { get; set; }
        public virtual ICollection<TbDistributionOrderDetail> TbDistributionOrderDetails { get; set; }
    }
}

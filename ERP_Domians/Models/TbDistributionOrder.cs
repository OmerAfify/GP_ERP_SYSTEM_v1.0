using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_Domians.Models
{
    public partial class TbDistributionOrder
    {
        public TbDistributionOrder()
        {

        }

        public TbDistributionOrder(int distributorId, int totalQty, decimal totalPrice, decimal subtotal,
            DateTime expectedArrivalDate, int orderStatusId, IReadOnlyList<TbDistributionOrderDetail> distributionOrderDetails)
        {
            DistributorId = distributorId;
            TotalQty = totalQty;
            SubTotal = subtotal;
            TotalPrice = totalPrice;
            ExpectedArrivalDate = expectedArrivalDate;
            OrderStatusId = orderStatusId;
            DistributionOrderDetails = distributionOrderDetails;
        }

        public int Id { get; set; }
    
        public int DistributorId { get; set; }
        public virtual TbDistributor Distributor { get; set; }


        public int TotalQty { get; set; }

          public decimal SubTotal { get; set; }
        public decimal TotalPrice { get; set; }
        
        public DateTime OrderingDate  { get; set; } = DateTime.UtcNow;
        public DateTime ExpectedArrivalDate { get; set; }

        public int OrderStatusId { get; set; }
        public virtual TbDistributionOrderStatus OrderStatus { get; set; }

        public virtual IReadOnlyList<TbDistributionOrderDetail> DistributionOrderDetails { get; set; }

    }
}

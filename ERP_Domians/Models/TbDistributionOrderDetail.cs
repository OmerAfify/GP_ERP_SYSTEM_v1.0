using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_Domians.Models
{
    public partial class TbDistributionOrderDetail
    {

        public TbDistributionOrderDetail()
        {

        }

        

        public TbDistributionOrderDetail(int productId, int qty, decimal price)
        {
            this.ProductId = productId;
            this.Qty = qty;
            this.Price = price;
        }


        public int DistributionOrderId { get; set; }
        public virtual TbDistributionOrder DistributionOrder { get; set; }

        public int ProductId { get; set; }
        public virtual TbProduct Product { get; set; }

        public int Qty { get; set; }
        public decimal Price { get; set; }
    }
}

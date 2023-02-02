using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_BusinessLogic.Models
{
    public partial class TbDistributionOrderDetail
    {
        public int DistributionOrderId { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }

        public virtual TbDistributionOrder DistributionOrder { get; set; }
        public virtual TbProduct Product { get; set; }
    }
}

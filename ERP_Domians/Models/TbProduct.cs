using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_Domians.Models
{
    public partial class TbProduct
    {
        public TbProduct()
        {
            TbDistributionOrderDetails = new HashSet<TbDistributionOrderDetail>();
       //     TbManufacturingOrders = new HashSet<TbManufacturingOrder>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalesPrice { get; set; }
        public int CategoryId { get; set; }

        public virtual TbCategory Category { get; set; }
        public virtual TbFinishedProductsInventory TbFinishedProductsInventory { get; set; }
        public virtual ICollection<TbDistributionOrderDetail> TbDistributionOrderDetails { get; set; }
      //  public virtual ICollection<TbManufacturingOrder> TbManufacturingOrders { get; set; }
    }
}

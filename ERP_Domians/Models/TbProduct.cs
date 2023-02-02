using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ERP_BusinessLogic.Models
{
    public partial class TbProduct
    {
        public TbProduct()
        {
            TbDistributionOrderDetails = new HashSet<TbDistributionOrderDetail>();
            TbManufacturingOrders = new HashSet<TbManufacturingOrder>();
        }

        [Required]
        public int ProductId { get; set; }
       
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        
        [Required]
        public decimal PurchasePrice { get; set; }
       
        [Required]  
        public decimal SalesPrice { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public virtual TbCategory Category { get; set; }
        public virtual TbFinishedProductsInventory TbFinishedProductsInventory { get; set; }
        public virtual ICollection<TbDistributionOrderDetail> TbDistributionOrderDetails { get; set; }
        public virtual ICollection<TbManufacturingOrder> TbManufacturingOrders { get; set; }
    }
}

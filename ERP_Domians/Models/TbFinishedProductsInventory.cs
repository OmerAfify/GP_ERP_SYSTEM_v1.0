using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_Domians.Models
{
    public partial class TbFinishedProductsInventory
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime? ShippingDate { get; set; }
        public decimal? MonthlyCosts { get; set; }
        public string Area { get; set; }
        public int ReorderingPoint { get; set; }
        public bool HasReachedROP { get; set; } = false;
        public virtual TbProduct Product { get; set; }
    }
}

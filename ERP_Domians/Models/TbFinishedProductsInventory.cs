using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_BusinessLogic.Models
{
    public partial class TbFinishedProductsInventory
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime? ShippingDate { get; set; }
        public decimal? MonthlyCosts { get; set; }
        public string Area { get; set; }
        public int ReorderingPoint { get; set; }

        public virtual TbProduct Product { get; set; }
    }
}

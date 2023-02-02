using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_BusinessLogic.Models
{
    public partial class TbSupplyOrder
    {
        public TbSupplyOrder()
        {
            TbSupplyOrderDetails = new HashSet<TbSupplyOrderDetail>();
        }

        public int SupplyOrderId { get; set; }
        public int SupplierId { get; set; }
        public int TotalQty { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderingDate { get; set; }
        public DateTime ExpectedArrivalDate { get; set; }
        public int OrderStatus { get; set; }

        public virtual TbSupplier Supplier { get; set; }
        public virtual ICollection<TbSupplyOrderDetail> TbSupplyOrderDetails { get; set; }
    }
}

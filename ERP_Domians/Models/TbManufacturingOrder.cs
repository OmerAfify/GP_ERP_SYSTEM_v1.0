using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_Domians.Models
{
    public partial class TbManufacturingOrder
    {
        public TbManufacturingOrder()
        {
            TbManufacturingOrderDetails = new HashSet<TbManufacturingOrderDetail>();
        }

        public int ManufactoringOrderId { get; set; }
        public int ProductManufacturedId { get; set; }
        public string LeadTimePerDays { get; set; }
        public int QtyToProduce { get; set; }

        public virtual TbProduct ProductManufactured { get; set; }
        public virtual ICollection<TbManufacturingOrderDetail> TbManufacturingOrderDetails { get; set; }
    }
}

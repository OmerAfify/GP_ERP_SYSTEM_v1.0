using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_Domians.Models
{
    public partial class TbSupplyOrderDetail
    {
        public int SupplyOrderId { get; set; }
        public int MaterialId { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }

        public virtual TbRawMaterial Material { get; set; }
        public virtual TbSupplyOrder SupplyOrder { get; set; }
    }
}

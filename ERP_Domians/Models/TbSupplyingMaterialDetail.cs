using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_Domians.Models
{
    public partial class TbSupplyingMaterialDetail
    {
        public int SupplierId { get; set; }
        public int MaterialId { get; set; }
        public decimal PricePerUnit { get; set; }
 
        public virtual TbRawMaterial Material { get; set; }
        public virtual TbSupplier Supplier { get; set; }
    }
}

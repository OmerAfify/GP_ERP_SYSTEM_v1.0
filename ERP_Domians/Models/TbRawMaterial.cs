using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_Domians.Models
{
    public partial class TbRawMaterial
    {
        public TbRawMaterial()
        {
           TbManufacturingOrderDetails = new HashSet<TbManufacturingOrderDetail>();
            TbSupplyingMaterialDetails = new HashSet<TbSupplyingMaterialDetail>();
        }

        public int MaterialId { get; set; }
        public string MaterialName { get; set; }
        public string MaterialDescription { get; set; }

        public virtual TbRawMaterialsInventory TbRawMaterialsInventory { get; set; }
        public virtual ICollection<TbManufacturingOrderDetail> TbManufacturingOrderDetails { get; set; }
        public virtual ICollection<TbSupplyingMaterialDetail> TbSupplyingMaterialDetails { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_Domians.Models
{
    public partial class TbManufacturingOrderDetail
    {
        public TbManufacturingOrderDetail()
        {

        }


        public TbManufacturingOrderDetail(
            int rawMaterialId, int rawMaterialQtyUsed)
        {
            RawMaterialId = rawMaterialId;
            RawMaterialQtyUsed = rawMaterialQtyUsed;
        }


        public int ManfactoringOrderId { get; set; }
         public virtual TbManufacturingOrder ManfactoringOrder { get; set; }

        public int RawMaterialId { get; set; }
        public virtual TbRawMaterial RawMaterial { get; set; }

        public int RawMaterialQtyUsed { get; set; }

    }
}

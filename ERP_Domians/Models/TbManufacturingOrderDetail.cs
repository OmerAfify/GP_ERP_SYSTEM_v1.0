using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_BusinessLogic.Models
{
    public partial class TbManufacturingOrderDetail
    {
        public int ManfactoringOrderId { get; set; }
        public int RawMaterialId { get; set; }
        public int RawMaterialQtyUsed { get; set; }

        public virtual TbManufacturingOrder ManfactoringOrder { get; set; }
        public virtual TbRawMaterial RawMaterial { get; set; }
    }
}

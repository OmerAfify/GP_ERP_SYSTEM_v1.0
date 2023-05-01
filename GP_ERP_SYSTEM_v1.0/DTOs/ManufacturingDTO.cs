using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP_Domians.Models.HelpersProperties;

namespace GP_ERP_SYSTEM_v1._0.DTOs
{


    public class GeneralManufacturingOrderDTO {
        public int ProductManufacturedId { get; set; }
        public int QtyToManufacture { get; set; }
        public decimal ManufacturingCost { get; set; }
        public DateTime StartingDate { get; set; }
    }


    public class CreateManufacturingOrderDTO : GeneralManufacturingOrderDTO
    {
        public List<MaterialsOrderedParmeters> RawMaterialsUsed { get; set; }

    }

    public class ReturnedManufacturingOrderDTO : GeneralManufacturingOrderDTO
    {
        public int Id { get; set; }
        public string ProductManufacturedName { get; set; }
        public DateTime FinishingDate { get; set; }
        public string ManufacturingStatus { get; set; }

        public IReadOnlyList<ManufactoringOrderDetailsDTO> ManufacturingOrderDetails { get; set; }

    }


    public class ManufactoringOrderDetailsDTO{
        public int RawMaterialId { get; set; }
        public string RawMaterialName { get; set; }
        public int RawMaterialQtyUsed { get; set; }

}


}

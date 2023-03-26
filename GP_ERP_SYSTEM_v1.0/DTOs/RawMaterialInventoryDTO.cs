using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GP_ERP_SYSTEM_v1._0.DTOs
{
    public class AddRawMaterialInventoryDTO
    {
        public int MaterialId { get; set; }
        public int Quantity { get; set; }
        public DateTime ShippingDate { get; set; }
        public decimal MonthlyCosts { get; set; }
        public string Area { get; set; }
        public int ReorderingPoint { get; set; }
        public bool HasReachedROP { get; set; }


    }
    
    public class RawMaterialInventoryDTO : AddRawMaterialInventoryDTO
    {
        public string MaterialName { get; set; }
        public string MaterialDescription { get; set; }

    }
}

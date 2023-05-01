using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GP_ERP_SYSTEM_v1._0.DTOs
{
    public class SupplyingMaterialDetailDTO
    {      
        public int MaterialId { get; set; }
        public decimal PricePerUnit { get; set; }

    }


    public class ReturnedSupplyingMaterialDetailDTO : SupplyingMaterialDetailDTO
    {
        public string MaterialName { get; set; }


    }




}

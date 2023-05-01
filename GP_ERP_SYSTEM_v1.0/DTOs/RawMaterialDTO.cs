using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GP_ERP_SYSTEM_v1._0.DTOs
{
    public class AddRawMaterialDTO
    {
        public string MaterialName { get; set; }
        public string MaterialDescription { get; set; }

    }
    public class RawMaterialDTO : AddRawMaterialDTO
    {
        public int MaterialId { get; set; }
     
    }
}

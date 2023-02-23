using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Domians.Models.OwnedProperties
{
    public class OrderedRawMaterialDetails
    {
        public OrderedRawMaterialDetails()
        {

        }

        public OrderedRawMaterialDetails(int materialId, string materialName, decimal salesPrice)
        {
            MaterialId = materialId;
            MaterialName = materialName;
            SalesPrice = salesPrice;
        }

        public int MaterialId { get; set; }
        public string MaterialName { get; set; }
        public decimal SalesPrice { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_Domians.Models
{
    public partial class TbSupplier
    {
        public TbSupplier()
        {
            TbSupplyingMaterialDetails = new HashSet<TbSupplyingMaterialDetail>();
        }

        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierDescription { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public int AdverageDeliveryTimeInDays { get; set; }

       public virtual ICollection<TbSupplyingMaterialDetail> TbSupplyingMaterialDetails { get; set; }
    }
}

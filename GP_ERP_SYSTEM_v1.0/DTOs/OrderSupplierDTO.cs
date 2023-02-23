using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GP_ERP_SYSTEM_v1._0.DTOs
{
    public class OrderSupplierDTO
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }

        public int TotalQty { get; set; }
        public decimal SubTotalPrice { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal TotalPrice { get; set; }

        public int OrderStatusId { get; set; }
        public string OrderStatus { get; set; }

        public DateTime OrderingDate { get; set; }
        public DateTime ExpectedArrivalDate { get; set; }

        public IReadOnlyList<OrderDetailsSupplierDTO> OrderedMaterials { get; set; }


    }
}

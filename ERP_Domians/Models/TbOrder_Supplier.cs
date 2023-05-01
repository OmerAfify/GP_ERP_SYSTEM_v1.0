using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Domians.Models
{
    public class TbOrder_Supplier
    {
        
        public TbOrder_Supplier()
        {

        }

       

        public TbOrder_Supplier(int supplierId, int totalQty,  decimal subTotalPrice, decimal shippingCost, decimal totalPrice,
                                int orderStatusId, DateTime expectedArrivalDate, 
                                IReadOnlyList<TbOrderDetails_Supplier> orderedMaterials)
        {
            SupplierId = supplierId;
            TotalQty = totalQty;
            SubTotalPrice = subTotalPrice;
            ShippingCost = shippingCost;
            TotalPrice = totalPrice;
            OrderStatusId = orderStatusId;
            ExpectedArrivalDate = expectedArrivalDate;
            OrderedMaterials = orderedMaterials;
        }

        public int Id { get; set; }

        public int SupplierId { get; set; }
        public TbSupplier Supplier { get; set; }

        public int TotalQty { get; set; }
        public decimal SubTotalPrice { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal TotalPrice { get; set; }

        public int OrderStatusId { get; set; }
        public TbOrderStatus_Supplier OrderStatus { get; set; }


        public DateTime OrderingDate { get; set; } = DateTime.UtcNow;
        public DateTime ExpectedArrivalDate { get; set; }

        public IReadOnlyList<TbOrderDetails_Supplier> OrderedMaterials { get; set; }

    }
}

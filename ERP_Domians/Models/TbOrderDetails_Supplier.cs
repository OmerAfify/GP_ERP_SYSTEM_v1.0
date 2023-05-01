using ERP_Domians.Models.OwnedProperties;

namespace ERP_Domians.Models
{
    public class TbOrderDetails_Supplier
    {
        public TbOrderDetails_Supplier()
        {

        }

        public TbOrderDetails_Supplier(OrderedRawMaterialDetails orderedRawMaterials, int quantity, decimal price)
        {
            OrderedRawMaterials = orderedRawMaterials;
            Quantity = quantity;
            Price = price;
        }

        public int Id { get; set; }
        public OrderedRawMaterialDetails OrderedRawMaterials { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
namespace GP_ERP_SYSTEM_v1._0.DTOs
{
    public class OrderDetailsSupplierDTO
    {
        public int Id { get; set; }

        public int MaterialId { get; set; }
        public string MaterialName { get; set; }
        public decimal SalesPrice { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }

    }
}
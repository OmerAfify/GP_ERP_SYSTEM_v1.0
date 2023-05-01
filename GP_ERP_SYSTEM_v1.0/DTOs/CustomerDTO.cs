namespace GP_ERP_SYSTEM_v1._0.DTOs
{
    public class AddCustomerDTO
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public decimal? Phone { get; set; }
        public string Address { get; set; }
        public bool? Sex { get; set; }
        public decimal? Age { get; set; }
        public byte[] Image { get; set; }
    }
    public class CustomerDTO : AddCustomerDTO
    {
        public int CustomerId { get; set; }
    }
}

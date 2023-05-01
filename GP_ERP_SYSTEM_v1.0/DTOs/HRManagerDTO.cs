namespace GP_ERP_SYSTEM_v1._0.DTOs
{
    public class AddHRManagerDTO
    {
        public string HrfullName { get; set; }
        public string Hrpassword { get; set; }

    }
    public class HRManagerDTO: AddHRManagerDTO
    {
        public string hrid { get; set; }
    }
}

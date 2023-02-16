namespace GP_ERP_SYSTEM_v1._0.DTOs
{
    public class AddCategoryDTO
    {
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }

    }

    public class CategoryDTO : AddCategoryDTO
    {

        public int CategoryId { get; set; }
    

    }
}

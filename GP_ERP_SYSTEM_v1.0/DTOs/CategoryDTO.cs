using System.ComponentModel.DataAnnotations;

namespace GP_ERP_SYSTEM_v1._0.DTOs
{
    public class AddCategoryDTO
    {
       [Required]
        public string CategoryName { get; set; }

        [Required]
        public string CategoryDescription { get; set; }

    }

    public class CategoryDTO : AddCategoryDTO
    {

        [Required]
        public int CategoryId { get; set; }
    

    }
}

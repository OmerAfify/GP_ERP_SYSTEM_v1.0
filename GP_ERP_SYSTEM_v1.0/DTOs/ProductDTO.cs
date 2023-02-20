using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GP_ERP_SYSTEM_v1._0.DTOs
{
  

    public class AddProductDTO 
    {
        [Required]
        public string ProductName { get; set; }
        
        [Required]
        public string ProductDescription { get; set; }
        
        [Required]
        [Range(0.1,double.MaxValue,ErrorMessage ="value can not be 0 or less")]
        public decimal PurchasePrice { get; set; }


        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "value can not be 0 or less")] public decimal SalesPrice { get; set; }
        
        [Required]
        public int CategoryId { get; set; }

    }

    public class ProductDTO : AddProductDTO
    {

        public int ProductId { get; set; }

        public string CategoryName { get; set; }
    }


}

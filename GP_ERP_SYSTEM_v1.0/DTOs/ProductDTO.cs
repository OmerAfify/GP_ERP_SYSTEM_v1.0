using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GP_ERP_SYSTEM_v1._0.DTOs
{
    public class ProductDTO : AddProductDTO
    {
        
        public int ProductId { get; set; }
   
        public string CategoryName { get; set; }
    }

    public class AddProductDTO 
    {
        public string? ProductName { get; set; }
        
        public string? ProductDescription { get; set; }
        
        [Required]
        public decimal PurchasePrice { get; set; }
        [Required]
        public decimal SalesPrice { get; set; }
        
        [Required]
        public int CategoryId { get; set; }

    }


}

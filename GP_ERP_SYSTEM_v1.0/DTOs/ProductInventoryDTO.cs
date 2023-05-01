using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GP_ERP_SYSTEM_v1._0.DTOs
{


    public class AddProductInventoryDTO
    {
        [Required]
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime ShippingDate { get; set; }
        public decimal MonthlyCosts { get; set; }
        public string Area { get; set; }
        public int ReorderingPoint { get; set; }
        public bool HasReachedROP { get; set; }



    }

    public class ProductInventoryDTO : AddProductInventoryDTO
    {
     
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
      
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP_Domians.Models.HelpersProperties;

namespace GP_ERP_SYSTEM_v1._0.DTOs
{

    public class DistributionOrderDTO
    {
        public int DistributorId { get; set; }
      
    }

    public class CreateDistributionOrderDTO : DistributionOrderDTO
    {
        public List<OrderedFinishedProductParameters> ProductsOrdered { get; set; }
    }


    public class ReturnedDistributionOrderDTO : DistributionOrderDTO
    {
        public int Id { get; set; }

        public string DistributorName { get; set; }
        public int OrderStatusId { get; set; }
        public string OrderStatus { get; set; } 

        public int TotalQty { get; set; }

        public decimal SubTotal { get; set; }
        public decimal TotalPrice { get; set; }

        public DateTime OrderingDate { get; set; } = DateTime.UtcNow;
        public DateTime ExpectedArrivalDate { get; set; }

        public virtual IReadOnlyList<DistributionOrderDetailsDTO> DistributionOrderDetails { get; set; }


    }


    public class DistributionOrderDetailsDTO
    {

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductSalesPrice { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }

    }

}

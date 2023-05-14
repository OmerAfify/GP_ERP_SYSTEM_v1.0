using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP_Domians.Models;
using ERP_Domians.Models.HelpersProperties;

namespace ERP_Domians.IServices
{
    public interface IDistributionOrderService
    {
     public Task<TbDistributionOrder> CreateDistributionOrder(int DistributorId,
                                                                List<OrderedFinishedProductParameters> orderedProducts );

    }
}

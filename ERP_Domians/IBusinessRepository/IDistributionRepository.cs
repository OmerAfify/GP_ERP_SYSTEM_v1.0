using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.Interfaces.IGenericRepository;
using ERP_Domians.Models;

namespace ERP_Domians.IBusinessRepository
{
    public interface IDistributionRepository : IGenericRepository<TbDistributionOrder>
    {
        public Task<IEnumerable<TbDistributionOrder>> GetAllDistributionOrders();
        public Task<TbDistributionOrder> GetDistributionOrderById(int id);

    }
}

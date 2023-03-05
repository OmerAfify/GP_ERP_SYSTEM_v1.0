using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinesssLogic.Repository.GenericRepository;
using ERP_BusinessLogic.Context;
using ERP_Domians.IBusinessRepository;
using ERP_Domians.Models;
using Microsoft.EntityFrameworkCore;

namespace ERP_BusinessLogic.Repository.BusinessRepository
{
    public class DistributionRepository : GenericRepository<TbDistributionOrder>, IDistributionRepository
    {

        private readonly DbContext _context;
        public DistributionRepository(DbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TbDistributionOrder>> GetAllDistributionOrders()
        {
            return await _context.Set<TbDistributionOrder>()
             .Include(d => d.OrderStatus)
             .Include(d => d.Distributor)
             .Include(d => d.DistributionOrderDetails).ThenInclude(d => d.Product).ToListAsync();
        }

        public async Task<TbDistributionOrder> GetDistributionOrderById(int id)
        {
            return await _context.Set<TbDistributionOrder>()
                .Include(d => d.OrderStatus)
                .Include(d => d.Distributor)
                .Include(d => d.DistributionOrderDetails).ThenInclude(d => d.Product).FirstOrDefaultAsync(i=>i.Id==id);
        }
    }
}

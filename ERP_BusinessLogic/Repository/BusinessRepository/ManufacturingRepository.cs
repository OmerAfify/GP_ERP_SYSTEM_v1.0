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
    public class ManufacturingRepository : GenericRepository<TbManufacturingOrder>, IManufactoringRepository
    {

        private readonly DbContext _context;
        public ManufacturingRepository(DbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TbManufacturingOrder>> GetAllManufacturingOrders()
        {
            return await _context.Set<TbManufacturingOrder>()
                .Include(m => m.ProductManufactured)
                .Include(m => m.ManufacturingStatus)
                .Include(m => m.ManufacturingOrderDetails).ThenInclude(m => m.RawMaterial)
                .OrderByDescending(m=>m.StartingDate).ToListAsync();
        }

        public async Task<TbManufacturingOrder> GetManufacturingOrderById(int id)
        {

            return await _context.Set<TbManufacturingOrder>()
                .Include(m => m.ProductManufactured)
                .Include(m => m.ManufacturingStatus)
                .Include(m => m.ManufacturingOrderDetails).ThenInclude(m => m.RawMaterial)
                .Where(i => i.Id == id).FirstOrDefaultAsync();

        }
    }
}

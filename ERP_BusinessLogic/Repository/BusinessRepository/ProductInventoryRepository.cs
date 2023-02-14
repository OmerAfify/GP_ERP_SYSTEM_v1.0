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
    public class ProductInventoryRepository : GenericRepository<TbFinishedProductsInventory>, IProductsInventoryRepository
    {

        private readonly DbContext _context;
        public ProductInventoryRepository(DbContext context) : base(context)
        {
            _context = context;
        }

        public  async Task<IEnumerable<TbFinishedProductsInventory>> GetAllProductsInventoryWithProductAndCategoryDetails()
        {
            
          return  await _context.Set<TbFinishedProductsInventory>().Include(p => p.Product).ThenInclude(c => c.Category).ToListAsync();
        }
        
        public  async Task<TbFinishedProductsInventory> GetProductInventoryWithProductAndCategoryDetails(int id)
        {
            
          return  await _context.Set<TbFinishedProductsInventory>().Where(p=>p.ProductId==id).Include(p => p.Product).ThenInclude(c => c.Category).FirstOrDefaultAsync();
        }
    }
}

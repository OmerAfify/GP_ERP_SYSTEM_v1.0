using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.Interfaces.IGenericRepository;
using ERP_Domians.Models;

namespace ERP_Domians.IBusinessRepository
{
    public interface IProductsInventoryRepository : IGenericRepository<TbFinishedProductsInventory>
    {
        public Task<IEnumerable<TbFinishedProductsInventory>> GetAllProductsInventoryWithProductAndCategoryDetails();
        public Task<TbFinishedProductsInventory> GetProductInventoryWithProductAndCategoryDetails(int id);
       
    }
}

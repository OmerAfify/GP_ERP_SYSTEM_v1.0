using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.Interfaces.IGenericRepository;
using ERP_Domians.Models;

namespace ERP_Domians.IBusinessRepository
{
    public interface IManufactoringRepository : IGenericRepository<TbManufacturingOrder>
    {
        public Task<IEnumerable<TbManufacturingOrder>> GetAllManufacturingOrders();
        public Task<TbManufacturingOrder> GetManufacturingOrderById(int id);
       
    }
}

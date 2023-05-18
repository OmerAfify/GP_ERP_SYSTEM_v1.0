using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.Interfaces.IGenericRepository;
using ERP_Domians.Models;

namespace ERP_Domians.IBusinessRepository
{
    public interface ITrainningEmployeeRepository : IGenericRepository<TbEmployeeTrainning>
    {
        public Task<IEnumerable<TbEmployeeTrainning>> GetAllEmployeeTrainningWithEmployeeeAndHRManager();

        public Task<TbEmployeeTrainning> GetEmployeeTrainningWithEmployeeAndHRMangerById(int id);
    }
}
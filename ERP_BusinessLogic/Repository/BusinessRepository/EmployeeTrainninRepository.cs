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
    public class EmployeeTrainninRepository : GenericRepository<TbEmployeeTrainning>, ITrainningEmployeeRepository
    {

        private readonly DbContext _context;
        public EmployeeTrainninRepository(DbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TbEmployeeTrainning>> GetAllEmployeeTrainningWithEmployeeeAndHRManager()
        {
            return await _context.Set<TbEmployeeTrainning>().Include(e => e.Employee).ThenInclude(hr => hr.Hrmanager).ToListAsync();
        }



        public async Task<TbEmployeeTrainning> GetEmployeeTrainningWithEmployeeeAndHRManager(int id)
        {
            return  await _context.Set<TbEmployeeTrainning>().Where(T => T.TrainnningId == id).Include(e => e.Employee).ThenInclude(hr => hr.Hrmanager).FirstOrDefaultAsync();
        }


    }


}

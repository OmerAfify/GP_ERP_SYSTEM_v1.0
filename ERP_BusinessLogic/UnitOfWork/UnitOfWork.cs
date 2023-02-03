
using System.Threading.Tasks;
using BusinesssLogic.Repository.GenericRepository;
using Domains.Interfaces.IGenericRepository;
using Domains.Interfaces.IUnitOfWork;
using ERP_BusinessLogic.Context;
using ERP_BusinessLogic.Models;

namespace BusinessLogic.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IGenericRepository<TbProduct> Product { get; private set; }
        public IGenericRepository<TbCategory> Category { get; private set; }
        public IGenericRepository<TbRawMaterial> RawMaterial { get; private set; }
        public IGenericRepository<TbSupplier> Supplier { get; private set; }
        public IGenericRepository<TbSupplyingMaterialDetail> SupplingMaterialDetails { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
 
            Product = new GenericRepository<TbProduct>(_context);
            Category = new GenericRepository<TbCategory>(_context);
            RawMaterial= new GenericRepository<TbRawMaterial>(_context);
            Supplier= new GenericRepository<TbSupplier>(_context);
            SupplingMaterialDetails = new GenericRepository<TbSupplyingMaterialDetail>(_context);

        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

    }
}

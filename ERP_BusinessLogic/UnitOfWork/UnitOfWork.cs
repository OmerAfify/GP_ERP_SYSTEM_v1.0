
using System.Threading.Tasks;
using BusinesssLogic.Repository.GenericRepository;
using Domains.Interfaces.IGenericRepository;
using Domains.Interfaces.IUnitOfWork;
using ERP_BusinessLogic.Context;
using ERP_Domians.Models;

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
        public IGenericRepository<TbFmsCategory> FmsCategory { get; private set; }
        public IGenericRepository<TbFmsAccount> FmsAccount { get; private set; }
        public IGenericRepository<TbFmsAccCat> FmsAccCat { get; private set; }
        public IGenericRepository<TbFmsStatement> FmsStatement { get; private set; }
        public IGenericRepository<TbFmsStatementTemplate> FmsStatementTemplate { get; private set; }
        public IGenericRepository<TbFmsStatementAccount> FmsStatementAccount { get; private set; }
        public IGenericRepository<TbFmsTemplateAccount> FmsTemplateAccount { get; private set; }
        public IGenericRepository<TbFmsJournalEntry> FmsJournalEntry { get; private set; }


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Product = new GenericRepository<TbProduct>(_context);
            Category = new GenericRepository<TbCategory>(_context);
            RawMaterial = new GenericRepository<TbRawMaterial>(_context);
            Supplier = new GenericRepository<TbSupplier>(_context);
            SupplingMaterialDetails = new GenericRepository<TbSupplyingMaterialDetail>(_context);
            FmsCategory = new GenericRepository<TbFmsCategory>(_context);
            FmsAccount = new GenericRepository<TbFmsAccount>(_context);
            FmsAccCat = new GenericRepository<TbFmsAccCat>(_context);
            FmsJournalEntry = new GenericRepository<TbFmsJournalEntry>(_context);
            FmsStatementTemplate = new GenericRepository<TbFmsStatementTemplate>(_context);
            FmsStatement = new GenericRepository<TbFmsStatement>(_context);
            FmsStatementAccount = new GenericRepository<TbFmsStatementAccount>(_context);
            FmsTemplateAccount = new GenericRepository<TbFmsTemplateAccount>(_context);
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
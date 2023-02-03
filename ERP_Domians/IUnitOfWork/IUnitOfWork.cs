using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.Interfaces.IGenericRepository;
using ERP_BusinessLogic.Models;

namespace Domains.Interfaces.IUnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TbProduct> Product { get; }
        IGenericRepository<TbCategory> Category { get; }
        IGenericRepository<TbRawMaterial> RawMaterial { get; }
        IGenericRepository<TbSupplier> Supplier { get; }
        IGenericRepository<TbSupplyingMaterialDetail> SupplingMaterialDetails { get; }
        

        public Task Save();
    }
}

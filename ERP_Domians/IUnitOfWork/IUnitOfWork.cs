using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.Interfaces.IGenericRepository;
using ERP_Domians.IBusinessRepository;
using ERP_Domians.Models;

namespace Domains.Interfaces.IUnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        //SCM
        IGenericRepository<TbProduct> Product { get; }
        IGenericRepository<TbCategory> Category { get; }
        IGenericRepository<TbRawMaterial> RawMaterial { get; }
        IGenericRepository<TbSupplier> Supplier { get; }
        IGenericRepository<TbSupplyingMaterialDetail> SupplingMaterialDetails { get; }
        IProductsInventoryRepository ProductsInventory { get; }
        IGenericRepository<TbRawMaterialsInventory> RawMaterialInventory { get; }
        IGenericRepository<TbDistributor> Distributor { get; }
        IGenericRepository<TbOrder_Supplier> OrderSupplier { get; }
        IManufactoringRepository Manufacturing { get; }
        IDistributionRepository Distribution { get; }
       

        //FMS
        IGenericRepository<TbFmsCategory> FmsCategory { get; }
        IGenericRepository<TbFmsAccount> FmsAccount { get; }
        IGenericRepository<TbFmsStatement> FmsStatement { get; }
        IGenericRepository<TbFmsStatementTemplate> FmsStatementTemplate { get; }
        IGenericRepository<TbFmsStatementAccount> FmsStatementAccount { get; }
        IGenericRepository<TbFmsTemplateAccount> FmsTemplateAccount { get; }
        IGenericRepository<TbFmsJournalEntry> FmsJournalEntry { get; }
        IGenericRepository<TbFmsAccCat> FmsAccCat { get; }

        //HRMS
        IGenericRepository<TbEmployeeDetail> Employee { get; }
  
        IGenericRepository<TbHrmanagerDetail> Hrmanager { get; }
        IGenericRepository<TbEmployeeTrainning> TrainningEmployee { get; }
        IGenericRepository<TbEmployeeTaskDetail> EmployeeTask { get; }
        IGenericRepository<TbRecuirement> Recuiremnet { get; }
        IGenericRepository<TbInterviewDetail> Interview { get; }
        
        //CRM
        IGenericRepository<TbCustomer> Customer { get; }
        IGenericRepository<TbTask> Task { get; }
        IGenericRepository<TbToDoList> ToDoList { get; }
        public Task<int> Save();
    }
}
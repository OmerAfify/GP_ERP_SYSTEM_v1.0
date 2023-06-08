
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinesssLogic.Repository.GenericRepository;
using Domains.Interfaces.IGenericRepository;
using Domains.Interfaces.IUnitOfWork;
using ERP_BusinessLogic.Context;
using ERP_BusinessLogic.Repository.BusinessRepository;
using ERP_Domians.IBusinessRepository;
using ERP_Domians.Models;

namespace BusinessLogic.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

    
       
        //SCM
        public IGenericRepository<TbProduct> Product { get; private set; }
        public IGenericRepository<TbCategory> Category { get; private set; }
        public IGenericRepository<TbRawMaterial> RawMaterial { get; private set; }
        public IGenericRepository<TbSupplier> Supplier { get; private set; }
        public IGenericRepository<TbSupplyingMaterialDetail> SupplingMaterialDetails { get; private set; }
        public IProductsInventoryRepository ProductsInventory { get; private set; }
        public IGenericRepository<TbRawMaterialsInventory> RawMaterialInventory { get; private set; }
        public IGenericRepository<TbDistributor> Distributor { get; private set; }
        public IGenericRepository<TbOrder_Supplier> OrderSupplier { get; private set; }

        public IManufactoringRepository Manufacturing { get; private set; }
        public IDistributionRepository Distribution { get; private set; }
        


        //FMS
        public IGenericRepository<TbFmsCategory> FmsCategory { get; private set; }
        public IGenericRepository<TbFmsAccount> FmsAccount { get; private set; }
        public IGenericRepository<TbFmsAccCat> FmsAccCat { get; private set; }
        public IGenericRepository<TbFmsStatement> FmsStatement { get; private set; }
        public IGenericRepository<TbFmsStatementTemplate> FmsStatementTemplate { get; private set; }
        public IGenericRepository<TbFmsStatementAccount> FmsStatementAccount { get; private set; }
        public IGenericRepository<TbFmsTemplateAccount> FmsTemplateAccount { get; private set; }
        public IGenericRepository<TbFmsJournalEntry> FmsJournalEntry { get; private set; }

        //HRMS
        public IGenericRepository<TbEmployeeDetail> Employee { get; private set; }
        public IGenericRepository<TbEmployeeTrainning> TrainningEmployee { get; private set; }
        public IGenericRepository<TbEmployeeTaskDetail> EmployeeTask { get; private set; }
        public IGenericRepository<TbInterviewDetail> Interview { get; private set; }
        public IGenericRepository<TbRecuirement> Recuiremnet { get; private set; }
        public IGenericRepository<TbHrmanagerDetail> Hrmanager { get; private set; }

        //CRM
        public IGenericRepository<TbCustomer> Customer { get; private set; }

        public IGenericRepository<TbTask> Task { get; private set; }

        public IGenericRepository<TbToDoList> ToDoList { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            //SCM
            Product = new GenericRepository<TbProduct>(_context);
            Category = new GenericRepository<TbCategory>(_context);
            RawMaterial = new GenericRepository<TbRawMaterial>(_context);
            Supplier = new GenericRepository<TbSupplier>(_context);
            SupplingMaterialDetails = new GenericRepository<TbSupplyingMaterialDetail>(_context);
            ProductsInventory = new ProductInventoryRepository(_context);
            RawMaterialInventory = new GenericRepository<TbRawMaterialsInventory>(_context);
            Distributor = new GenericRepository<TbDistributor>(_context);
            OrderSupplier = new GenericRepository<TbOrder_Supplier>(_context);

            Manufacturing = new ManufacturingRepository(_context);
            Distribution = new DistributionRepository(_context);
            


            //FMS
            FmsCategory = new GenericRepository<TbFmsCategory>(_context);
            FmsAccount = new GenericRepository<TbFmsAccount>(_context);
            FmsAccCat = new GenericRepository<TbFmsAccCat>(_context);
            FmsJournalEntry = new GenericRepository<TbFmsJournalEntry>(_context);
            FmsStatementTemplate = new GenericRepository<TbFmsStatementTemplate>(_context);
            FmsStatement = new GenericRepository<TbFmsStatement>(_context);
            FmsStatementAccount = new GenericRepository<TbFmsStatementAccount>(_context);
            FmsTemplateAccount = new GenericRepository<TbFmsTemplateAccount>(_context);


            //HRMS
            Employee = new GenericRepository<TbEmployeeDetail>(_context);
            TrainningEmployee = new GenericRepository<TbEmployeeTrainning>(_context);
            EmployeeTask = new GenericRepository<TbEmployeeTaskDetail>(_context);
            Recuiremnet = new GenericRepository<TbRecuirement>(_context);
            Interview = new GenericRepository<TbInterviewDetail>(_context);
            Hrmanager = new GenericRepository<TbHrmanagerDetail>(_context);

            //CRM 
            Customer = new GenericRepository<TbCustomer>(_context);
            Task = new GenericRepository<TbTask>(_context);
            ToDoList = new GenericRepository<TbToDoList>(_context);

        }

        public void Dispose()
        {   
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }

    }
}
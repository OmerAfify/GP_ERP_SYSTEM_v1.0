using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ERP_Domians.Models;
using GP_ERP_SYSTEM_v1._0.DTOs;

namespace GP_ERP_SYSTEM_v1._0.Helpers.AutomapperProfile
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {

            //SCM DTOs
            CreateMap<TbProduct, AddProductDTO>().ReverseMap();

            CreateMap<TbProduct, ProductDTO>()
                .ForMember(d => d.CategoryName, opt => opt.MapFrom(s => s.Category.CategoryName)).ReverseMap();

            CreateMap<TbCategory, CategoryDTO>().ReverseMap();
            CreateMap<TbCategory, AddCategoryDTO>().ReverseMap();

            CreateMap<TbRawMaterial, AddRawMaterialDTO>().ReverseMap();
            CreateMap<TbRawMaterial, RawMaterialDTO>().ReverseMap();


            CreateMap<TbSupplier, AddSupplierDTO>().ReverseMap();
            CreateMap<TbSupplier, SupplierDTO>().ReverseMap();



            CreateMap<TbFinishedProductsInventory, AddProductInventoryDTO>().ReverseMap();
          
            CreateMap<TbFinishedProductsInventory, ProductInventoryDTO>()
            .ForMember(d => d.ProductName, opt => opt.MapFrom(s => s.Product.ProductName))
            .ForMember(d=>d.CategoryId, opt=>opt.MapFrom(s=>s.Product.Category.CategoryId))
            .ForMember(d=>d.CategoryName, opt=>opt.MapFrom(s=>s.Product.Category.CategoryName)).ReverseMap();


            CreateMap<TbRawMaterialsInventory, AddRawMaterialInventoryDTO>().ReverseMap();

            CreateMap<TbRawMaterialsInventory, RawMaterialInventoryDTO>()
            .ForMember(d => d.MaterialName, opt => opt.MapFrom(s => s.Material.MaterialName))
            .ForMember(d => d.MaterialDescription, opt => opt.MapFrom(s => s.Material.MaterialDescription));


            CreateMap<TbSupplyingMaterialDetail, SupplyingMaterialDetailDTO>().ReverseMap();

            CreateMap<TbSupplyingMaterialDetail, ReturnedSupplyingMaterialDetailDTO>()
                .ForMember(d => d.MaterialName, opt => opt.MapFrom(s => s.Material.MaterialName)).ReverseMap();


            CreateMap<TbOrder_Supplier, OrderSupplierDTO>()
                .ForMember(d => d.SupplierName, opt => opt.MapFrom(s => s.Supplier.SupplierName))
                .ForMember(d => d.OrderStatus, opt => opt.MapFrom(s => s.OrderStatus.OrderStatusName));


            CreateMap<TbOrderDetails_Supplier, OrderDetailsSupplierDTO>()
                .ForMember(d => d.MaterialId, opt => opt.MapFrom(s => s.OrderedRawMaterials.MaterialId))
                .ForMember(d => d.MaterialName, opt => opt.MapFrom(s => s.OrderedRawMaterials.MaterialName))
                .ForMember(d => d.SalesPrice, opt => opt.MapFrom(s => s.OrderedRawMaterials.SalesPrice));

            CreateMap<TbDistributor, DistributorDTO>().ReverseMap();
            CreateMap<TbDistributor, AddDistributorDTO>().ReverseMap();




            CreateMap<TbManufacturingOrder, ReturnedManufacturingOrderDTO>()
                .ForMember(d => d.ProductManufacturedName, opt => opt.MapFrom(s => s.ProductManufactured.ProductName))
                .ForMember(d => d.ManufacturingStatus, opt => opt.MapFrom(s => s.ManufacturingStatus.statusName));

        
            CreateMap<TbManufacturingOrderDetail, ManufactoringOrderDetailsDTO>()
                .ForMember(d => d.RawMaterialName, opt => opt.MapFrom(s => s.RawMaterial.MaterialName));




            CreateMap<TbDistributionOrder, ReturnedDistributionOrderDTO>()
                .ForMember(d => d.OrderStatus, opt => opt.MapFrom(s => s.OrderStatus.Status))
                .ForMember(d => d.DistributorName, opt => opt.MapFrom(s => s.Distributor.DistributorName));



                CreateMap<TbDistributionOrderDetail, DistributionOrderDetailsDTO>()
                .ForMember(d => d.ProductName, opt => opt.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.ProductSalesPrice, opt => opt.MapFrom(s => s.Product.SalesPrice));



            //FMS DTOs

            CreateMap<TbFmsAccount, AddFmsAccountDTO>().ReverseMap()
                .ForMember(src => src.IncreaseMode, opt => opt.MapFrom(S => S.IncreaseMode == "Debit" ? 0 :
                1))
                .ForMember(src => src.AccBalance, opt => opt.MapFrom(S => 0))
                .ForMember(src => src.AccDebit, opt => opt.MapFrom(S => 0))
                .ForMember(src => src.AccCredit, opt => opt.MapFrom(S => 0));

            CreateMap<TbFmsAccount, FmsAccountDTO>()
                .ForMember(src => src.IncreaseMode, opt => opt.MapFrom(S => S.IncreaseMode == 0 ? "Debit" :
                "Credit"));

            CreateMap<TbFmsAccCat, FmsAccCatDTO>().ReverseMap();

            CreateMap<TbFmsStatementAccount, FmsStatementAccountDTO>().ReverseMap();

            CreateMap<TbFmsStatementTemplate, AddFmsTemplateDTO>().ReverseMap();

            CreateMap<TbFmsStatement, AddFmsStatementDTO>().ReverseMap();
            CreateMap<TbFmsStatement, FmsStatementDTO>().ReverseMap();

            
            CreateMap<TbFmsTemplateAccount, FmsTemplateAccountDTO>().ReverseMap();

            CreateMap<TbFmsJournalEntry, AddFmsJeDTO>().ReverseMap();
            CreateMap<TbFmsJournalEntry, FmsJeDTO>().ReverseMap();

            CreateMap<TbFmsCategory, AddFmsCategoryDTO>().ReverseMap();
            CreateMap<TbFmsCategory, FmsCategoryDTO>().ReverseMap();




            //HRMS
            CreateMap<TbEmployeeDetail, AddEmployeeDTO>().ForMember(dest => dest.Hrid, opt => opt.MapFrom(src => src.HrmanagerId)).ReverseMap();

            CreateMap<TbEmployeeDetail, EmployeeDetailsDTO>()
                .ForMember(dest => dest.HRName, opt => opt.MapFrom(src => src.Hrmanager.HrfullName))
                .ForMember(dest => dest.Hrid, opt => opt.MapFrom(src => src.HrmanagerId)).ReverseMap();

            CreateMap<TbHrmanagerDetail, AddHRManagerDTO>().ReverseMap();
            CreateMap<TbHrmanagerDetail, HRManagerDTO>().ForMember(dest => dest.Hrid, opt => opt.MapFrom(src => src.Hrid)).ReverseMap();

            CreateMap<TbEmployeeTrainning, AddEmployeeTrainningDTO>().ForMember(dest => dest.Hrid, opt => opt.MapFrom(src => src.HrmangerId)).ReverseMap();
            CreateMap<TbEmployeeTrainning, EmployeeTrainningDTO>()              
            .ForMember(dest => dest.Hrid, opt => opt.MapFrom(src => src.HrmangerId))
            .ForMember(dest => dest.HRName, opt => opt.MapFrom(src => src.Hrmanger.HrfullName))
            .ForMember(dest => dest.EmployeeFullName, opt => opt.MapFrom(src => src.Employee.EmployeeFullName));

            CreateMap<TbEmployeeTaskDetail, AddEmployeeTaskDTO>().ReverseMap();
            CreateMap<TbEmployeeTaskDetail, EmployeeTaskDTO>()
                .ForMember(dest => dest.EmployeeFullName, opt => opt.MapFrom(src => src.Emplyee.EmployeeFullName))
                .ForMember(dest => dest.BounsHours, opt => opt.MapFrom(src => src.Emplyee.HoursWorked > 24 ? src.Emplyee.HoursWorked - 24 : 0)).ReverseMap();

            CreateMap<TbRecuirement, AddRecuirementDTO>().ForMember(dest => dest.Hrid, opt => opt.MapFrom(src => src.HrmanagerId)).ReverseMap();
            CreateMap<TbRecuirement, RecuirementDTO>().ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.Employee.EmployeeId)).ReverseMap();

            CreateMap<TbInterviewDetail, AddinterviewDTO>().ReverseMap();
            CreateMap<TbInterviewDetail, InterviewDTO>().ReverseMap();

            //CRM
            CreateMap<TbCustomer, AddCustomerDTO>().ReverseMap();
            CreateMap<TbCustomer, CustomerDTO>().ReverseMap();

            CreateMap<TbTask, AddTaskDTO>().ReverseMap();
            CreateMap<TbTask, TaskDTO>()
            .ForMember(dest => dest.CustomerFullName, opt => opt.MapFrom(src => src.Customer.FullName))
            .ForMember(dest => dest.CustomerPhone, opt => opt.MapFrom(src => src.Customer.Phone))
            .ForMember(dest => dest.CustomerAge, opt => opt.MapFrom(src => src.Customer.Age));

            CreateMap<TbToDoList, AddToDoListDTO>().ReverseMap();
            CreateMap<TbToDoList, ToDoListDTO>()
            .ForMember(dest => dest.CustomerFullName, opt => opt.MapFrom(src => src.Customer.FullName))
            .ForMember(dest => dest.CustomerPhone, opt => opt.MapFrom(src => src.Customer.Phone));

        }
    }
}

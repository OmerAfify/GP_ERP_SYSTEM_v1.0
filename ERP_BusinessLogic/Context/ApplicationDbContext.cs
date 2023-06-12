using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP_Domians.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ERP_BusinessLogic.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

      


        /*SCM*/


        /*Products Section*/
        public virtual DbSet<TbProduct> TbProducts { get; set; }
        public virtual DbSet<TbCategory> TbCategories { get; set; }
        public virtual DbSet<TbFinishedProductsInventory> TbFinishedProductsInventories { get; set; }

        /*End of Products Section*/


        /*RawMaterials Section*/
        public virtual DbSet<TbRawMaterial> TbRawMaterials { get; set; }
        public virtual DbSet<TbRawMaterialsInventory> TbRawMaterialsInventories { get; set; }

        /*End of RawMaterials Section*/



        /*Distribution Section*/
        public virtual DbSet<TbDistributor> TbDistributors { get; set; }

        public virtual DbSet<TbDistributionOrder> TbDistributionOrders { get; set; }
        public virtual DbSet<TbDistributionOrderDetail> TbDistributionOrderDetails { get; set; }
        public virtual DbSet<TbDistributionOrderStatus> TbDistributionOrderStatus { get; set; }

        /*End of Distribution Section*/



        /*Supplier Section*/
        public virtual DbSet<TbSupplier> TbSuppliers { get; set; }
        public virtual DbSet<TbSupplyingMaterialDetail> TbSupplyingMaterialDetails { get; set; }
        public virtual DbSet<TbOrder_Supplier> TbOrder_Suppliers { get; set; }
        public virtual DbSet<TbOrderStatus_Supplier> TbOrderStatus_Suppliers{ get; set; }
        public virtual DbSet<TbOrderDetails_Supplier> TbOrderDetails_Suppliers { get; set; }

        /*End of Supplier Section*/


        /*Manufacturing Order*/
        public virtual DbSet<TbManufacturingOrder> TbManufacturingOrders { get; set; }
        public virtual DbSet<TbManufacturingOrderDetail> TbManufacturingOrderDetails { get; set; }
        public virtual DbSet<TbManufacturingStatus> TbManufacturingStatus { get; set; }

        /*End of Manufacturing Order*/


        /*END OF SCM */




        public virtual DbSet<TbAdminstrator> TbAdminstrators { get; set; }
        public virtual DbSet<TbCustomer> TbCustomers { get; set; }


        public virtual DbSet<TbEmployeeDetail> TbEmployeeDetails { get; set; }
        public virtual DbSet<TbEmployeeTaskDetail> TbEmployeeTaskDetails { get; set; }
        public virtual DbSet<TbEmployeeTrainning> TbEmployeeTrainnings { get; set; }
        public virtual DbSet<TbFmsAccCat> TbFmsAccCats { get; set; }
        public virtual DbSet<TbFmsAccount> TbFmsAccounts { get; set; }
        public virtual DbSet<TbFmsCategory> TbFmsCategories { get; set; }
        public virtual DbSet<TbFmsJournalEntry> TbFmsJournalEntries { get; set; }
        public virtual DbSet<TbFmsStatement> TbFmsStatements { get; set; }
        public virtual DbSet<TbFmsStatementAccount> TbFmsStatementAccounts { get; set; }
        public virtual DbSet<TbFmsStatementTemplate> TbFmsStatementTemplates { get; set; }
        public virtual DbSet<TbFmsTemplateAccount> TbFmsTemplateAccounts { get; set; }
        public virtual DbSet<TbHrmanagerDetail> TbHrmanagerDetails { get; set; }
        public virtual DbSet<TbInterviewDetail> TbInterviewDetails { get; set; }

        public virtual DbSet<TbQuestion> TbQuestions { get; set; }



        public virtual DbSet<TbRecuirement> TbRecuirements { get; set; }
        public virtual DbSet<TbReporter> TbReporters { get; set; }
        public virtual DbSet<TbSurvey> TbSurveys { get; set; }
        public virtual DbSet<TbTask> TbTasks { get; set; }
        public virtual DbSet<TbToDoList> TbToDoLists { get; set; }
        public virtual DbSet<TbVisualReport> TbVisualReports { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder){


            base.OnModelCreating(modelBuilder);


            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            /*SCM*/

            /*Products*/
            modelBuilder.Entity<TbCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.ToTable("TB_Category");

                entity.Property(e => e.CategoryId).HasColumnName("categoryId");

                entity.Property(e => e.CategoryDescription).HasColumnName("categoryDescription");

                entity.Property(e => e.CategoryName).HasColumnName("categoryName");
            });
            modelBuilder.Entity<TbFinishedProductsInventory>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.ToTable("TB_FinishedProductsInventory");

                entity.Property(e => e.ProductId)
                    .ValueGeneratedNever()
                    .HasColumnName("productId");

                entity.Property(e => e.Area)
                    .HasMaxLength(50)
                    .HasColumnName("area");

                entity.Property(e => e.MonthlyCosts)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("monthlyCosts");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.ReorderingPoint).HasColumnName("reorderingPoint");

                entity.Property(e => e.ShippingDate)
                    .HasColumnType("datetime")
                    .HasColumnName("shippingDate");

                entity.HasOne(d => d.Product)
                    .WithOne(p => p.TbFinishedProductsInventory)
                    .HasForeignKey<TbFinishedProductsInventory>(d => d.ProductId)
                    .HasConstraintName("FK__TB_Finish__produ__3C69FB99");
            });
            modelBuilder.Entity<TbProduct>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK_Tb_Product");

                entity.ToTable("TB_Product");

                entity.Property(e => e.ProductId).HasColumnName("productId");

                entity.Property(e => e.CategoryId).HasColumnName("categoryId");

                entity.Property(e => e.ProductDescription).HasColumnName("productDescription");

                entity.Property(e => e.ProductName).HasColumnName("productName");

                entity.Property(e => e.PurchasePrice)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("purchasePrice");

                entity.Property(e => e.SalesPrice)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("salesPrice");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.TbProducts)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Product_PK_Category");
            });
            /*End of Products*/

            /*Rawmaterials*/
            modelBuilder.Entity<TbRawMaterial>(entity =>
            {
                entity.HasKey(e => e.MaterialId)
                    .HasName("PK_TB_Supplier");

                entity.ToTable("TB_RawMaterial");

                entity.Property(e => e.MaterialId).HasColumnName("materialId");

                entity.Property(e => e.MaterialDescription).HasColumnName("materialDescription");

                entity.Property(e => e.MaterialName).HasColumnName("materialName");
            });
            modelBuilder.Entity<TbRawMaterialsInventory>(entity =>
            {
                entity.HasKey(e => e.MaterialId)
                    .HasName("PK__TB_RawMa__99B653FDB26AF845");

                entity.ToTable("TB_RawMaterialsInventory");

                entity.Property(e => e.MaterialId)
                    .ValueGeneratedNever()
                    .HasColumnName("materialId");

                entity.Property(e => e.Area).HasColumnName("area");

                entity.Property(e => e.MonthlyCosts)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("monthlyCosts");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.ReorderingPoint).HasColumnName("reorderingPoint");

                entity.Property(e => e.ShippingDate)
                    .HasColumnType("datetime")
                    .HasColumnName("shippingDate");

                entity.HasOne(d => d.Material)
                    .WithOne(p => p.TbRawMaterialsInventory)
                    .HasForeignKey<TbRawMaterialsInventory>(d => d.MaterialId)
                    .HasConstraintName("FK_RawMaterialsInventory_PK_RawMaterials");
            });
            /*ENd of RawMaterials*/


            /*Manufacturing*/
            modelBuilder.Entity<TbManufacturingOrder>(entity =>
            {
                entity.HasMany(od => od.ManufacturingOrderDetails).WithOne().OnDelete(DeleteBehavior.Cascade);

            });
            modelBuilder.Entity<TbManufacturingOrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.ManfactoringOrderId, e.RawMaterialId });

                entity.ToTable("TB_ManufacturingOrderDetails");

                entity.Property(e => e.ManfactoringOrderId).HasColumnName("manfactoringOrderId");

                entity.Property(e => e.RawMaterialId).HasColumnName("rawMaterialId");

                entity.Property(e => e.RawMaterialQtyUsed).HasColumnName("rawMaterialQtyUsed");

                entity.HasOne(d => d.ManfactoringOrder)
                    .WithMany(p => p.ManufacturingOrderDetails)
                    .HasForeignKey(d => d.ManfactoringOrderId)
                    .HasConstraintName("FK_TB_ManufacturingOrderDetails_TB_ManufacturingOrder");

                entity.HasOne(d => d.RawMaterial)
                    .WithMany(p => p.TbManufacturingOrderDetails)
                    .HasForeignKey(d => d.RawMaterialId)
                    .HasConstraintName("FK_TB_ManufacturingOrderDetails_TB_RawMaterial");
            });
            modelBuilder.Entity<TbManufacturingStatus>().HasKey(x => x.statusId);

            /*End of Manufacturing*/


            /*Distributor*/
            modelBuilder.Entity<TbDistributionOrder>(entity =>
            {

                entity.ToTable("TB_DistributionOrder");

                entity.Property(e => e.ExpectedArrivalDate)
                    .HasColumnType("datetime")
                    .HasColumnName("expectedArrivalDate");

                entity.Property(e => e.OrderStatusId)
                    .HasColumnName("orderStatusId")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.OrderingDate)
                    .HasColumnType("datetime")
                    .HasColumnName("orderingDate");

                entity.Property(e => e.TotalPrice)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("totalPrice");

                entity.Property(e => e.TotalQty).HasColumnName("totalQty");

                entity.HasOne(d => d.Distributor)
                    .WithMany()
                    .HasForeignKey(d => d.DistributorId)
                    .HasConstraintName("FK_distributionOrder_PK_Distributor");

            });
            modelBuilder.Entity<TbDistributionOrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.DistributionOrderId, e.ProductId })
                    .HasName("COM_PK_distributionOrderId_productId");

                entity.ToTable("TB_DistributionOrderDetails");

                entity.Property(e => e.DistributionOrderId).HasColumnName("distributionOrderId");

                entity.Property(e => e.ProductId).HasColumnName("productId");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("price");

                entity.Property(e => e.Qty).HasColumnName("qty");

                entity.HasOne(d => d.DistributionOrder)
                    .WithMany(p => p.DistributionOrderDetails)
                    .HasForeignKey(d => d.DistributionOrderId)
                    .HasConstraintName("FK_DistributionOrderDetails_PK_DistributionOrder");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TbDistributionOrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_DistributionOrderDetails_PK_Products");
            });
            modelBuilder.Entity<TbDistributionOrderStatus>().HasKey(x => x.Id);



            modelBuilder.Entity<TbDistributor>(entity =>
            {
                entity.HasKey(e => e.DistributorId);

                entity.ToTable("TB_Distributor");

                entity.Property(e => e.DistributorId).HasColumnName("distributorId");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.DistributorName).HasColumnName("distributorName");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("phoneNumber");
            });
            /*End of Distributor*/

            /*Supplier*/
            modelBuilder.Entity<TbSupplier>(entity =>
            {
                entity.HasKey(e => e.SupplierId)
                    .HasName("PK_TB_Supplier_1");

                entity.ToTable("TB_Supplier");

                entity.Property(e => e.SupplierId).HasColumnName("supplierId");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("phoneNumber");

                entity.Property(e => e.SupplierDescription).HasColumnName("supplierDescription");

                entity.Property(e => e.SupplierName)
                    .IsRequired()
                    .HasColumnName("supplierName");
            });
            modelBuilder.Entity<TbSupplyingMaterialDetail>(entity =>
            {
                entity.HasKey(e => new { e.SupplierId, e.MaterialId })
                    .HasName("COM_PK_supplierId_materialId");

                entity.ToTable("TB_SupplyingMaterialDetails");

                entity.Property(e => e.SupplierId).HasColumnName("supplierId");

                entity.Property(e => e.MaterialId).HasColumnName("materialId");


                entity.Property(e => e.PricePerUnit)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("pricePerUnit");

                entity.HasOne(d => d.Material)
                    .WithMany(p => p.TbSupplyingMaterialDetails)
                    .HasForeignKey(d => d.MaterialId)
                    .HasConstraintName("FK_SupplyingMaterialDetails_PK_RawMaterial");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.TbSupplyingMaterialDetails)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_SupplyingMaterialDetails_PK_Supplier");
            });
            modelBuilder.Entity<TbOrder_Supplier>().HasMany(od => od.OrderedMaterials).WithOne().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<TbOrderDetails_Supplier>().OwnsOne(i => i.OrderedRawMaterials, io => { io.WithOwner(); });
            modelBuilder.Entity<TbOrderStatus_Supplier>().HasKey(x => x.OrderStatusId);
            /*End of Supplier*/


            /*END OF SCM*/

            modelBuilder.Entity<TbAdminstrator>(entity =>
            {
                entity.HasKey(e => e.AdminId)
                    .HasName("PK__TB_Admin__719FE4E800A4E6F2");

                entity.ToTable("TB_Adminstrator");

                entity.Property(e => e.AdminId).HasColumnName("AdminID");

                entity.Property(e => e.AdminEntryDate).HasColumnType("datetime");

                entity.Property(e => e.ReporterIdFk).HasColumnName("ReporterID_FK");

                entity.HasOne(d => d.ReporterIdFkNavigation)
                    .WithMany(p => p.TbAdminstrators)
                    .HasForeignKey(d => d.ReporterIdFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_Adminstrator_TB_Reporter");
            });
        
            modelBuilder.Entity<TbCustomer>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("PK__TB_Custo__A4AE64D85267C97A");

                entity.ToTable("TB_Customer");

                entity.Property(e => e.Age).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.FullName).IsRequired();

                entity.Property(e => e.Image).HasColumnType("image");

                entity.Property(e => e.Phone).HasColumnType("nvarchar(max)");
                entity.Property(e => e.Sex).HasColumnType("nvarchar(max)");
            });
       
            modelBuilder.Entity<TbEmployeeDetail>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.ToTable("TB_EmployeeDetails");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.AttendenceTime).HasColumnType("datetime");

                entity.Property(e => e.DateOfJoining).HasColumnType("datetime");

                entity.Property(e => e.EmployeeSalary).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Holidays).HasColumnType("date");

                entity.Property(e => e.HrmanagerId).HasColumnName("HRManagerId");

                entity.Property(e => e.TaxWithholding).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Hrmanager)
                    .WithMany(p => p.TbEmployeeDetails)
                    .HasForeignKey(d => d.HrmanagerId)
                    .HasConstraintName("FK_TB_EmployeeDetails_TB_HRManagerDetails");
            });

            modelBuilder.Entity<TbEmployeeTaskDetail>(entity =>
            {
                entity.HasKey(e => e.TaskId);

                entity.ToTable("TB_EmployeeTaskDetails");

                entity.Property(e => e.TaskAssignedTime).HasColumnType("datetime");

                entity.Property(e => e.TaskDeadlineTime).HasColumnType("datetime");

                entity.HasOne(d => d.Emplyee)
                    .WithMany(p => p.TbEmployeeTaskDetails)
                    .HasForeignKey(d => d.EmplyeeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TB_EmployeeTaskDetails_TB_EmployeeDetails");
            });

            modelBuilder.Entity<TbEmployeeTrainning>(entity =>
            {
                entity.HasKey(e => e.TrainnningId);

                entity.ToTable("TB_EmployeeTrainning");

                entity.Property(e => e.HrmangerId).HasColumnName("HRMangerId");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TbEmployeeTrainnings)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TB_EmployeeTrainning_TB_EmployeeDetails");

                entity.HasOne(d => d.Hrmanger)
                    .WithMany(p => p.TbEmployeeTrainnings)
                    .HasForeignKey(d => d.HrmangerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TB_EmployeeTrainning_TB_HRManagerDetails");
            });

     
            modelBuilder.Entity<TbFmsAccCat>(entity =>
            {
                entity.HasKey(e => new { e.AccId, e.CatId })
                    .HasName("composite_pk category_account");

                entity.ToTable("TB_FMS_AccCat");

                entity.Property(e => e.AccId).HasColumnName("AccID");

                entity.Property(e => e.CatId).HasColumnName("CatID");

                entity.HasOne(d => d.Acc)
                    .WithMany(p => p.TbFmsAccCats)
                    .HasForeignKey(d => d.AccId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TB_FMS_Ac__AccID__114A936A");

                entity.HasOne(d => d.Cat)
                    .WithMany(p => p.TbFmsAccCats)
                    .HasForeignKey(d => d.CatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TB_FMS_Ac__CatID__123EB7A3");
            });

            modelBuilder.Entity<TbFmsAccount>(entity =>
            {
                entity.HasKey(e => e.AccId)
                    .HasName("PK__TB_FMS_A__91CBC39804A449CE");

                entity.ToTable("TB_FMS_Account");

                entity.Property(e => e.AccId).HasColumnName("AccID");

                entity.Property(e => e.AccBalance).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.AccCredit).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.AccDebit).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<TbFmsCategory>(entity =>
            {
                entity.HasKey(e => e.CatId)
                    .HasName("PK__TB_FMS_C__6A1C8ADAB474143C");

                entity.ToTable("TB_FMS_Category");

                entity.Property(e => e.CatId).HasColumnName("CatID");
            });

            modelBuilder.Entity<TbFmsJournalEntry>(entity =>
            {
                entity.HasKey(e => e.Jeid)
                    .HasName("PK__TB_FMS_J__703C596C510B154B");

                entity.ToTable("TB_FMS_JournalEntry");

                entity.Property(e => e.Jeid).HasColumnName("JEID");

                entity.Property(e => e.Jeaccount1).HasColumnName("JEAccount1");

                entity.Property(e => e.Jeaccount2).HasColumnName("JEAccount2");

                entity.Property(e => e.Jecredit)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("JECredit");

                entity.Property(e => e.Jedate)
                    .HasColumnType("datetime")
                    .HasColumnName("JEDate");

                entity.Property(e => e.Jedebit)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("JEDebit");

                entity.Property(e => e.Jedescription).HasColumnName("JEDescription");

                entity.Property(e => e.Jename).HasColumnName("JEName");

                entity.HasOne(d => d.Jeaccount1Navigation)
                    .WithMany(p => p.TbFmsJournalEntryJeaccount1Navigations)
                    .HasForeignKey(d => d.Jeaccount1)
                    .HasConstraintName("FK__TB_FMS_Jo__JEAcc__10566F31");

                entity.HasOne(d => d.Jeaccount2Navigation)
                    .WithMany(p => p.TbFmsJournalEntryJeaccount2Navigations)
                    .HasForeignKey(d => d.Jeaccount2)
                    .HasConstraintName("FK__TB_FMS_Jo__JEAcc__114A936A");
            });

            modelBuilder.Entity<TbFmsStatement>(entity =>
            {
                entity.HasKey(e => e.StaId)
                    .HasName("PK__TB_FMS_S__BA875A61CF47BBBC");

                entity.ToTable("TB_FMS_Statement");

                entity.Property(e => e.StaId).HasColumnName("StaID");

                entity.Property(e => e.StaBalance).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.StaDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TbFmsStatementAccount>(entity =>
            {
                entity.HasKey(e => new { e.AccName, e.StaId })
                    .HasName("composite primary key");

                entity.ToTable("TB_FMS_StatementAccounts");

                entity.Property(e => e.StaId).HasColumnName("StaID");

                entity.Property(e => e.AccBalance).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Sta)
                    .WithMany(p => p.TbFmsStatementAccounts)
                    .HasForeignKey(d => d.StaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TB_FMS_St__StaID__18EBB532");
            });

            modelBuilder.Entity<TbFmsStatementTemplate>(entity =>
            {
                entity.HasKey(e => e.TempId)
                    .HasName("PK__TB_FMS_S__06C703E10B86E807");

                entity.ToTable("TB_FMS_StatementTemplate");

                entity.Property(e => e.TempId).HasColumnName("TempID");

                entity.Property(e => e.TempDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TbFmsTemplateAccount>(entity =>
            {
                entity.HasKey(e => new { e.AccId, e.TempId })
                    .HasName("composite_pk template_account");

                entity.ToTable("TB_FMS_TemplateAccounts");

                entity.Property(e => e.AccId).HasColumnName("AccID");

                entity.Property(e => e.TempId).HasColumnName("TempID");

                entity.HasOne(d => d.Acc)
                    .WithMany(p => p.TbFmsTemplateAccounts)
                    .HasForeignKey(d => d.AccId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TB_FMS_Te__AccID__160F4887");

                entity.HasOne(d => d.Temp)
                    .WithMany(p => p.TbFmsTemplateAccounts)
                    .HasForeignKey(d => d.TempId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TB_FMS_Te__TempI__17036CC0");
            });


            modelBuilder.Entity<TbHrmanagerDetail>(entity =>
            {
                entity.HasKey(e => e.Hrid);

                entity.ToTable("TB_HRManagerDetails");

                entity.Property(e => e.Hrid).HasColumnName("HRId");

                entity.Property(e => e.HrfullName).HasColumnName("HRFullName");

                entity.Property(e => e.Hremail).HasColumnName("HREmail");
                entity.Property(e => e.Age).HasColumnName("Age");
                entity.Property(e => e.Gender).HasColumnName("Gender");
                entity.Property(e => e.Salary).HasColumnName("Salary");
                entity.Property(e => e.Phone).HasColumnName("Phone");
            });

            modelBuilder.Entity<TbInterviewDetail>(entity =>
            {
                entity.HasKey(e => e.InterviewId);

                entity.ToTable("TB_InterviewDetails");

                entity.Property(e => e.InterviewDate).HasColumnType("datetime");

                entity.HasOne(d => d.Recuriement)
                    .WithMany(p => p.TbInterviewDetails)
                    .HasForeignKey(d => d.RecuriementId)
                    .HasConstraintName("FK_TB_InterviewDetails_TB_Recuirement");
            });

        
            modelBuilder.Entity<TbQuestion>(entity =>
            {
                entity.HasKey(e => e.QuestionId)
                    .HasName("PK__TB_Quest__0DC06FAC6B4573AC");

                entity.ToTable("TB_Question");
            });

      
            modelBuilder.Entity<TbRecuirement>(entity =>
            {
                entity.HasKey(e => e.RecuirementId);

                entity.ToTable("TB_Recuirement");

                entity.Property(e => e.HrmanagerId).HasColumnName("HRManagerId");

                entity.Property(e => e.RecuirementDate).HasColumnType("datetime");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TbRecuirements)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_TB_Recuirement_TB_EmployeeDetails");

                entity.HasOne(d => d.Hrmanager)
                    .WithMany(p => p.TbRecuirements)
                    .HasForeignKey(d => d.HrmanagerId)
                    .HasConstraintName("FK_TB_Recuirement_TB_HRManagerDetails");
            });

            modelBuilder.Entity<TbReporter>(entity =>
            {
                entity.HasKey(e => e.ReporterId)
                    .HasName("PK__TB_Repor__4406548BB7FD1991");

                entity.ToTable("TB_Reporter");

                entity.Property(e => e.ReporterId).HasColumnName("ReporterID");

                entity.Property(e => e.ReporterEntryDate).HasColumnType("datetime");
            });

      
            modelBuilder.Entity<TbSurvey>(entity =>
            {
                entity.HasKey(e => e.SurveyId)
                    .HasName("PK__TB_Surve__A5481F7D0DEB2CDB");

                entity.ToTable("TB_Survey");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.TbSurveys)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__TB_Survey__Custo__48CFD27E");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.TbSurveys)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK__TB_Survey__Quest__49C3F6B7");
            });

            modelBuilder.Entity<TbTask>(entity =>
            {
                entity.HasKey(e => e.TaskId)
                    .HasName("PK__TB_Task__7C6949B1BE263896");

                entity.ToTable("TB_Task");

                entity.Property(e => e.TaskDate).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.TbTasks)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TB_Task_TB_Customer");
                    

                   
            });

            modelBuilder.Entity<TbToDoList>(entity =>
            {
                entity.HasKey(e => e.ToDoListId)
                    .HasName("PK__TB_ToDoL__1BEFD56CDF0D5E6E");

                entity.ToTable("TB_ToDoList");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.TbToDoLists)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__TB_ToDoLi__Custo__45F365D3");
            });

            modelBuilder.Entity<TbVisualReport>(entity =>
            {
                entity.HasKey(e => e.ReportId)
                    .HasName("PK__TB_Visua__D5BD48E54B3BA761");

                entity.ToTable("TB_VisualReport");

                entity.Property(e => e.ReportId).HasColumnName("ReportID");

                entity.Property(e => e.RAdminId).HasColumnName("R_AdminID");

                entity.Property(e => e.RReporterId).HasColumnName("R_ReporterID");

                entity.Property(e => e.ReportDate).HasColumnType("datetime");

                entity.HasOne(d => d.RAdmin)
                    .WithMany(p => p.TbVisualReports)
                    .HasForeignKey(d => d.RAdminId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_VisualReport_TB_Adminstrator");

                entity.HasOne(d => d.RReporter)
                    .WithMany(p => p.TbVisualReports)
                    .HasForeignKey(d => d.RReporterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_VisualReport_TB_Reporter");

            });



      
        }


    }
}




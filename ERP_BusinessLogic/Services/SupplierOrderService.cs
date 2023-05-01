using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.Interfaces.IUnitOfWork;
using ERP_Domians.IServices;
using ERP_Domians.Models;
using ERP_Domians.Models.HelpersProperties;
using ERP_Domians.Models.OwnedProperties;

namespace ERP_BusinessLogic.Services
{
    public class SupplierOrderService : ISupplierOrderService
    {

        private readonly IUnitOfWork _unitOfWork;
        public SupplierOrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

    public async Task<TbOrder_Supplier> CreateSupplierOrder(int supplierId, decimal shippingCost,
        List<MaterialsOrderedParmeters> materialsOrdered)
        {
            //get supplier from DB
            var supplier = await _unitOfWork.Supplier.GetByIdAsync(supplierId);

            //get Supplier supplying Materials Details from DB
var supplierSupplyingMaterials = await _unitOfWork.SupplingMaterialDetails
                .FindRangeAsync(r => r.SupplierId == supplierId && materialsOrdered.Select(m=>m.MaterialId)
                            .Any(m=> r.MaterialId == m ) );

            //get RawMaterials details from DB
            var rawMaterials = await _unitOfWork.RawMaterial
                 .FindRangeAsync(m=> materialsOrdered.Select(m=>m.MaterialId)
                 .Any(mo=>m.MaterialId==mo));



            var orderedMaterialsDetails = new List<TbOrderDetails_Supplier>();
            
            foreach(var material in materialsOrdered)
            {
                var supplierMaterialDetails = supplierSupplyingMaterials
                                              .FirstOrDefault(s => s.MaterialId == material.MaterialId);

                var rawMaterial = rawMaterials.FirstOrDefault(m=>m.MaterialId==material.MaterialId);

                var orderedMaterialDetail = new OrderedRawMaterialDetails(rawMaterial.MaterialId, rawMaterial.MaterialName,
                                                                          supplierMaterialDetails.PricePerUnit);
                
                var orderedMaterial = new TbOrderDetails_Supplier(orderedMaterialDetail, material.Qty, material.Qty * supplierMaterialDetails.PricePerUnit);
                orderedMaterialsDetails.Add(orderedMaterial);
            }

            var subTotalPrice = orderedMaterialsDetails.Sum(p => p.Price);
            var totalQty= orderedMaterialsDetails.Sum(p => p.Quantity);


            var order = new TbOrder_Supplier(supplierId, totalQty, subTotalPrice, shippingCost, subTotalPrice + shippingCost,
                                1, DateTime.Now.AddDays(supplier.AdverageDeliveryTimeInDays), orderedMaterialsDetails);

            _unitOfWork.OrderSupplier.InsertAsync(order);
            var result = await _unitOfWork.Save();

            if (result == 0)
                return null;
            else
                return order;
        }



    }
}

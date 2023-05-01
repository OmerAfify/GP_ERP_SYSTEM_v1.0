using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.Interfaces.IUnitOfWork;
using ERP_Domians.IServices;
using ERP_Domians.Models;
using ERP_Domians.Models.HelpersProperties;

namespace ERP_BusinessLogic.Services
{
    public class ManufacturingOrderService : IManufacturingOrderService
    {

        private readonly IUnitOfWork _unitOfWork;
        public ManufacturingOrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<TbManufacturingOrder> CreateManufacturingOrder(int productToManufacturedId, int qty, decimal costs,
                                           DateTime startingDate,List<MaterialsOrderedParmeters> rawMaterialsUsed ) {

            var ManufacturingDetailsList = new List<TbManufacturingOrderDetail>();

            foreach (var rawMaterial in rawMaterialsUsed)
            {
                var rawMaterialUsed = new TbManufacturingOrderDetail(rawMaterial.MaterialId, rawMaterial.Qty);
                ManufacturingDetailsList.Add(rawMaterialUsed);
            }

            var Order = new TbManufacturingOrder(productToManufacturedId, qty, startingDate, startingDate.AddDays(3),
                                                 costs,1,ManufacturingDetailsList);

            _unitOfWork.Manufacturing.InsertAsync(Order);

            var result = await _unitOfWork.Save();

            if(result == 0)
                return  null;
            else
                return Order;


           
        }


    }

}

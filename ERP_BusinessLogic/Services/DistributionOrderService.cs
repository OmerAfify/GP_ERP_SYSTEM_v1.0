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
    public class DistributionOrderService : IDistributionOrderService
    {

        private readonly IUnitOfWork _unitOfWork;
        public DistributionOrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<TbDistributionOrder> CreateDistributionOrder(int DistributorId, 
             List<OrderedFinishedProductParameters> orderedProducts)
        {

            var orderDetailsList = new List<TbDistributionOrderDetail>();
            foreach(var product in orderedProducts)
            {
                var productToAdd = await _unitOfWork.Product.GetByIdAsync(product.ProductId);
                var orderedProduct = new TbDistributionOrderDetail(productToAdd.ProductId,product.Qty, product.Qty * productToAdd.SalesPrice);

                orderDetailsList.Add(orderedProduct);
            }

            var totalQty = orderDetailsList.Sum(q => q.Qty);
            var subtotal = orderDetailsList.Sum(q => q.Price);

            var order = new TbDistributionOrder(DistributorId, totalQty, subtotal,
                                                subtotal, DateTime.UtcNow.AddDays(3), 1, orderDetailsList);


            _unitOfWork.Distribution.InsertAsync(order);
            var result = await _unitOfWork.Save();

            if (result == 0)
                return null;
            else
                return order;


        }

   

    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domains.Interfaces.IUnitOfWork;
using ERP_Domians.IServices;
using ERP_Domians.Models.HelpersProperties;
using GP_ERP_SYSTEM_v1._0.DTOs;
using GP_ERP_SYSTEM_v1._0.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GP_ERP_SYSTEM_v1._0
{
    [Route("api/[action]")]
    [ApiController]
    public class OrderSupplierController : ControllerBase
    {
        private readonly ISupplierOrderService _orderService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderSupplierController(ISupplierOrderService orderService, IUnitOfWork unitOfWork, IMapper mapper)
        {
             _unitOfWork = unitOfWork;
             _mapper = mapper;
             _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult> OrderRawMaterialFromSupplier(int supplierId, decimal shippingCost , List<MaterialsOrderedParmeters> order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
         
            try {

                var supplierMaterialDetails = await _unitOfWork.SupplingMaterialDetails
                                .FindRangeAsync(r => r.SupplierId == supplierId && order.Select(m => m.MaterialId)
                                            .Any(m => r.MaterialId == m));


                if (supplierMaterialDetails.Count() == 0)
                    return NotFound(new ErrorApiResponse(404, "the supplier Id associated with the materials ids are not found.")); ;


                if (supplierMaterialDetails.Count() != order.Count())
                        return BadRequest(new ErrorApiResponse(400, "Some materials Ids are not being provided by this supplier.")); ;



                var result = await _orderService.CreateSupplierOrder(supplierId, shippingCost, order);
                
                if (result == null)
                    return StatusCode(500, new ErrorApiResponse(500) { Message = "Error Occured While Creating your Order" });

             
                return Ok(result);
            
            }catch(Exception ex)
            {
                return StatusCode(500, new ErrorApiResponse(500) { Message = ex.Message });
            }

        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult> GetSupplierOrderById(int orderId) {
            try {

                var order = await _unitOfWork.OrderSupplier.FindAsync(o=>o.Id==orderId,
                    new List<string>() { "OrderStatus", "OrderedMaterials", "Supplier" });

                if (order == null) return NotFound(new ErrorApiResponse(404));

                return Ok( _mapper.Map<OrderSupplierDTO>( order));
            
            } catch (Exception ex)
            {
                return StatusCode(500, new ErrorApiResponse(500) { Message = ex.Message });

            }

        }


        [HttpGet]
        public async Task<ActionResult> GetAllSupplierOrders()
        {
            try
            {

                var orders = await _unitOfWork.OrderSupplier.GetAllAsync(
                    new List<string>() { "OrderStatus", "OrderedMaterials","Supplier" },
                    o => o.OrderByDescending(o => o.OrderingDate));

                if (orders == null) return NotFound(new ErrorApiResponse(404));

               
                return Ok(_mapper.Map<List<OrderSupplierDTO>>(orders));

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorApiResponse(500) { Message = ex.Message });

            }

        }



        // Updating Status APIs

        [HttpGet]
        public async Task<ActionResult> ChangeSupplierOrderStatusToShipped(int orderId)
        {

            try
            {
                var order = await _unitOfWork.OrderSupplier.GetByIdAsync(orderId);

                if (order == null) return NotFound(new ErrorApiResponse(404, "Supplier Order Id is not found."));

                if (order.OrderStatusId != 1)
                    return BadRequest(new ErrorApiResponse(400, "Supplier Order status has to be pending inorder to change it to shipped.."));

                order.OrderStatusId = 2;
                _unitOfWork.OrderSupplier.Update(order);

                await _unitOfWork.Save();
                return Ok("status updated from pending to shipped");

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorApiResponse(500) { Message = ex.Message });
            }


        }

        [HttpGet]
        public async Task<ActionResult> ChangeSupplierOrderStatusToFullfilled(int orderId)
        {

            try
            {
                var order = await _unitOfWork.OrderSupplier.FindAsync(i=> i.Id ==orderId, new List<string>(){"OrderedMaterials"});

                if (order == null) return NotFound(new ErrorApiResponse(404, "Supplier Order Id is not found."));

                if (order.OrderStatusId != 2)
                    return BadRequest(new ErrorApiResponse(400, "Supplier Order status has to be shipped inorder to change it to fullfilled.."));

                order.OrderStatusId = 3;
                _unitOfWork.OrderSupplier.Update(order);


                foreach (var rawMaterial in order.OrderedMaterials)
                {
                    await UpdateRawMaterialsInventory(rawMaterial.OrderedRawMaterials.MaterialId , rawMaterial.Quantity);
                }

                await _unitOfWork.Save();

                return Ok("status updated from shipped to fullfilled");

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorApiResponse(500) { Message = ex.Message });
            }

        }


        [HttpGet]
        public async Task<ActionResult> ChangeSupplierOrderStatusToFailed(int orderId)
        {

            try
            {
                var order = await _unitOfWork.OrderSupplier.GetByIdAsync(orderId);

                if (order == null) return NotFound(new ErrorApiResponse(404, "Supplier Order Id is not found."));


                order.OrderStatusId = 4;
                _unitOfWork.OrderSupplier.Update(order);

                await _unitOfWork.Save();
                return Ok("status set to failed");

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorApiResponse(500) { Message = ex.Message });
            }

        }


        //private helper methods
        private async Task UpdateRawMaterialsInventory(int rawMaterialId, int qtyToAdd)
        {

            var rawMaterial = await _unitOfWork.RawMaterialInventory.GetByIdAsync(rawMaterialId);

            rawMaterial.Quantity += qtyToAdd;

            if (rawMaterial.Quantity > rawMaterial.ReorderingPoint)
                rawMaterial.HasReachedROP = false;
            else
                rawMaterial.HasReachedROP = true;

            _unitOfWork.RawMaterialInventory.Update(rawMaterial);

        }


    }
}

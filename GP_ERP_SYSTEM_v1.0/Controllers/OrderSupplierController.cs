using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domains.Interfaces.IUnitOfWork;
using ERP_Domians.IServices;
using ERP_Domians.Models.HelpersProperties;
using GP_ERP_SYSTEM_v1._0.DTOs;
using GP_ERP_SYSTEM_v1._0.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GP_ERP_SYSTEM_v1._0
{
    [Route("api/[action]")]
    [ApiController]
    [Authorize(Roles = "Admin,SCM")]
    public class OrderSupplierController : ControllerBase
    {
        private readonly ISupplierOrderService _orderService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;

        public OrderSupplierController(ISupplierOrderService orderService, IUnitOfWork unitOfWork,
            IMapper mapper, HttpClient httpClient)
        {
             _unitOfWork = unitOfWork;
             _mapper = mapper;
             _orderService = orderService;
            _httpClient = httpClient;
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


                /*Create JE*/

                var supplierJE = new AddFmsJeDTO()
                {
                    Jename = "Supplier Order from Supplier ID = " + result.SupplierId,
                    Jedescription = "An Order from Supplier whose ID is " + result.SupplierId +
                      " with total cash of  $" + result.TotalPrice + " and total quantity of " + result.TotalQty,
                    Jeaccount1 = 3,
                    Jeaccount2 = 1,
                    Jecredit = result.TotalPrice,
                    Jedebit = result.TotalPrice,
                    Jedate = result.OrderingDate,
                };

                var shippingCostJE = new AddFmsJeDTO()
                {
                    Jename = "ShippingCost of Supplier Order from Supplier ID = " + result.SupplierId,
                    Jedescription = "Cost of Shiping Order from Supplier To the Inventory.",
                    Jeaccount1 = 3,
                    Jeaccount2 = 1,
                    Jecredit = result.ShippingCost,
                    Jedebit = result.ShippingCost,
                    Jedate = result.OrderingDate,
                };

                var createJE = await _httpClient.PostAsync("https://localhost:44393/api/AddNewFmsJournalEntry",
                   new StringContent(JsonConvert.SerializeObject(supplierJE), Encoding.UTF8, "application/json"));

                var createJE2 = await _httpClient.PostAsync("https://localhost:44393/api/AddNewFmsJournalEntry",
               new StringContent(JsonConvert.SerializeObject(shippingCostJE), Encoding.UTF8, "application/json"));

                // Check if the response was successfull
                if (!createJE.IsSuccessStatusCode && !createJE2.IsSuccessStatusCode)
                    return BadRequest();


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

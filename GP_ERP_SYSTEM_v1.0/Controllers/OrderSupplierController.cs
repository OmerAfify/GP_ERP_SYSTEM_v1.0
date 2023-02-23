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
                                .FindRangeAsync(r => r.SupplierId == supplierId && order.Select(m => m.materialId)
                                            .Any(m => r.MaterialId == m));


                if (supplierMaterialDetails.Count()==0)
                    return NotFound(new ErrorApiResponse(404, "the supplier Id associated with the materials ids are not found.")); ;


                if (supplierMaterialDetails.Count() != order.Count())
                        return BadRequest(new ErrorApiResponse(400, "Some materials Ids are not being provided by this supplier.")); ;


                return Ok(await _orderService.CreateSupplierOrder(supplierId, shippingCost, order));
            
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


    }
}

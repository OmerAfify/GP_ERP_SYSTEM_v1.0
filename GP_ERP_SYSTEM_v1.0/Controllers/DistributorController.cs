using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domains.Interfaces.IUnitOfWork;
using ERP_Domians.IServices;
using ERP_Domians.Models;
using ERP_Domians.Models.HelpersProperties;
using GP_ERP_SYSTEM_v1._0.DTOs;
using GP_ERP_SYSTEM_v1._0.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace GP_ERP_SYSTEM_v1._0.Controllers
{
    [Route("api/[action]")]
    [ApiController]
  //  [Authorize(Roles = "Admin,SCM")]

    public class DistributorController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDistributionOrderService _distributionOrderService;
        private readonly HttpClient _httpClient;
    
        public DistributorController(IUnitOfWork unitOfWork, IMapper mapper,
            HttpClient httpClient, IDistributionOrderService distributionOrderService)
        {
            _httpClient = httpClient;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _distributionOrderService = distributionOrderService;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllDistributors()
        {
            try
            {
                var distributors = await _unitOfWork.Distributor.GetAllAsync();
                return Ok(_mapper.Map<List<DistributorDTO>>(distributors));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorExceptionResponse(500, null, ex.Message));
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetDirstributorById(int id)
        {

            if (id <= 0)
                return BadRequest(new ErrorValidationResponse() { Errors = new List<string> { "Id can't be 0 or less." } });


            try
            {
                var distributor = await _unitOfWork.Distributor.GetByIdAsync(id);

                if (distributor == null)
                    return NotFound(new ErrorApiResponse(400));

                return Ok(_mapper.Map<DistributorDTO>(distributor));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorExceptionResponse(500, null, ex.Message));
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddNewDistributor([FromBody] AddDistributorDTO distributorDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _unitOfWork.Distributor.InsertAsync(_mapper.Map<TbDistributor>(distributorDTO));
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorExceptionResponse(500, null, ex.Message));
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDistributor(int id, [FromBody] AddDistributorDTO UpdateDistributor)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id <= 0)
                return BadRequest(new ErrorValidationResponse() { Errors = new List<string> { "Id can't be 0 or less." } });

            try
            {
                var distributorToUpdate = await _unitOfWork.Distributor.GetByIdAsync(id);

                if (distributorToUpdate == null)
                    return BadRequest(new ErrorApiResponse(400, "Invalid Id is sent."));


                _mapper.Map(UpdateDistributor, distributorToUpdate);

                _unitOfWork.Distributor.Update(distributorToUpdate);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorExceptionResponse(500, null, ex.Message));
            }
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteDistributor(int id)
        {

            if (id <= 0)
                return BadRequest(new ErrorValidationResponse() { Errors = new List<string> { "Id can't be 0 or less." } });

            try
            {
                var distributorToDelete = await _unitOfWork.Distributor.GetByIdAsync(id);

                if (distributorToDelete == null)
                    return BadRequest(new ErrorApiResponse(400, "Invalid Id is sent."));

                _unitOfWork.Distributor.Delete(distributorToDelete);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorExceptionResponse(500, null, ex.Message));
            }
        }



        /////////////////////////////////////////////  DISTRIBUTION ORDER /////////////////////////////////////////////////////
        
        [HttpPost]
        public async Task<ActionResult> CreateDistributionOrder([FromBody] CreateDistributionOrderDTO createDistibutionOrder)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var distributor = await _unitOfWork.Distributor.GetByIdAsync(createDistibutionOrder.DistributorId);

                if (distributor == null) return NotFound(new ErrorApiResponse(404, "Distributor id is not found"));




                var invalidProductsIds = new List<int>();
                foreach (var finishedProduct in createDistibutionOrder.ProductsOrdered)
                {
                    var InventoryFinishedProduct = await _unitOfWork.ProductsInventory.GetByIdAsync(finishedProduct.ProductId);

                    if (InventoryFinishedProduct == null) invalidProductsIds.Add(finishedProduct.ProductId);

                }

                if (invalidProductsIds.Count > 0)
                    return BadRequest(new ErrorApiResponse(400, "The following Products ids are not included in the invertory : "
                                                                 + String.Join(", ", invalidProductsIds) + " ."));


                var order = await _distributionOrderService.CreateDistributionOrder(createDistibutionOrder.DistributorId,
                 createDistibutionOrder.ProductsOrdered);


                if (order == null)
                    return StatusCode(500, "An uexpected error occured while creating your order. please try again later");
                
                /*Create JE*/

                var JE = new AddFmsJeDTO()
                {
                    Jename = "Distribution Order from Distributor ID = " + order.DistributorId,
                    Jedescription = "Distribution Order from Distributor " + order.DistributorId + 
                      " with total cash of  $" + order.TotalPrice + " and total quantity of " + order.TotalQty,
                    Jeaccount1 = 1,
                    Jeaccount2 = 3,
                    Jecredit = order.TotalPrice,
                    Jedebit = order.TotalPrice,
                    Jedate = order.OrderingDate,
               
                };

                var createJE = await _httpClient.PostAsync("https://localhost:44393/api/AddNewFmsJournalEntry",
                   new StringContent(JsonConvert.SerializeObject(JE), Encoding.UTF8, "application/json"));

                // Check if the response was successfull
                if (!createJE.IsSuccessStatusCode)
                    return BadRequest();


                return Ok(_mapper.Map<ReturnedDistributionOrderDTO>(order) );


            }
            catch (Exception ex)
            {

                return StatusCode(500, new ErrorExceptionResponse(500, null, ex.Message));
            }



        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<ReturnedDistributionOrderDTO>> GetDistributionOrderById(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {

                var distributionOrder= await _unitOfWork.Distribution.GetDistributionOrderById(id);

                if (distributionOrder == null)
                    return NotFound(new ErrorApiResponse(404, "Distribution Order Id is not found."));

                return Ok(_mapper.Map<ReturnedDistributionOrderDTO>(distributionOrder));

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorApiResponse(500) { Message = ex.Message });
            }

        }


        [HttpGet]
        public async Task<ActionResult<List<ReturnedDistributionOrderDTO>>> GetAllDistributionOrders()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var distributionOrders = await _unitOfWork.Distribution.GetAllDistributionOrders();


                return Ok(_mapper.Map<List<ReturnedDistributionOrderDTO>>(distributionOrders));

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorApiResponse(500) { Message = ex.Message });
            }

        }




        // Updating Status APIs

        [HttpGet]
        public async Task<ActionResult> ChangeDistributionStatusToShipped(int orderId) {

            try
            {
                var order = await _unitOfWork.Distribution.GetByIdAsync(orderId);

                if(order==null) return NotFound(new ErrorApiResponse(404, "Distribution Order Id is not found."));

                if(order.OrderStatusId != 1)
                    return BadRequest(new ErrorApiResponse(400, "Distribution Order status has to be pending inorder to change it to shipped.."));

                order.OrderStatusId = 2;
                _unitOfWork.Distribution.Update(order);

                await _unitOfWork.Save();
                return Ok("status updated from pending to shipped");

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorApiResponse(500) { Message = ex.Message });
            }


        }

        [HttpGet]
        public async Task<ActionResult> ChangeDistributionStatusToFullfilled(int orderId) {

            try
            {
                var order = await _unitOfWork.Distribution.GetDistributionOrderById(orderId);

                if (order == null) return NotFound(new ErrorApiResponse(404, "Distribution Order Id is not found."));

                if (order.OrderStatusId != 2)
                    return BadRequest(new ErrorApiResponse(400, "Distribution Order status has to be shipped inorder to change it to fullfilled.."));

                order.OrderStatusId = 3;
                _unitOfWork.Distribution.Update(order);



                // Remove Qty to distribute  + set ROP from the products inventory when its status are shipped.

                foreach (var product in order.DistributionOrderDetails)
                {
                    await UpdateProductsInventory(product.ProductId, product.Qty);
                }


                await _unitOfWork.Save();
                return Ok("status updated from shipped to fullfilled");

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorApiResponse(500) { Message = ex.Message });
            }

        }



        //private helper methods 

        private async Task UpdateProductsInventory(int productId, int qtyToRemove) {

            var product = await _unitOfWork.ProductsInventory.GetByIdAsync(productId);

            product.Quantity -= qtyToRemove;
       
            if (product.Quantity <= product.ReorderingPoint)
                product.HasReachedROP = true;

          _unitOfWork.ProductsInventory.Update(product);

        }





    }
}

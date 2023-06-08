using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domains.Interfaces.IUnitOfWork;
using ERP_Domians.Models;
using GP_ERP_SYSTEM_v1._0.DTOs;
using GP_ERP_SYSTEM_v1._0.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace GP_ERP_SYSTEM_v1._0.Controllers
{
    [Route("api/[action]")]
    [ApiController]
  // [Authorize(Roles = "Admin,SCM,IM")]
    public class ProductInventoryController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductInventoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductInInventoryById(int id)
        {


            if (id <= 0)
                return BadRequest(new ErrorValidationResponse() { Errors = new List<string> { "Id can't be 0 or less." } });


            try
            {
                var productInventory = await _unitOfWork.ProductsInventory
                                              .GetProductInventoryWithProductAndCategoryDetails(id);

                if (productInventory == null)
                    return NotFound(new ErrorApiResponse(404,"Inventory Product is not Found") );

                return Ok(_mapper.Map<ProductInventoryDTO>(productInventory));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorExceptionResponse(500, null, ex.Message));

            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAllProductsInInventory()
        {
            try
            {
                var productsInventory = await _unitOfWork.ProductsInventory.GetAllProductsInventoryWithProductAndCategoryDetails();


                return Ok(_mapper.Map<List<ProductInventoryDTO>>(productsInventory));
            }
            catch (Exception ex)
            {
            return StatusCode(500, new ErrorExceptionResponse(500, null, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddAProductToInventory([FromBody] AddProductInventoryDTO addProductInventoryDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {

                var Product = await _unitOfWork.Product.GetByIdAsync(addProductInventoryDTO.ProductId);

                if (Product == null)
                    return BadRequest( new ErrorApiResponse(400,"Invalid ProductId is sent."));

                var ProductInventory = await _unitOfWork.ProductsInventory.GetByIdAsync(addProductInventoryDTO.ProductId);

                if (ProductInventory != null)
                    return BadRequest(new ErrorApiResponse(400,"Product is already included in the inventory."));


                _unitOfWork.ProductsInventory.InsertAsync(_mapper.Map<TbFinishedProductsInventory>(addProductInventoryDTO));
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
            return StatusCode(500, new ErrorExceptionResponse(500, null, ex.Message));
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateProductInInventory([FromBody] AddProductInventoryDTO addProductInventoryDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (addProductInventoryDTO.ProductId <= 0)
                return BadRequest(new ErrorValidationResponse() { Errors = new List<string> { "Id can't be 0 or less." } });


            try
            {
                var productInInventoryToUpdate = await _unitOfWork.ProductsInventory.GetByIdAsync(addProductInventoryDTO.ProductId);

                if (productInInventoryToUpdate == null)
                    return NotFound(new ErrorApiResponse(404,"Product Inventory is not found."));


                _mapper.Map(addProductInventoryDTO, productInInventoryToUpdate);

                _unitOfWork.ProductsInventory.Update(productInInventoryToUpdate);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
            return StatusCode(500, new ErrorExceptionResponse(500, null, ex.Message));
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductFromInventory(int id)
        {
          
            if (id <= 0)
                return BadRequest(new ErrorValidationResponse() { Errors = new List<string> { "Id can't be 0 or less." } });


            try
            {
                var productInventoryToDelete = await _unitOfWork.ProductsInventory.GetByIdAsync(id);

                if (productInventoryToDelete == null)
                    return NotFound(new ErrorApiResponse(404, "Inventory Product is not found."));

                _unitOfWork.ProductsInventory.Delete(productInventoryToDelete);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorExceptionResponse(500, null, ex.Message));

            }
        }



    }
}

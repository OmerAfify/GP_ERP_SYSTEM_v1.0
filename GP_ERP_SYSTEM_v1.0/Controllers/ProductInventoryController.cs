using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domains.Interfaces.IUnitOfWork;
using ERP_Domians.Models;
using GP_ERP_SYSTEM_v1._0.DTOs;
using Microsoft.AspNetCore.Mvc;


namespace GP_ERP_SYSTEM_v1._0.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class ProductInventoryController : ControllerBase
    {

        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public ProductInventoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductInInventoryById(int id)
        {
            
            try
            {
                var productInventory = await _unitOfWork.ProductsInventory
                                              .GetProductInventoryWithProductAndCategoryDetails(id);

                if (productInventory == null)
                    return BadRequest("Id is not found.");

                return Ok(_mapper.Map<ProductInventoryDTO>(productInventory));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
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
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddAProductToInventory([FromBody] AddProductInventoryDTO addProductInventoryDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList());

            }

            try
            {

                var Products = await _unitOfWork.Product.GetAllAsync();
                var ValidProductsIds = Products.Select(m => m.ProductId);

                if (!ValidProductsIds.Contains(addProductInventoryDTO.ProductId))
                    return BadRequest("Invalid ProductId is being selected.");


                if (await _unitOfWork.ProductsInventory.GetByIdAsync(addProductInventoryDTO.ProductId)!=null)
                    return BadRequest("ProductId is already included.");

 
                _unitOfWork.ProductsInventory.InsertAsync(_mapper.Map<TbFinishedProductsInventory>(addProductInventoryDTO));
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateProductInInventory([FromBody] AddProductInventoryDTO addProductInventoryDTO)
        {
            if (!ModelState.IsValid || addProductInventoryDTO.ProductId < 1)
            {
               return BadRequest(ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList());
                
            }

            try
            {
                var productInInventoryToUpdate = await _unitOfWork.ProductsInventory.GetByIdAsync(addProductInventoryDTO.ProductId);

                if (productInInventoryToUpdate == null)
                    return BadRequest("Submitted productId is invalid.");


                _mapper.Map(addProductInventoryDTO, productInInventoryToUpdate);

                _unitOfWork.ProductsInventory.Update(productInInventoryToUpdate);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteProductFromInventory(int id)
        {
            if (id < 1)
            {
                return BadRequest("Id cannot be 0 or less.");
            }

            try
            {
                var productInventoryToDelete = await _unitOfWork.ProductsInventory.GetByIdAsync(id);

                if (productInventoryToDelete == null)
                    return BadRequest("Invalid Id is submitted.");

                _unitOfWork.ProductsInventory.Delete(productInventoryToDelete);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error. " + ex.Message);
            }
        }




      


    }
}

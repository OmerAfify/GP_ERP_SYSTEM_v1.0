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
    public class ProductController : ControllerBase
    {

        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
               var products = await _unitOfWork.Product.GetAllAsync(new List<string>(){ "Category" });
               return Ok(_mapper.Map<List<ProductDTO>>(products));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await _unitOfWork.Product.FindAsync(p => p.ProductId == id,
                                                                 new List<string>() { "Category" });
                return Ok(_mapper.Map<ProductDTO>(product));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddNewProduct([FromBody] AddProductDTO product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            if (!this.ValidateCategoryId(product.CategoryId))
                  return BadRequest("invalid CategoryID is selected");

            try
            {
                _unitOfWork.Product.InsertAsync(_mapper.Map<TbProduct>(product));
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] AddProductDTO product)
        {
            if (!ModelState.IsValid || id < 1)
            {
                return BadRequest(ModelState);
            }

            if (!this.ValidateCategoryId(product.CategoryId))
                return BadRequest("invalid CategoryID is selected");

            try
            {
                var productToUpdate = await _unitOfWork.Product.GetByIdAsync(id);

                if (productToUpdate == null)
                    return BadRequest("submitted productId is invalid.");


                 _mapper.Map(product, productToUpdate);

                _unitOfWork.Product.Update(productToUpdate);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (id < 1)
            {
                return BadRequest("Id cannot be 0 or less.");
            }

            try
            {
                var productToDelete = await _unitOfWork.Product.GetByIdAsync(id);

                if (productToDelete == null)
                    return BadRequest("Invalid Id is submitted.");

                _unitOfWork.Product.Delete(productToDelete);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error. " + ex.Message);
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
        public async Task<IActionResult> AddAProductToInventory(AddProductInventoryDTO addProductInventoryDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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


        //private helper methods 

        private bool ValidateCategoryId(int catId)
        {
            var categoriesIdsList = _unitOfWork.Category.GetAllAsync().Result.Select(c => c.CategoryId);

            return (categoriesIdsList.Contains(catId))? true : false;
            
        }



    }
}

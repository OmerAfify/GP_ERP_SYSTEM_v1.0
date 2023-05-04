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
    [Authorize(Roles = "Admin,SCM")]
    public class ProductController : ControllerBase
    {

        private readonly  IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

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
                return StatusCode(500, new ErrorExceptionResponse(500,null,ex.Message));
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
          
            if (id <= 0)
                return BadRequest(new ErrorValidationResponse() { Errors = new List<string> { "Id can't be 0 or less." } });

            try
            {
                var product = await _unitOfWork.Product.FindAsync(p => p.ProductId == id,
                                                                 new List<string>() { "Category" });

                if (product == null)
                    return BadRequest(new ErrorApiResponse(404, "Product Not Found."));

                return Ok(_mapper.Map<ProductDTO>(product));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorExceptionResponse(500, null, ex.Message));
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
                  return BadRequest(new ErrorApiResponse(400,"Invalid Category Id is Sent.") );

            try
            {
                _unitOfWork.Product.InsertAsync(_mapper.Map<TbProduct>(product));
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorExceptionResponse(500, null, ex.Message));
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] AddProductDTO product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id <= 0)
                return BadRequest(new ErrorValidationResponse() { Errors = new List<string> { "Id can't be 0 or less." } });


            if (!this.ValidateCategoryId(product.CategoryId))
                return BadRequest(new ErrorApiResponse(400,"Invalid CategoryId is sent."));

            try
            {
                var productToUpdate = await _unitOfWork.Product.GetByIdAsync(id);

                if (productToUpdate == null)
                    return BadRequest(new ErrorApiResponse(400, "Invalid ProductID is sent."));


                _mapper.Map(product, productToUpdate);

                _unitOfWork.Product.Update(productToUpdate);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorExceptionResponse(500, null, ex.Message));
            }
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {

            if (id <= 0)
                return BadRequest(new ErrorValidationResponse() { Errors = new List<string> { "Id can't be 0 or less." } });
       

            try
            {
                var productToDelete = await _unitOfWork.Product.GetByIdAsync(id);

                if (productToDelete == null)
                    return BadRequest(new ErrorApiResponse(400, "Invalid Product ID is sent."));

                _unitOfWork.Product.Delete(productToDelete);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorExceptionResponse(500, null, ex.Message));
            }
        }



        //private helper methods 

        private bool ValidateCategoryId(int catId)
        {
            var categoriesIdsList = _unitOfWork.Category.GetAllAsync().Result.Select(c => c.CategoryId);

            return categoriesIdsList.Contains(catId);
            
        }



    }
}

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

  //  [Authorize(Roles ="Admin,SCM")]
    public class CategoryController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
               var Categories = await _unitOfWork.Category.GetAllAsync();
               return Ok(_mapper.Map<List<CategoryDTO>>(Categories));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorExceptionResponse(500, null, ex.Message));
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                var Category = await _unitOfWork.Category.GetByIdAsync(id);

                if (Category == null)
                    return NotFound(new ErrorApiResponse(404, "category Id is not found."));

                return Ok(_mapper.Map<CategoryDTO>(Category));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorExceptionResponse(500, null, ex.Message));
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddNewCategory([FromBody] AddCategoryDTO Category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            try
            {
                _unitOfWork.Category.InsertAsync(_mapper.Map<TbCategory>(Category));
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorExceptionResponse(500, null, ex.Message));
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] AddCategoryDTO Category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            if (id <= 0)
                return BadRequest(new ErrorValidationResponse() { Errors = new List<string> { "Id can't be 0 or less."} });
            
            try
            {
                var CategoryToUpdate = await _unitOfWork.Category.GetByIdAsync(id);

                if (CategoryToUpdate == null)
                    return BadRequest(new ErrorApiResponse(400,"Category Id is not found.") );


                 _mapper.Map(Category, CategoryToUpdate);

                _unitOfWork.Category.Update(CategoryToUpdate);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorExceptionResponse(500, null, ex.Message));
            }
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);     

            if (id <= 0)
                return BadRequest(new ErrorValidationResponse() { Errors = new List<string> { "Id can't be 0 or less." } });
        
            try
            {
                var CategoryToDelete = await _unitOfWork.Category.GetByIdAsync(id);

                if (CategoryToDelete == null)
                    return BadRequest(new ErrorApiResponse(400, "Category Id is not found."));

                _unitOfWork.Category.Delete(CategoryToDelete);
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

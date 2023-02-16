using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domains.Interfaces.IUnitOfWork;
using ERP_Domians.Models;
using GP_ERP_SYSTEM_v1._0.DTOs;

namespace GP_ERP_SYSTEM_v1._0.Controllers
{

    [Route("api/[action]")]
    [ApiController]
    public class FmsCategoryController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public FmsCategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> FmsGetAllCategories()
        {
            try
            {
                var Categories = await _unitOfWork.FmsCategory.GetAllAsync();
                return Ok(_mapper.Map<List<FmsCategoryDTO>>(Categories));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> FmsGetCategoryById(int id)
        {
            try
            {
                var Category = await _unitOfWork.FmsCategory.GetByIdAsync(id);
                return Ok(_mapper.Map<FmsCategoryDTO>(Category));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> FmsAddCategory([FromBody] AddFmsCategoryDTO Category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _unitOfWork.FmsCategory.InsertAsync(_mapper.Map<TbFmsCategory>(Category));
                await _unitOfWork.Save();
                return Ok();
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }


        [HttpPut("{id}")]
        public async Task<IActionResult> FmsUpdateCategory(int id, [FromBody] AddFmsCategoryDTO Category)
        {
            if (!ModelState.IsValid || id < 1) { return BadRequest(ModelState); }
            try
            {
                var CategoryToUpdate = await _unitOfWork.FmsCategory.GetByIdAsync(id);
                if (CategoryToUpdate == null) { return BadRequest("Submitted ID is Invalid."); }
                _mapper.Map(Category, CategoryToUpdate);
                _unitOfWork.FmsCategory.Update(CategoryToUpdate);
                await _unitOfWork.Save();
                return NoContent();
            }

            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }

        [HttpDelete]
        public async Task<IActionResult> FmsDeleteCategory(int id)
        {
            if (id < 1)
            {
                return BadRequest("Id cannot be 0 or less.");
            }

            try
            {
                var FmsCategoryToDelete = await _unitOfWork.FmsCategory.GetByIdAsync(id);

                if (FmsCategoryToDelete == null)
                    return BadRequest("Invalid Id is submitted.");

                _unitOfWork.FmsCategory.Delete(FmsCategoryToDelete);
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

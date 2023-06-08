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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GP_ERP_SYSTEM_v1._0.Controllers
{
    [Route("api/[action]")]
    [ApiController]
  //  [Authorize(Roles = "Admin,SCM")]
    public class RawMaterialController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RawMaterialController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllRawMaterials()
        {
            try
            {
                var RawMaterials = await _unitOfWork.RawMaterial.GetAllAsync();
                return Ok(_mapper.Map<List<RawMaterialDTO>>(RawMaterials));
            }
            catch (Exception ex)
            {
                 return StatusCode(500, new ErrorExceptionResponse(500, null, ex.Message));
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetRawMaterialById(int id)
        {

            if (id <= 0)
                return BadRequest(new ErrorValidationResponse() { Errors = new List<string> { "Id can't be 0 or less." } });

            try
            {
                var RawMaterial = await _unitOfWork.RawMaterial.GetByIdAsync(id);

                if(RawMaterial == null)
                        NotFound(new ErrorApiResponse(404, "rawmaterial is not found."));

                return Ok(_mapper.Map<RawMaterialDTO>(RawMaterial));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorExceptionResponse(500, null, ex.Message));
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddNewRawMaterial([FromBody] AddRawMaterialDTO RawMaterial)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _unitOfWork.RawMaterial.InsertAsync(_mapper.Map<TbRawMaterial>(RawMaterial));
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorExceptionResponse(500, null, ex.Message));
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRawMaterial(int id, [FromBody] AddRawMaterialDTO RawMaterial)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id <= 0)
                return BadRequest(new ErrorValidationResponse() { Errors = new List<string> { "Id can't be 0 or less." } });
            try
            {
                var RawMaterialToUpdate = await _unitOfWork.RawMaterial.GetByIdAsync(id);

                if (RawMaterialToUpdate == null)
                    return NotFound(new ErrorApiResponse(404,"RawMaterial is not found."));


                _mapper.Map(RawMaterial, RawMaterialToUpdate);

                _unitOfWork.RawMaterial.Update(RawMaterialToUpdate);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorExceptionResponse(500, null, ex.Message));
            }
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteRawMaterial(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ErrorApiResponse(400,"Id cannot be 0 or less."));
            }

            try
            {
                var RawMaterialToDelete = await _unitOfWork.RawMaterial.GetByIdAsync(id);

                if (RawMaterialToDelete == null)
                    return NotFound(new ErrorApiResponse(404, "Rawmaterial is not found."));

                _unitOfWork.RawMaterial.Delete(RawMaterialToDelete);
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

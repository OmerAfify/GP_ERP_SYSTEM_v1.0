using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domains.Interfaces.IUnitOfWork;
using ERP_Domians.Models;
using GP_ERP_SYSTEM_v1._0.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GP_ERP_SYSTEM_v1._0.Controllers
{
    [Route("api/[action]")]
    [ApiController]
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
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetRawMaterialById(int id)
        {
            try
            {
                var RawMaterial = await _unitOfWork.RawMaterial.GetByIdAsync(id);
                return Ok(_mapper.Map<RawMaterialDTO>(RawMaterial));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddNewRawMaterial([FromBody] AddRawMaterialDTO RawMaterial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _unitOfWork.RawMaterial.InsertAsync(_mapper.Map<TbRawMaterial>(RawMaterial));
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRawMaterial(int id, [FromBody] AddRawMaterialDTO RawMaterial)
        {
            if (!ModelState.IsValid || id < 1)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var RawMaterialToUpdate = await _unitOfWork.RawMaterial.GetByIdAsync(id);

                if (RawMaterialToUpdate == null)
                    return BadRequest("submitted RawMaterialId is invalid.");


                _mapper.Map(RawMaterial, RawMaterialToUpdate);

                _unitOfWork.RawMaterial.Update(RawMaterialToUpdate);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteRawMaterial(int id)
        {
            if (id < 1)
            {
                return BadRequest("Id cannot be 0 or less.");
            }

            try
            {
                var RawMaterialToDelete = await _unitOfWork.RawMaterial.GetByIdAsync(id);

                if (RawMaterialToDelete == null)
                    return BadRequest("Invalid Id is submitted.");

                _unitOfWork.RawMaterial.Delete(RawMaterialToDelete);
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

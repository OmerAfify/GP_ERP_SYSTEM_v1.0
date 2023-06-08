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
  //  [Authorize(Roles = "Admin,SCM,IM")]
    public class RawMaterialInventoryController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RawMaterialInventoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllRawMaterialsInventory()
        {
            try
            {
                var RawMaterialsInventory = await _unitOfWork.RawMaterialInventory.GetAllAsync(new List<string>{ "Material" });
                return Ok(_mapper.Map<List<RawMaterialInventoryDTO>>(RawMaterialsInventory));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorExceptionResponse(500, null, ex.Message));
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetRawMaterialInventoryById(int id)
        {

            if (id <= 0)
                return BadRequest(new ErrorValidationResponse() { Errors = new List<string> { "Id can't be 0 or less." } });

            try
            {
                var RawMaterialInventory = await _unitOfWork.RawMaterialInventory.FindAsync(r=>r.MaterialId==id, new List<string> { "Material"});

                if (RawMaterialInventory == null)
                    return NotFound(new ErrorApiResponse(404, "Raw material in inventory is not found."));
                
                return Ok(_mapper.Map<RawMaterialInventoryDTO>(RawMaterialInventory));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorExceptionResponse(500, null, ex.Message));
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddNewRawMaterialToInventory([FromBody] AddRawMaterialInventoryDTO AddRawMaterialInventoryDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (AddRawMaterialInventoryDTO.MaterialId <= 0)
                return BadRequest(new ErrorValidationResponse() { Errors = new List<string> { "Material Id can't be 0 or less." } });

            try
            {
                var rawMaterial = await _unitOfWork.RawMaterial.GetByIdAsync(AddRawMaterialInventoryDTO.MaterialId);

                if (rawMaterial == null)
                    return BadRequest(new ErrorApiResponse(400, "Raw Material is not found."));

                var rawMaterialInventory = await _unitOfWork.RawMaterialInventory.GetByIdAsync(AddRawMaterialInventoryDTO.MaterialId);

                if (rawMaterialInventory != null)
                    return BadRequest(new ErrorApiResponse(400, "Raw Material already exists in the inventory."));


                _unitOfWork.RawMaterialInventory.InsertAsync(_mapper.Map<TbRawMaterialsInventory>(AddRawMaterialInventoryDTO));
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorExceptionResponse(500, null, ex.Message));
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateRawMaterialInInventory([FromBody] AddRawMaterialInventoryDTO UpdateRawMaterialInventoryDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (UpdateRawMaterialInventoryDTO.MaterialId <= 0)
                return BadRequest(new ErrorValidationResponse() { Errors = new List<string> { "Id can't be 0 or less." } });

            try
            {

                var rawMaterialInventoryToUpdate = await _unitOfWork.RawMaterialInventory
                                                          .GetByIdAsync(UpdateRawMaterialInventoryDTO.MaterialId);


                if (rawMaterialInventoryToUpdate == null)
                    return BadRequest(new ErrorApiResponse(400, "submitted RawMaterialInventory  is not found."));



                _mapper.Map(UpdateRawMaterialInventoryDTO, rawMaterialInventoryToUpdate);

                _unitOfWork.RawMaterialInventory.Update(rawMaterialInventoryToUpdate);

                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorExceptionResponse(500, null, ex.Message));
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRawMaterialFromInventory(int id)
        {

            if (id <= 0)
                return BadRequest(new ErrorValidationResponse() { Errors = new List<string> { "Id can't be 0 or less." } });


            try
            {
                var RawMaterialInventoryToDelete = await _unitOfWork.RawMaterialInventory.GetByIdAsync(id);

                if (RawMaterialInventoryToDelete == null)
                    return NotFound(new ErrorApiResponse(404,"Raw Material is not found."));

                _unitOfWork.RawMaterialInventory.Delete(RawMaterialInventoryToDelete);
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

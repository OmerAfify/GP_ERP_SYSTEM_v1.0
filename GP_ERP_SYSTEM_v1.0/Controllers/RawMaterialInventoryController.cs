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
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetRawMaterialInventoryById(int id)
        {
            try
            {
                var RawMaterialInventory = await _unitOfWork.RawMaterialInventory.FindAsync(r=>r.MaterialId==id, new List<string> { "Material"});
                return Ok(_mapper.Map<RawMaterialInventoryDTO>(RawMaterialInventory));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddNewRawMaterialToInventory([FromBody] AddRawMaterialInventoryDTO AddRawMaterialInventoryDTO)
        {
           if (!ModelState.IsValid)
                    return BadRequest(ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList());            

            try
            {
                var rawMaterial = await _unitOfWork.RawMaterial.GetByIdAsync(AddRawMaterialInventoryDTO.MaterialId);

                if (rawMaterial == null)
                    return BadRequest("Raw Material id is invalid.");

                var rawMaterialInventory = await _unitOfWork.RawMaterialInventory.GetByIdAsync(AddRawMaterialInventoryDTO.MaterialId);

                if (rawMaterialInventory != null)
                    return BadRequest("This material already exists in the inventory.");


                _unitOfWork.RawMaterialInventory.InsertAsync(_mapper.Map<TbRawMaterialsInventory>(AddRawMaterialInventoryDTO));
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateRawMaterialInInventory([FromBody] AddRawMaterialInventoryDTO UpdateRawMaterialInventoryDTO)
        {
            if (!ModelState.IsValid || UpdateRawMaterialInventoryDTO.MaterialId < 1)
                 return BadRequest(ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList());

            try
            {

                var rawMaterialInventoryToUpdate = await _unitOfWork.RawMaterialInventory
                                                          .GetByIdAsync(UpdateRawMaterialInventoryDTO.MaterialId);


                if (rawMaterialInventoryToUpdate == null)
                    return BadRequest("submitted RawMaterialInventory ID is invalid.");



                _mapper.Map(UpdateRawMaterialInventoryDTO, rawMaterialInventoryToUpdate);

                _unitOfWork.RawMaterialInventory.Update(rawMaterialInventoryToUpdate);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRawMaterialFromInventory(int id)
        {
            if (id < 1)
            {
                return BadRequest("Id cannot be 0 or less.");
            }

            try
            {
                var RawMaterialInventoryToDelete = await _unitOfWork.RawMaterialInventory.GetByIdAsync(id);

                if (RawMaterialInventoryToDelete == null)
                    return BadRequest("Invalid Id is submitted.");

                _unitOfWork.RawMaterialInventory.Delete(RawMaterialInventoryToDelete);
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

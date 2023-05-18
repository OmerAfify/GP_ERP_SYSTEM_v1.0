using AutoMapper;
using Domains.Interfaces.IUnitOfWork;
using ERP_Domians.Models;
using GP_ERP_SYSTEM_v1._0.DTOs;
using GP_ERP_SYSTEM_v1._0.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GP_ERP_SYSTEM_v1._0.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    [Authorize(Roles = "Admin,HR")]
    public class HRManagerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HRManagerController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllHRMangers()
        {
            try
            {
                var HRManagers = await _unitOfWork.HRManager.GetAllAsync();
                return Ok(_mapper.Map<List<HRManagerDTO>>(HRManagers));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error" + ex.Message);
            }
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetHRManagerByID(int id)
        {
            try
            {
                var HRManagerId = await _unitOfWork.HRManager.GetByIdAsync(id);
                if (HRManagerId == null)
                    return NotFound(new ErrorApiResponse(404, "HR Manager Id is not found."));
                return Ok(_mapper.Map<AddHRManagerDTO>(HRManagerId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error" + ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddNewHRManager([FromBody] AddHRManagerDTO hRManager)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _unitOfWork.HRManager.InsertAsync(_mapper.Map<TbHrmanagerDetail>(hRManager));
                await _unitOfWork.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error" + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHRManager(int id, [FromBody] AddHRManagerDTO hRManager)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id <= 0)
                return BadRequest(new ErrorValidationResponse() { Errors = new List<string> { "Id can't be 0 or less." } });
            try
            {
                var hRManagerIdToUpdate = await _unitOfWork.HRManager.GetByIdAsync(id);
                if (hRManagerIdToUpdate == null)
                    return BadRequest(new ErrorApiResponse(400,"Invalid HRManager's Id Is Submitted"));

                _mapper.Map(hRManager, hRManagerIdToUpdate);

                _unitOfWork.HRManager.Update(hRManagerIdToUpdate);

                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error" + ex.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleletHRManagerById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id <= 0)
                return BadRequest(new ErrorValidationResponse() { Errors = new List<string> { "Id can't be 0 or less." } });
            try
            {
                var HRManagerIdToDelete = await _unitOfWork.HRManager.GetByIdAsync(id);
                if (HRManagerIdToDelete == null)
                    return BadRequest(new ErrorApiResponse(400, "Invalid HRManager's Id Is Submitted"));
                _unitOfWork.HRManager.Delete(HRManagerIdToDelete);
                await _unitOfWork.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error" + ex.Message);
            }
        }

    } 

}

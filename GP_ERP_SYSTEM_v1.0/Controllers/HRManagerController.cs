using AutoMapper;
using Domains.Interfaces.IUnitOfWork;
using ERP_Domians.Models;
using GP_ERP_SYSTEM_v1._0.DTOs;
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
                return Ok(_mapper.Map<AddHRManagerDTO>(HRManagerId));
            }
            catch(Exception ex)
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
            if (!ModelState.IsValid || id < 1)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var hRManagerIdToUpdate = await _unitOfWork.HRManager.GetByIdAsync(id);
                if (hRManagerIdToUpdate == null)
                    return BadRequest("Invalid HRManager's Id Is Submitted");

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
            if (id < 1)
            {
                return BadRequest("ID can't be 0 or less");
            }
            try
            {
                var HRManagerIdToDelete = await _unitOfWork.HRManager.GetByIdAsync(id);
                if (HRManagerIdToDelete == null)
                    return BadRequest("Invalid HRManager's Id Is Submitted");
                _unitOfWork.HRManager.Delete(HRManagerIdToDelete);
                await _unitOfWork.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error" + ex.Message);
            }
        }
       //Trainning Employee

        [HttpPost]
        public async Task<IActionResult> AddNewEmployeeTrainnig([FromBody] EmployeeTrainningDTO employeeTrainning)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _unitOfWork.EmployeeTrainning.InsertAsync(_mapper.Map<TbEmployeeTrainning>(employeeTrainning));
                await _unitOfWork.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error" + ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTrainnig(int id, [FromBody] AddEmployeeTrainningDTO employeeTrainning)
        {
            if (!ModelState.IsValid || id < 1)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var EmployeeTrainnigToUpdate = await _unitOfWork.EmployeeTrainning.GetByIdAsync(id);
                if (EmployeeTrainnigToUpdate == null)
                    return BadRequest("Invalid Trainning's Id Is Submitted");

                _mapper.Map(employeeTrainning, EmployeeTrainnigToUpdate);

                _unitOfWork.EmployeeTrainning.Update(EmployeeTrainnigToUpdate);

                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error" + ex.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleletEmployeeTrainningById(int id)
        {
            if (id < 1)
            {
                return BadRequest("ID can't be 0 or less");
            }
            try
            {
                var EmployeeTrainningIdToDelete = await _unitOfWork.EmployeeTrainning.GetByIdAsync(id);
                if (EmployeeTrainningIdToDelete == null)
                    return BadRequest("Invalid EmployeeTrainning's Id Is Submitted");
                _unitOfWork.EmployeeTrainning.Delete(EmployeeTrainningIdToDelete);
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

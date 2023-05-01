using AutoMapper;
using Domains.Interfaces.IUnitOfWork;
using ERP_Domians.Models;
using GP_ERP_SYSTEM_v1._0.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace GP_ERP_SYSTEM_v1._0.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class EmployeeTaskController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeTaskController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllEmployeeTasks()
        {
            try
            {
                var EmployeesTasks = await _unitOfWork.EmployeeTaskDetail.GetAllAsync();
                return Ok(_mapper.Map<List<EmployeeTaskDTO>>(EmployeesTasks));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeTaskById(int id)
        {
            try
            {
                var EmployeeTaskId = await _unitOfWork.EmployeeTaskDetail.GetByIdAsync(id);
                return Ok(_mapper.Map<EmployeeTaskDTO>(EmployeeTaskId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error" + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddNewEmployee([FromBody] AddEmployeeTaskDTO employeeTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _unitOfWork.EmployeeTaskDetail.InsertAsync(_mapper.Map<TbEmployeeTaskDetail>(employeeTask));
                await _unitOfWork.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error" + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployeeTask(int id, [FromBody] AddEmployeeTaskDTO employeeTask)
        {
            if (!ModelState.IsValid || id < 1)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var employeeTaskIdToUpdate = await _unitOfWork.EmployeeTaskDetail.GetByIdAsync(id);
                if (employeeTaskIdToUpdate == null)
                    return BadRequest("Invalid Employee's Id Is Submitted");

                _mapper.Map(employeeTask, employeeTaskIdToUpdate);

                _unitOfWork.EmployeeTaskDetail.Update(employeeTaskIdToUpdate);

                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleletEmployeeTaskById(int id)
        {
            if (id < 1)
            {
                return BadRequest("ID can't be 0 or less");
            }
            try
            {
                var EmployeeTaskIdToDelete = await _unitOfWork.EmployeeTaskDetail.GetByIdAsync(id);
                if (EmployeeTaskIdToDelete == null)
                    return BadRequest("Invalid Employee's Id Is Submitted");
                _unitOfWork.EmployeeTaskDetail.Delete(EmployeeTaskIdToDelete);
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

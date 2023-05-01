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
    public class EmployeeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllEmployee()
        {
            try
            {
                var Employees = await _unitOfWork.Employee.GetAllAsync();

                return Ok(_mapper.Map<List<EmployeeDetailsDTO>>(Employees));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                var EmployeeId = await _unitOfWork.Employee.GetByIdAsync(id);

                return Ok(_mapper.Map<EmployeeDetailsDTO>(EmployeeId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error" + ex.Message);
            }
        }

       /* [HttpPost]
        public async Task<IActionResult> AddNewEmployee([FromBody] AddEmployeeDTO Employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _unitOfWork.Employee.InsertAsync(_mapper.Map<TbEmployeeDetail>(Employee));
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error" + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] AddEmployeeDTO Employee)
        {
            if (!ModelState.IsValid || id < 1)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var employeeIdToUpdate = await _unitOfWork.Employee.GetByIdAsync(id);

                if (employeeIdToUpdate == null)
                    return BadRequest("Invalid Employee's Id Is Submitted");

                _mapper.Map(Employee, employeeIdToUpdate);

                _unitOfWork.Employee.Update(employeeIdToUpdate);

                await _unitOfWork.Save();


                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error" + ex.Message);
            }
        }*/
        [HttpDelete]
        public async Task<IActionResult> DeleletEmployeeById(int id)
        {
            if (id < 1)
            {
                return BadRequest("ID can't be 0 or less");
            }
            try
            {
                var EmployeeIdToDelete = await _unitOfWork.Employee.GetByIdAsync(id);

                if (EmployeeIdToDelete == null)
                    return BadRequest("Invalid Employee's Id Is Submitted");

                _unitOfWork.Employee.Delete(EmployeeIdToDelete);

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

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
    [Authorize(Roles = "Admin,HRMS")]
    public class EmployeeTrainningController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EmployeeTrainningController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllTrainingEmployee()
        {
            try
            {
                var Training = await _unitOfWork.TrainningEmployee.GetAllEmployeeTrainningWithEmployeeeAndHRManager();
                return Ok(_mapper.Map<List<EmployeeTrainningDTO>>(Training));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorExceptionResponse(500, null, ex.Message));
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetEmployeeTraining(int id)
        {
            if (id <= 0)
                return BadRequest(new ErrorValidationResponse() { Errors = new List<string> { "Id can't be 0 or less." } });
            try
            {
                var training = await _unitOfWork.TrainningEmployee.GetEmployeeTrainningWithEmployeeeAndHRManager(id);
                if (training == null)
                    return NotFound(new ErrorApiResponse(404, "Inventory Product is not Found"));
                return Ok(_mapper.Map<List<EmployeeTrainningDTO>>(training));

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorExceptionResponse(500, null, ex.Message));
            }
        }
  [HttpPost]
  public async Task<IActionResult> CreateNewEmployeeTrainnig([FromBody] EmployeeTrainningDTO employeeTrainning)
  {
      if (!ModelState.IsValid)
      {
          return BadRequest(ModelState);
      }
      try
      {
                var Employees = await _unitOfWork.Employee.GetAllAsync();
                foreach(var employee in Employees)
                {
                    if(employee.HoursWorked <= 1 )
                    {
                        _unitOfWork.TrainningEmployee.InsertAsync(_mapper.Map<TbEmployeeTrainning>(employeeTrainning));
                        
                    }
                    await _unitOfWork.Save();

                }
         
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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (id <= 0)
                return BadRequest(new ErrorValidationResponse() { Errors = new List<string> { "Id can't be 0 or less." } });

            try
            {
          var EmployeeTrainnigToUpdate = await _unitOfWork.TrainningEmployee.GetByIdAsync(id);
          if (EmployeeTrainnigToUpdate == null)
              return NotFound(new ErrorApiResponse(404,"Invalid Trainning's Id Is Submitted"));

          _mapper.Map(employeeTrainning, EmployeeTrainnigToUpdate);

          _unitOfWork.TrainningEmployee.Update(EmployeeTrainnigToUpdate);

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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (id <= 0)
                return BadRequest(new ErrorValidationResponse() { Errors = new List<string> { "Id can't be 0 or less." } });
            try
      {
          var EmployeeTrainningIdToDelete = await _unitOfWork.TrainningEmployee.GetByIdAsync(id);
                if (EmployeeTrainningIdToDelete == null)
                    return NotFound(new ErrorApiResponse(404, "Invalid Trainning's Id Is Submitted"));
                _unitOfWork.TrainningEmployee.Delete(EmployeeTrainningIdToDelete);
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

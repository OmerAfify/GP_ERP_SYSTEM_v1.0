using AutoMapper;
using Domains.Interfaces.IUnitOfWork;
using ERP_Domians.Models;
using GP_ERP_SYSTEM_v1._0.DTOs;
using GP_ERP_SYSTEM_v1._0.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GP_ERP_SYSTEM_v1._0.Controllers
{
    [Route("api/[action]")]
    [ApiController]

   //  [Authorize(Roles = "Admin,HR")]
    public class EmployeeTrainingController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EmployeeTrainingController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllTrainingEmployee()
        {
            try
            {
                var Trainning = await _unitOfWork.TrainningEmployee.GetAllAsync(new List<string>() { "Hrmanger", "Employee" });

                return Ok(_mapper.Map<List<EmployeeTrainningDTO>>(Trainning));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetEmployeeTrainingById(int id)
        {
            if (id <= 0)
                return BadRequest(new ErrorValidationResponse() { Errors = new List<string> { "Id can't be 0 or less." } });

            try
            {
                var TrainningId = await _unitOfWork.TrainningEmployee.FindAsync(P => P.TrainnningId == id, new List<string>() { "Hrmanger", "Employee" });
                if (TrainningId == null)
                    return BadRequest(new ErrorApiResponse(404, "Trainning Not Found."));

                return Ok(_mapper.Map<EmployeeTrainningDTO>(TrainningId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error" + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTrainning([FromBody] AddEmployeeTrainningDTO Trainning)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
            try
            {
                _unitOfWork.TrainningEmployee.InsertAsync(_mapper.Map<TbEmployeeTrainning>(Trainning));
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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (id <= 0)
                return BadRequest(new ErrorValidationResponse() { Errors = new List<string> { "Id can't be 0 or less." } });

            try
            {
                var EmployeeTrainnigToUpdate = await _unitOfWork.TrainningEmployee.GetByIdAsync(id);
                if (EmployeeTrainnigToUpdate == null)
                    return NotFound(new ErrorApiResponse(404, "Invalid Employee Trainning's Id Is Submitted"));

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

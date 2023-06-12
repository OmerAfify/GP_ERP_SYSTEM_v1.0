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
    //[Authorize(Roles = "Admin,CRM")]
    public class CustomerTaskController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerTaskController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        private bool ValidateCustomerId(int CustomerId)
        {
            var CustomerIdsList = _unitOfWork.Customer.GetAllAsync().Result?.Select(e => e.CustomerId);

            if (CustomerIdsList == null)
                return false;

            return CustomerIdsList.Contains(CustomerId);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCustomerTasks()
        {
            try
            {
                var CustomersTask = await _unitOfWork.Task.GetAllAsync(new List<string>() { "Customer" });

                return Ok(_mapper.Map<List<TaskDTO>>(CustomersTask));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerTasksById(int id)
        {
            if (id <= 0)
                return BadRequest(new ErrorValidationResponse() { Errors = new List<string> { "Id can't be 0 or less." } });

            try
            {
                var CustomerTaskId = await _unitOfWork.Task.FindAsync(P => P.TaskId == id, new List<string>() { "Customer" });
                if (CustomerTaskId == null)
                    return BadRequest(new ErrorApiResponse(404, "   Customer's Task Not Found."));

                return Ok(_mapper.Map<TaskDTO>(CustomerTaskId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error" + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddNewTaskForCustomer([FromBody] AddTaskDTO customerTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _unitOfWork.Task.InsertAsync(_mapper.Map<TbTask>(customerTask));
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error" + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomerTask(int id, [FromBody] AddTaskDTO customerTask)
        {
            if (id <= 0)
                return BadRequest(new ErrorValidationResponse() { Errors = new List<string> { "Id can't be 0 or less." } });


            if (!this.ValidateCustomerId(customerTask.CustomerId))
                return BadRequest(new ErrorApiResponse(400, "Invalid Customer's is sent."));
            try
            {
                var customeTaskToUpdate = await _unitOfWork.Task.GetByIdAsync(id);

                if (customeTaskToUpdate == null)
                    return BadRequest("Invalid Customer's Task Id Is Submitted");

                _mapper.Map(customerTask, customeTaskToUpdate);

                _unitOfWork.Task.Update(customeTaskToUpdate);

                await _unitOfWork.Save();


                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error" + ex.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleletCustomerTaskById(int id)
        {
            if (id < 1)
            {
                return BadRequest("ID can't be 0 or less");
            }
            try
            {
                var CustomerTaskIdToDelete = await _unitOfWork.Task.GetByIdAsync(id);

                if (CustomerTaskIdToDelete == null)
                    return BadRequest("Invalid Custoemr's Id Is Submitted");

                _unitOfWork.Task.Delete(CustomerTaskIdToDelete);

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

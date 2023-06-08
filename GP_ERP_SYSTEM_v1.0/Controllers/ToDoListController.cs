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
    public class ToDoListController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ToDoListController(IUnitOfWork unitOfWork, IMapper mapper)
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
        public async Task<IActionResult> GetAllCustomersToDoList()
        {
            try
            {
                var CustomersToDoList = await _unitOfWork.ToDoList.GetAllAsync(new List<string>() { "Customer" });

                return Ok(_mapper.Map<List<ToDoListDTO>>(CustomersToDoList));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomersToDoListsById(int id)
        {
            if (id <= 0)
                return BadRequest(new ErrorValidationResponse() { Errors = new List<string> { "Id can't be 0 or less." } });

            try
            {
                var CustomersToDoListId = await _unitOfWork.ToDoList.FindAsync(P => P.ToDoListId == id, new List<string>() { "Customer" });
                if (CustomersToDoListId == null)
                    return BadRequest(new ErrorApiResponse(404, "Customer's To Do list Not Found."));

                return Ok(_mapper.Map<ToDoListDTO>(CustomersToDoListId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error" + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddNewToDolistForCustomer([FromBody] AddToDoListDTO customerToDoList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _unitOfWork.ToDoList.InsertAsync(_mapper.Map<TbToDoList>(customerToDoList));
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error" + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomerToDoList(int id, [FromBody] AddToDoListDTO customerToDoList)
        {
            if (id <= 0)
                return BadRequest(new ErrorValidationResponse() { Errors = new List<string> { "Id can't be 0 or less." } });


            if (!this.ValidateCustomerId(customerToDoList.CustomerId))
                return BadRequest(new ErrorApiResponse(400, "Invalid Customer's is sent."));
            try
            {
                var customeToDoListToUpdate = await _unitOfWork.ToDoList.GetByIdAsync(id);

                if (customeToDoListToUpdate == null)
                    return BadRequest("Invalid Customer's TodDoList Id Is Submitted");

                _mapper.Map(customerToDoList, customeToDoListToUpdate);

                _unitOfWork.ToDoList.Update(customeToDoListToUpdate);

                await _unitOfWork.Save();


                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error" + ex.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleletCustomerTODoListById(int id)
        {
            if (id < 1)
            {
                return BadRequest("ID can't be 0 or less");
            }
            try
            {
                var CustomerToDoListIdToDelete = await _unitOfWork.ToDoList.GetByIdAsync(id);

                if (CustomerToDoListIdToDelete == null)
                    return BadRequest("Invalid Custoemr's Id Is Submitted");

                _unitOfWork.ToDoList.Delete(CustomerToDoListIdToDelete);

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

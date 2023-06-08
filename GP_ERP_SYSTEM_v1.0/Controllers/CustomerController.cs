using AutoMapper;
using Domains.Interfaces.IUnitOfWork;
using ERP_Domians.Models;
using GP_ERP_SYSTEM_v1._0.DTOs;
using GP_ERP_SYSTEM_v1._0.Errors;
using Microsoft.AspNetCore.Authorization;
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
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CustomerController(IUnitOfWork unitOfWork,IMapper mapper )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var Customers = await _unitOfWork.Customer.GetAllAsync();
                return Ok(_mapper.Map<List<CustomerDTO>>(Customers));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error" + ex.Message);
            }
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetCutomerrByID(int id)
        {
            try
            {
                var CustomerId = await _unitOfWork.Customer.GetByIdAsync(id);
                if (CustomerId == null)
                    return NotFound(new ErrorApiResponse(404, "Customer Id is not found."));
                return Ok(_mapper.Map<AddCustomerDTO>(CustomerId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error" + ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddCustomerProfile([FromBody] AddCustomerDTO Customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _unitOfWork.Customer.InsertAsync(_mapper.Map<TbCustomer>(Customer));
                await _unitOfWork.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error" + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditCustomerProfile(int id, [FromBody] AddCustomerDTO Customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id <= 0)
                return BadRequest(new ErrorValidationResponse() { Errors = new List<string> { "Id can't be 0 or less." } });
            try
            {
                var customerIdToUpdate = await _unitOfWork.Customer.GetByIdAsync(id);
                if (customerIdToUpdate == null)
                    return BadRequest(new ErrorApiResponse(400, "Invalid Customer's Id Is Submitted"));

                _mapper.Map(Customer, customerIdToUpdate);

                _unitOfWork.Customer.Update(customerIdToUpdate);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error" + ex.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleletCustomerById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id <= 0)
                return BadRequest(new ErrorValidationResponse() { Errors = new List<string> { "Id can't be 0 or less." } });
            try
            {
                var CustomemrIdToDelete = await _unitOfWork.Customer.GetByIdAsync(id);

                if (CustomemrIdToDelete == null)
                    return BadRequest("Invalid Customer's Id Is Submitted");

                _unitOfWork.Customer.Delete(CustomemrIdToDelete);

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

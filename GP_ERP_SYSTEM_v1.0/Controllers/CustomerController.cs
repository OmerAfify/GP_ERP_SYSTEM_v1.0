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
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerController(IUnitOfWork unitOfWork, IMapper mapper)
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
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            try
            {
                var CustomerId = await _unitOfWork.Customer.GetByIdAsync(id);
                return Ok(_mapper.Map<CustomerDTO>(CustomerId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error" + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddNewCustomer([FromBody] AddCustomerDTO Customer)
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
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] AddCustomerDTO Customer)
        {
            if (!ModelState.IsValid || id < 1)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var customerIdToUpdate = await _unitOfWork.Customer.GetByIdAsync(id);
                if (customerIdToUpdate == null)
                    return BadRequest("Invalid Employee's Id Is Submitted");

                _mapper.Map(Customer, customerIdToUpdate);

                _unitOfWork.Customer.Update(customerIdToUpdate);

                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleletCustomerById(int id)
        {
            if (id < 1)
            {
                return BadRequest("ID can't be 0 or less");
            }
            try
            {
                var CustomerIdToDelete = await _unitOfWork.Customer.GetByIdAsync(id);
                if (CustomerIdToDelete == null)
                    return BadRequest("Invalid Employee's Id Is Submitted");
                _unitOfWork.Customer.Delete(CustomerIdToDelete);
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

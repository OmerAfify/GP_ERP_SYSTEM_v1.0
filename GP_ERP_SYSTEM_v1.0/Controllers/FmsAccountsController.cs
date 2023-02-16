using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domains.Interfaces.IUnitOfWork;
using ERP_Domians.Models;
using GP_ERP_SYSTEM_v1._0.DTOs;
using Microsoft.AspNetCore.Mvc;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GP_ERP_SYSTEM_v1._0.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class FmsAccountsController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public FmsAccountsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> FmsGetAllAccounts()
        {
            try
            {
                var Accounts = await _unitOfWork.FmsAccount.GetAllAsync();
                return Ok(_mapper.Map<List<FmsAccountDTO>>(Accounts));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> FmsGetAccountById(int id)
        {
            try
            {
                var Account = await _unitOfWork.FmsAccount.GetByIdAsync(id);
                return Ok(_mapper.Map<FmsAccountDTO>(Account));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> FmsAddAccount([FromBody] AddFmsAccountDTO Account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _unitOfWork.FmsAccount.InsertAsync(_mapper.Map<TbFmsAccount>(Account));
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }





        [HttpPut("{id}")]
        public async Task<IActionResult> FmsUpdateAccount(int id, [FromBody] AddFmsAccountDTO Account)
        {
            if (!ModelState.IsValid || id < 1)
            {
                return BadRequest(ModelState);
            }


            try
            {
                var AccountToUpdate = await _unitOfWork.FmsAccount.GetByIdAsync(id);

                if (AccountToUpdate == null)
                    return BadRequest("submitted Account ID is invalid.");


                _mapper.Map(Account, AccountToUpdate);

                _unitOfWork.FmsAccount.Update(AccountToUpdate);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }


        [HttpDelete]
        public async Task<IActionResult> FmsDeleteAccount(int id)
        {
            if (id < 1)
            {
                return BadRequest("Id cannot be 0 or less.");
            }

            try
            {
                var FmsAccountToDelete = await _unitOfWork.FmsAccount.GetByIdAsync(id);

                if (FmsAccountToDelete == null)
                    return BadRequest("Invalid Id is submitted.");

                _unitOfWork.FmsAccount.Delete(FmsAccountToDelete);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error. " + ex.Message);
            }
        }





    }
}
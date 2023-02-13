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
        public async Task<IActionResult> GetAllAccounts()
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
        public async Task<IActionResult> GetAccountById(int id)
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
        public async Task<IActionResult> AddNewAccount([FromBody] AddFmsAccountDTO Account)
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








    }
}

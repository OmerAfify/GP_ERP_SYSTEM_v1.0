using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domains.Interfaces.IUnitOfWork;
using ERP_Domians.Models;
using GP_ERP_SYSTEM_v1._0.DTOs;


namespace GP_ERP_SYSTEM_v1._0.Controllers
{
    [Route("api/[action]")]
    [ApiController]

    //  [Authorize(Roles = "Admin,FM")]
    public class FmsAccountsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
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
                var accCatObjs = await _unitOfWork.FmsAccCat.FindRangeAsync(o => o.AccId == id);
                var categories = await _unitOfWork.FmsCategory.GetAllAsync();
                List<string> accCatStr = (from p in accCatObjs join e in categories on p.CatId equals e.CatId
                                          select e.CatName).ToList();
                ViewFmsAccountDTO result = new ViewFmsAccountDTO
                {
                    AccBalance = Account.AccBalance,
                    AccCategories = accCatStr,
                    AccCredit = Account.AccCredit,
                    AccDebit = Account.AccDebit,
                    AccId = Account.AccId,
                    AccName = Account.AccName,
                    IncreaseMode = Account.IncreaseMode == 0 ? "Debit" : "Credit"
                };
                return Ok(result);
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

                TbFmsAccount tbFmsAccount = _mapper.Map<TbFmsAccount>(Account);
                _unitOfWork.FmsAccount.InsertAsync(tbFmsAccount);
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
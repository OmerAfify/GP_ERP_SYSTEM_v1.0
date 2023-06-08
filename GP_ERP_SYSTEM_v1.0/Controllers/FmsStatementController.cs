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
    public class FmsStatementController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public FmsStatementController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> FmsGetAllStatements()
        {
            try
            {
                var Statements = await _unitOfWork.FmsStatement.GetAllAsync();
                return Ok(_mapper.Map<List<FmsStatementDTO>>(Statements));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> FmsGetStatementById(int id)
        {
            try
            {
                var Statement = await _unitOfWork.FmsStatement.GetByIdAsync(id);
                var Accounts = await _unitOfWork.FmsStatementAccount.FindRangeAsync(p => p.StaId == id);
                ViewFmsStatementDTO result = new ViewFmsStatementDTO()
                {
                    StaId = id,
                    StaDate = Statement.StaDate,
                    StaName = Statement.StaName,
                    accounts = _mapper.Map<List<FmsStatementAccountDTO>>(Accounts),
                    StaBalance = Statement.StaBalance,

                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> FmsAddStatement(int templateID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                //create statement

                var template = await _unitOfWork.FmsStatementTemplate.GetByIdAsync(templateID);
                var statement = new TbFmsStatement

                {
                    StaDate = System.DateTime.Now,
                    StaName = template.TempName
                };

                //save statement to db, get statement id

                _unitOfWork.FmsStatement.InsertAsync(statement);
                await _unitOfWork.Save();
                TbFmsStatement saved_statement = await _unitOfWork.FmsStatement.FindAsync(o => o.StaDate == statement.StaDate);

                //add accounts to statement and update statement balance

                var accounts = await _unitOfWork.FmsTemplateAccount.FindRangeAsync(o => o.TempId == templateID);
                decimal? statementBalance = 0;

                foreach (var account in accounts)
                {
                    var fullAccount = await _unitOfWork.FmsAccount.GetByIdAsync(account.AccId);
                    statementBalance += fullAccount.AccBalance;
                    var statementAccount = new TbFmsStatementAccount
                    {
                        AccBalance = fullAccount.AccBalance,
                        AccName = fullAccount.AccName,
                        StaId = statement.StaId
                    };
                    _unitOfWork.FmsStatementAccount.InsertAsync(statementAccount);
                }

                statement.StaBalance = statementBalance;
                _unitOfWork.FmsStatement.Update(statement);

                //save
      
                await _unitOfWork.Save();
                return Ok();
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }


        [HttpPut("{id}")]
        public async Task<IActionResult> FmsUpdateStatement(int id, [FromBody] AddFmsStatementDTO Statement)
        {
            if (!ModelState.IsValid || id < 1) { return BadRequest(ModelState); }
            try
            {
                var StatementToUpdate = await _unitOfWork.FmsStatement.GetByIdAsync(id);
                if (StatementToUpdate == null) { return BadRequest("Submitted ID is Invalid."); }
                _mapper.Map(Statement, StatementToUpdate);
                _unitOfWork.FmsStatement.Update(StatementToUpdate);
                await _unitOfWork.Save();
                return NoContent();
            }

            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }

        [HttpDelete]
        public async Task<IActionResult> FmsDeleteStatement(int id)
        {
            if (id < 1)
            {
                return BadRequest("Id cannot be 0 or less.");
            }

            try
            {
                var FmsStatementToDelete = await _unitOfWork.FmsStatement.GetByIdAsync(id);
                var FmsStatementAccsToDelete = (await _unitOfWork.FmsStatementAccount.FindRangeAsync(p => p.StaId == id))
                    .ToList();

                if (FmsStatementToDelete == null)
                    return BadRequest("Invalid Id is submitted.");

                _unitOfWork.FmsStatementAccount.DeleteRange(FmsStatementAccsToDelete);
                _unitOfWork.FmsStatement.Delete(FmsStatementToDelete);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error. " + ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> FmsAddStatementAccount(int staID, string accName)
        {
            var StatementAccount = new FmsStatementAccountDTO { StaId = staID, AccName = accName };
            try
            {
                _unitOfWork.FmsStatementAccount.InsertAsync(_mapper.Map<TbFmsStatementAccount>(StatementAccount));
                await _unitOfWork.Save();
                return Ok();
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet]
        public async Task<IActionResult> FmsGetStatementAccounts(int staID)
        {
            try
            {
                var accounts = await _unitOfWork.FmsStatementAccount.FindRangeAsync(o => o.StaId == staID);
                return Ok(_mapper.Map<List<TbFmsStatementAccount>>(accounts));
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }


        [HttpDelete]
        public async Task<IActionResult> FmsDeleteStatementAccount(int staid)
        {
            if (staid < 1)
            {
                return BadRequest("Id cannot be 0 or less.");
            }

            try
            {
                var FmsStatementAccsToDelete = await _unitOfWork.FmsStatementAccount.FindRangeAsync(o => o.StaId == staid);

                if (FmsStatementAccsToDelete == null)
                    return BadRequest("Invalid Id is submitted.");

                foreach (var acc in FmsStatementAccsToDelete)
                {
                    _unitOfWork.FmsStatementAccount.Delete(acc);
                    await _unitOfWork.Save();
                }



                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error. " + ex.Message);
            }
        }


        //public  async Task<TbFmsStatement> CreateStatement(int tempID)
        //{
        //    var template = await _unitOfWork.FmsStatementTemplate.GetByIdAsync(tempID);
        //    var statement = new TbFmsStatement
        //    {
        //        StaDate = System.DateTime.Now,
        //        StaName = template.TempName
        //    };
        //    return statement;
        //}

        //public async Task<TbFmsStatement> SaveStatementDB(TbFmsStatement statement)
        //{
        //    _unitOfWork.FmsStatement.InsertAsync(statement);
        //    await _unitOfWork.Save();
        //    TbFmsStatement saved_statement = await _unitOfWork.FmsStatement.FindAsync(o => o.StaDate == statement.StaDate);
        //    return saved_statement;

        //}

        //public async void AddAccountsToStatement (int tempID, TbFmsStatement statement)

        //{
        //    var accounts = await _unitOfWork.FmsTemplateAccount.FindRangeAsync(o => o.TempId == tempID);
        //    decimal? statementBalance = 0;

        //    foreach (var account in accounts)
        //    {
        //        var fullAccount = await _unitOfWork.FmsAccount.GetByIdAsync(account.AccId);
        //        statementBalance += fullAccount.AccBalance;
        //        var statementAccount = new TbFmsStatementAccount
        //        {
        //            AccBalance = fullAccount.AccBalance,
        //            AccName = fullAccount.AccName,
        //            StaId = statement.StaId
        //        };
        //        _unitOfWork.FmsStatementAccount.InsertAsync(statementAccount);
        //    }

        //    statement.StaBalance = statementBalance;
        //    _unitOfWork.FmsStatement.Update(statement);
        //}









    }


}

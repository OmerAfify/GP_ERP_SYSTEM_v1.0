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
                return Ok(_mapper.Map<FmsStatementDTO>(Statement));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> FmsAddStatement([FromBody] AddFmsStatementDTO Statement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _unitOfWork.FmsStatement.InsertAsync(_mapper.Map<TbFmsStatement>(Statement));
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

                if (FmsStatementToDelete == null)
                    return BadRequest("Invalid Id is submitted.");

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
                var FmsStatementAccToDelete = await _unitOfWork.FmsStatementAccount.FindAsync(o => o.StaId == staid);

                if (FmsStatementAccToDelete == null)
                    return BadRequest("Invalid Id is submitted.");

                _unitOfWork.FmsStatementAccount.Delete(FmsStatementAccToDelete);
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

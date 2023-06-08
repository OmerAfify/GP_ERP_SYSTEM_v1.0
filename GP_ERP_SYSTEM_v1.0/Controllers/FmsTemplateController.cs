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
    public class FmsStatementTemplateController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public FmsStatementTemplateController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> FmsGetAllTemplates()
        {
            try
            {
                var Templates = await _unitOfWork.FmsStatementTemplate.GetAllAsync();
                List<ViewFmsTemplateListDTO> TemplatesDTO = new List<ViewFmsTemplateListDTO>();
                foreach (var Template in Templates)
                {
                    IEnumerable<TbFmsTemplateAccount> TemplateAccounts = await _unitOfWork.FmsTemplateAccount.FindRangeAsync(o => o.TempId == Template.TempId);
                    List<int> TemplateAccountIds = new List<int>();
                    foreach (TbFmsTemplateAccount TemplateAccount in TemplateAccounts)
                    {
                        TemplateAccountIds.Add(TemplateAccount.AccId);
                    }
                    ViewFmsTemplateListDTO viewFmsTemplateDTO = new ViewFmsTemplateListDTO()
                    {
                        Accounts = TemplateAccountIds,
                        TempId = Template.TempId,
                        TempDate = Template.TempDate,
                        TempName = Template.TempName,
                    };
                    TemplatesDTO.Add(viewFmsTemplateDTO);

                }
                return Ok(TemplatesDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> FmsGetTemplateById(int id)
        {
            try
            {
                var Template = await _unitOfWork.FmsStatementTemplate.GetByIdAsync(id);
                var tempAccounts = (await _unitOfWork.FmsTemplateAccount.FindRangeAsync(o => o.TempId == id)).
                    Select(p => p.AccId).ToList();
                List<FmsAccountDTO> accounts = new List<FmsAccountDTO>();
                foreach(var accountId in tempAccounts)
                {
                    var account = _mapper.Map<FmsAccountDTO>(await _unitOfWork.FmsAccount.GetByIdAsync(accountId));
                    accounts.Add(account);
                }

                ViewFmsTemplateDTO tempWithAccounts = new ViewFmsTemplateDTO()
                {
                    TempId = id,
                    TempName = Template.TempName,
                    TempDate = Template.TempDate,
                    Accounts = accounts
                };
                return Ok(tempWithAccounts);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> FmsAddTemplate([FromBody] AddFmsTemplateDTO Template)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _unitOfWork.FmsStatementTemplate.InsertAsync(_mapper.Map<TbFmsStatementTemplate>(Template));
                await _unitOfWork.Save();
                TbFmsStatementTemplate savedTemplate = await 
                    _unitOfWork.FmsStatementTemplate.FindAsync(o => o.TempDate == Template.TempDate);
                List<TbFmsTemplateAccount> tempAccounts = new List<TbFmsTemplateAccount>();
                foreach (var account in Template.Accounts)
                {

                    tempAccounts.Add(new TbFmsTemplateAccount { AccId = account.AccId, TempId = savedTemplate.TempId });
                } 
                _unitOfWork.FmsTemplateAccount.InsertRangeAsync(tempAccounts);
                  
                await _unitOfWork.Save();
                return Ok();
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
       



        [HttpPut("{id}")]
        public async Task<IActionResult> FmsUpdateTemplate(int id, [FromBody] AddFmsTemplateDTO Template)
        {
            if (!ModelState.IsValid || id < 1) { return BadRequest(ModelState); }
            try
            {
                var TemplateToUpdate = await _unitOfWork.FmsStatementTemplate.GetByIdAsync(id);
                if (TemplateToUpdate == null) { return BadRequest("Submitted ID is Invalid."); }
                _mapper.Map(Template, TemplateToUpdate);
                _unitOfWork.FmsStatementTemplate.Update(TemplateToUpdate);
                await _unitOfWork.Save();
                return NoContent();
            }

            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }

        [HttpDelete]
        public async Task<IActionResult> FmsDeleteTemplate(int id)
        {
            if (id < 1)
            {
                return BadRequest("Id cannot be 0 or less.");
            }

            try
            {
                var FmsStatementTemplateToDelete = await _unitOfWork.FmsStatementTemplate.GetByIdAsync(id);
                var FmsTempAccsToDelete = (await _unitOfWork.FmsTemplateAccount.FindRangeAsync(p => p.TempId == id))
                    .ToList();

                if (FmsStatementTemplateToDelete == null)
                    return BadRequest("Invalid Id is submitted.");

                _unitOfWork.FmsTemplateAccount.DeleteRange(FmsTempAccsToDelete);
                _unitOfWork.FmsStatementTemplate.Delete(FmsStatementTemplateToDelete);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error. " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> FmsAddTemplateAccount(int tempID, int accID)
        {
            var TemplateAccount = new FmsTemplateAccountDTO { TempId = tempID, AccId = accID };
            try
            {
                _unitOfWork.FmsTemplateAccount.InsertAsync(_mapper.Map<TbFmsTemplateAccount>(TemplateAccount));
                await _unitOfWork.Save();
                return Ok();
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet]
        public async Task<IActionResult> FmsGetTemplateAccounts(int tempID)
        {
            try
            {
                var accounts = await _unitOfWork.FmsTemplateAccount.FindRangeAsync(o => o.TempId == tempID);
                return Ok(_mapper.Map<List<TbFmsTemplateAccount>>(accounts));
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }


        [HttpDelete]
        public async Task<IActionResult> FmsDeleteTemplateAccount(int tempid)
        {
            if (tempid < 1)
            {
                return BadRequest("Id cannot be 0 or less.");
            }

            try
            {
                var FmsTemplateAccToDelete = await _unitOfWork.FmsTemplateAccount.FindAsync(o => o.TempId == tempid);

                if (FmsTemplateAccToDelete == null)
                    return BadRequest("Invalid Id is submitted.");

                _unitOfWork.FmsTemplateAccount.Delete(FmsTemplateAccToDelete);
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

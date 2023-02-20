﻿using Microsoft.AspNetCore.Mvc;
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
                return Ok(_mapper.Map<List<FmsStatementTemplateDTO>>(Templates));
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
                return Ok(_mapper.Map<FmsStatementTemplateDTO>(Template));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> FmsAddTemplate([FromBody] AddFmsStatementTemplateDTO Template)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _unitOfWork.FmsStatementTemplate.InsertAsync(_mapper.Map<TbFmsStatementTemplate>(Template));
                await _unitOfWork.Save();
                return Ok();
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

       


        [HttpPut("{id}")]
        public async Task<IActionResult> FmsUpdateTemplate(int id, [FromBody] AddFmsStatementTemplateDTO Template)
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

                if (FmsStatementTemplateToDelete == null)
                    return BadRequest("Invalid Id is submitted.");

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

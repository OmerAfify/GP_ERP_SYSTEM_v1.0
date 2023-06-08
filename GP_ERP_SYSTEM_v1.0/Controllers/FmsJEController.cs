using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domains.Interfaces.IUnitOfWork;
using ERP_Domians.Models;
using GP_ERP_SYSTEM_v1._0.DTOs;
using Microsoft.AspNetCore.Mvc;


namespace GP_ERP_SYSTEM_v1._0.Controllers
{
    [Route("api/[action]")]

    [ApiController]


    //  [Authorize(Roles = "Admin,FM")]
    public class FmsJEController : ControllerBase
    {

        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public FmsJEController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllJournals()
        {
            try
            {
                var Journals = await _unitOfWork.FmsJournalEntry.GetAllAsync();
                return Ok(_mapper.Map<List<FmsJeDTO>>(Journals));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetFmsJournalEntryById(int id)
        {
            try
            {
                var FmsJournalEntry = await _unitOfWork.FmsJournalEntry.GetByIdAsync(id);
                return Ok(_mapper.Map<FmsJeDTO>(FmsJournalEntry));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddNewFmsJournalEntry([FromBody] AddFmsJeDTO FmsJournalEntry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                //retrieve affected accounts
                TbFmsAccount debitAccount = await _unitOfWork.FmsAccount.GetByIdAsync(FmsJournalEntry.Jeaccount1);
                TbFmsAccount creditAccount = await _unitOfWork.FmsAccount.GetByIdAsync(FmsJournalEntry.Jeaccount2);
                
                //apply changes

                //debitAccount 
                debitAccount.AccDebit += FmsJournalEntry.Jedebit;
                if (debitAccount.IncreaseMode == 0)
                {
                    debitAccount.AccBalance += FmsJournalEntry.Jedebit;
                }
                else
                {
                    debitAccount.AccBalance -= FmsJournalEntry.Jedebit;
                }

                //credit account

                creditAccount.AccCredit += FmsJournalEntry.Jecredit;
                if (creditAccount.IncreaseMode == 1)
                {
                    creditAccount.AccBalance += FmsJournalEntry.Jecredit;
                }
                else
                {
                    creditAccount.AccBalance -= FmsJournalEntry.Jecredit;
                }
                _unitOfWork.FmsAccount.Update(creditAccount);
                _unitOfWork.FmsAccount.Update(debitAccount);
                _unitOfWork.FmsJournalEntry.InsertAsync(_mapper.Map<TbFmsJournalEntry>(FmsJournalEntry));
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFmsJournalEntry(int id, [FromBody] AddFmsJeDTO FmsJournalEntry)
        {
            if (!ModelState.IsValid || id < 1)
            {
                return BadRequest(ModelState);
            }


            try
            {
                var FmsJournalEntryToUpdate = await _unitOfWork.FmsJournalEntry.GetByIdAsync(id);

                if (FmsJournalEntryToUpdate == null)
                    return BadRequest("submitted FmsJournalEntryId is invalid.");


                _mapper.Map(FmsJournalEntry, FmsJournalEntryToUpdate);

                _unitOfWork.FmsJournalEntry.Update(FmsJournalEntryToUpdate);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteFmsJournalEntry(int id)
        {
            if (id < 1)
            {
                return BadRequest("Id cannot be 0 or less.");
            }

            try
            {
                var FmsJournalEntryToDelete = await _unitOfWork.FmsJournalEntry.GetByIdAsync(id);

                if (FmsJournalEntryToDelete == null)
                    return BadRequest("Invalid Id is submitted.");

                _unitOfWork.FmsJournalEntry.Delete(FmsJournalEntryToDelete);
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

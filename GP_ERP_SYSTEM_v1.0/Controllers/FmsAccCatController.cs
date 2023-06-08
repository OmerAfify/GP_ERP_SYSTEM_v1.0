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
    public class FmsAccCatController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public FmsAccCatController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> FmsGetAllAccCats()
        {
            try
            {
                var AccCats = await _unitOfWork.FmsAccCat.GetAllAsync();
                return Ok(_mapper.Map<List<FmsAccCatDTO>>(AccCats));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> FmsAddAccCat(int accID, int catID)
        {
            var AccCat = new FmsAccCatDTO { AccId = accID,  CatId = catID};
            try
            {
                _unitOfWork.FmsAccCat.InsertAsync(_mapper.Map<TbFmsAccCat>(AccCat));
                await _unitOfWork.Save();
                return Ok();
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }


        [HttpDelete]
        public async Task<IActionResult> FmsDeleteAccCat(int accID, int catID)
        {
            if (catID < 1 || accID < 1)
            {
                return BadRequest("Id cannot be 0 or less.");
            }

            try
            {
                var FmsAccCatToDelete = await _unitOfWork.FmsAccCat.FindAsync(o => o.AccId == accID && o.CatId == catID);

                if (FmsAccCatToDelete == null)
                    return BadRequest("Invalid Id is submitted.");

                _unitOfWork.FmsAccCat.Delete(FmsAccCatToDelete);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error. " + ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> FmsGetCategoryAccounts(int catID)
        {
            if (catID < 1)
            {
                return BadRequest("Id cannot be 0 or less.");
            }

            try
            {
                var AccCats = await _unitOfWork.FmsAccCat.FindRangeAsync(o => o.CatId==catID);
                return Ok(_mapper.Map<List<FmsAccCatDTO>>(AccCats));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error. " + ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> FmsGetAccountCategories(int accID)
        {
            if (accID < 1)
            {
                return BadRequest("Id cannot be 0 or less.");
            }

            try
            {
                var AccCats = await _unitOfWork.FmsAccCat.FindRangeAsync(o => o.AccId == accID);
                return Ok(_mapper.Map<List<FmsAccCatDTO>>(AccCats));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error. " + ex.Message);
            }
        }










    }


}

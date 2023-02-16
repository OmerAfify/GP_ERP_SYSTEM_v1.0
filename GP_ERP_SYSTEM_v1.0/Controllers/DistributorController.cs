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
    public class DistributorController : ControllerBase
    {

        private readonly  IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DistributorController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllDistributors()
        {
            try
            {
               var distributors = await _unitOfWork.Distributor.GetAllAsync();
               return Ok(_mapper.Map<List<DistributorDTO>>(distributors));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetDirstributorById(int id)
        {
            try
            {
                var distributor = await _unitOfWork.Distributor.GetByIdAsync(id);
                 return Ok(_mapper.Map<DistributorDTO>(distributor));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddNewDistributor([FromBody] AddDistributorDTO distributorDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
         
            try
            {
                _unitOfWork.Distributor.InsertAsync(_mapper.Map<TbDistributor>(distributorDTO));
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDistributor(int id, [FromBody] AddDistributorDTO UpdateDistributor)
        {
            if (!ModelState.IsValid || id < 1)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var distributorToUpdate = await _unitOfWork.Distributor.GetByIdAsync(id);

                if (distributorToUpdate == null)
                    return BadRequest("submitted distributorId is invalid.");


                 _mapper.Map(UpdateDistributor, distributorToUpdate);

                _unitOfWork.Distributor.Update(distributorToUpdate);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteDistributor(int id)
        {
            if (id < 1)
            {
                return BadRequest("Id cannot be 0 or less.");
            }

            try
            {
                var distributorToDelete = await _unitOfWork.Distributor.GetByIdAsync(id);

                if (distributorToDelete == null)
                    return BadRequest("Invalid Id is submitted.");

                _unitOfWork.Distributor.Delete(distributorToDelete);
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

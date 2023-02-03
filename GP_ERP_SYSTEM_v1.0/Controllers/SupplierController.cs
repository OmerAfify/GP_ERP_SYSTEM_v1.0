using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domains.Interfaces.IUnitOfWork;
using ERP_BusinessLogic.Models;
using GP_ERP_SYSTEM_v1._0.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GP_ERP_SYSTEM_v1._0.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public SupplierController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllSuppliers()
        {
            try
            {
                var Suppliers = await _unitOfWork.Supplier.GetAllAsync();
                return Ok(_mapper.Map<List<SupplierDTO>>(Suppliers));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupplierById(int id)
        {
            try
            {
                var Supplier = await _unitOfWork.Supplier.GetByIdAsync(id);
                return Ok(_mapper.Map<SupplierDTO>(Supplier));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddNewSupplier([FromBody] AddSupplierDTO Supplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _unitOfWork.Supplier.InsertAsync(_mapper.Map<TbSupplier>(Supplier));
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplier(int id, [FromBody] AddSupplierDTO Supplier)
        {
            if (!ModelState.IsValid || id < 1)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var SupplierToUpdate = await _unitOfWork.Supplier.GetByIdAsync(id);

                if (SupplierToUpdate == null)
                    return BadRequest("submitted SupplierId is invalid.");


                _mapper.Map(Supplier, SupplierToUpdate);

                _unitOfWork.Supplier.Update(SupplierToUpdate);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            if (id < 1)
            {
                return BadRequest("Id cannot be 0 or less.");
            }

            try
            {
                var SupplierToDelete = await _unitOfWork.Supplier.GetByIdAsync(id);

                if (SupplierToDelete == null)
                    return BadRequest("Invalid Id is submitted.");

                _unitOfWork.Supplier.Delete(SupplierToDelete);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error. " + ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddSupplingMaterialToSupplier(int supplierId, [FromBody] List<SupplyingMaterialDetailsDTO> supplingMaterialDetailsDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var supplyingMaterials = _mapper.Map<List<TbSupplyingMaterialDetail>>(supplingMaterialDetailsDTO);

                supplyingMaterials.ForEach(s => s.SupplierId = supplierId);

               _unitOfWork.SupplingMaterialDetails.InsertRangeAsync(supplyingMaterials);
               
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }

        }


        [HttpGet("{supplierId}")]  
            public async Task<IActionResult> GetSuppliersMaterials(int supplierId)
        {
            try
            {
                var supplier = await _unitOfWork.Supplier.GetByIdAsync(supplierId);

                if (supplier==null)
                    return BadRequest("supplier Id is not found");

                var supplyingMaterials = await _unitOfWork.SupplingMaterialDetails.FindRangeAsync(m => m.SupplierId == supplierId);
                return Ok(_mapper.Map<List<SupplyingMaterialDetailsDTO>>(supplyingMaterials) );


            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }

         }

        // AddSupplingMaterialToSupplier(supplierId, List<supplingMaterialDetaisl>) 
        // UpdateSupplingMaterialToSupplier(supplierId, List<supplingMaterialDetaisl>)
        // DeleteSupplingMaterialToSupplier(supplierId, List<supplingMaterialDetaisl>)



    }
}

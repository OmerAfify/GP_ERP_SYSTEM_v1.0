using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domains.Interfaces.IUnitOfWork;
using ERP_Domians.Models;
using GP_ERP_SYSTEM_v1._0.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GP_ERP_SYSTEM_v1._0.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

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




        [HttpGet("{supplierId}")]
        public async Task<IActionResult> GetSuppliersMaterials(int supplierId)
        {
            try
            {
                var supplier = await _unitOfWork.Supplier.GetByIdAsync(supplierId);

                if (supplier == null)
                    return BadRequest("supplier Id is not found");

                var supplyingMaterials = await _unitOfWork.SupplingMaterialDetails.FindRangeAsync(m => m.SupplierId == supplierId);
                return Ok(_mapper.Map<List<SupplyingMaterialDetailDTO>>(supplyingMaterials));


            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error." + ex.Message);
            }

        }


        [HttpPost]
        public async Task<IActionResult> AddNewSupplyingMaterialToSupplier(int supplierId,[FromBody] List<SupplyingMaterialDetailDTO> supplyingMaterialDetailsDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList());
          
            try
            {
                var supplier = await _unitOfWork.Supplier.GetByIdAsync(supplierId);

                if (supplier == null)
                    return BadRequest("Supplier Id is not found");

                var SendedRawMaterialsIdsList = supplyingMaterialDetailsDTO.Select(rm => rm.MaterialId).ToList();

                var StoredRawMaterialsIdsList = (await _unitOfWork.RawMaterial.GetAllAsync()).Select(i => i.MaterialId).ToList();

                var InvalidRawMaterialsIdsList = SendedRawMaterialsIdsList.
                          Where(sended => !StoredRawMaterialsIdsList.Any(stored => stored==sended )).ToList();

                if (InvalidRawMaterialsIdsList.Count > 0)
                {
                    return BadRequest("The following Materials ids are invalid: " + String.Join(", ", InvalidRawMaterialsIdsList)+" ." );
    
                }


                var supplyingMaterialsDetails = _mapper.Map< List<TbSupplyingMaterialDetail> >(supplyingMaterialDetailsDTO);

                supplyingMaterialsDetails.ForEach(s => s.SupplierId = supplierId);

                 _unitOfWork.SupplingMaterialDetails.InsertRangeAsync(supplyingMaterialsDetails);

                await _unitOfWork.Save();

                return NoContent();

            }catch(Exception ex)
            {
                return Problem("An error occured while processing the request. " + ex.Message);
            }


        }


        [HttpPut]
        public async Task<IActionResult>  UpdateSupplingMaterialToSupplier(int supplierId, [FromBody] List<SupplyingMaterialDetailDTO> supplyingMaterialDetailsDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList());

            try
            {
                var supplier = await _unitOfWork.Supplier.GetByIdAsync(supplierId);

                if (supplier == null)
                    return BadRequest("Supplier Id is not found");

  
                var SendedRawMaterialsIdsList = supplyingMaterialDetailsDTO.Select(rm => rm.MaterialId).ToList();

                var StoredRawMaterialsIdsList = (await _unitOfWork.RawMaterial.GetAllAsync()).Select(i => i.MaterialId).ToList();

                var InvalidRawMaterialsIdsList = GetInvalidRawMaterialsIdsSend(SendedRawMaterialsIdsList, StoredRawMaterialsIdsList);
              
                if (InvalidRawMaterialsIdsList.Count > 0)
                {
                  return BadRequest("The following Materials ids are invalid: " + String.Join(", ", InvalidRawMaterialsIdsList) + " .");

                }



                var supplyingMaterialsDetailsToUpdate = await (_unitOfWork.SupplingMaterialDetails.FindRangeAsync(s=>s.SupplierId==supplierId && supplyingMaterialDetailsDTO.Select(m=>m.MaterialId).Any(m=>m==s.MaterialId) ));


                if (supplyingMaterialDetailsDTO.Count != supplyingMaterialsDetailsToUpdate.ToList().Count)
                    return BadRequest("The entered Material Ids dosn't exist in the current suppleir Id number : "+supplierId);


                foreach (var newMaterial in supplyingMaterialDetailsDTO)
                {
                    var itemToChange = supplyingMaterialsDetailsToUpdate.FirstOrDefault(d => d.MaterialId== newMaterial.MaterialId);
                    if (itemToChange != null) {
                        _mapper.Map(newMaterial,itemToChange);
                         _unitOfWork.SupplingMaterialDetails.Update(itemToChange);
                    }
                }

                await  _unitOfWork.Save();
  

               return NoContent();

            }
            catch (Exception ex)
            {
                return Problem("An error occured while processing the request. " + ex.Message);
            }


        }
        
        
        [HttpDelete]
        public async Task<IActionResult>  DeleteSupplingMaterialToSupplier(int supplierId, [FromBody] List<int> supplyingMaterialsIds)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList());

            try
            {
                var supplier = await _unitOfWork.Supplier.GetByIdAsync(supplierId);

                if (supplier == null)
                    return BadRequest("Supplier Id is not found");

  

                var StoredRawMaterialsIdsList = (await _unitOfWork.RawMaterial.GetAllAsync()).Select(i => i.MaterialId).ToList();

                var InvalidRawMaterialsIdsList = GetInvalidRawMaterialsIdsSend(supplyingMaterialsIds, StoredRawMaterialsIdsList);
              
                if (InvalidRawMaterialsIdsList.Count > 0)
                {
                  return BadRequest("The following Materials ids are invalid: " + String.Join(", ", InvalidRawMaterialsIdsList) + " .");

                }


                var supplyingMaterialsDetailsToDelete = await (_unitOfWork.SupplingMaterialDetails.FindRangeAsync(s=>s.SupplierId==supplierId && supplyingMaterialsIds.Any(m=>m==s.MaterialId) ));


                if (supplyingMaterialsIds.Count != supplyingMaterialsDetailsToDelete.ToList().Count)
                    return BadRequest("The entered Material Ids dosn't exist in the current suppleir Id number : "+supplierId);


                 _unitOfWork.SupplingMaterialDetails.DeleteRange(supplyingMaterialsDetailsToDelete.ToList());
                await  _unitOfWork.Save();
  

               return NoContent();

            }
            catch (Exception ex)
            {
                return Problem("An error occured while processing the request. " + ex.Message);
            }


        }






        //helper Methods
        private static List<int> GetInvalidRawMaterialsIdsSend(List<int> SendedRawMaterialsIdsList, List<int> StoredRawMaterialsIdsList)
        {
        
            var InvalidRawMaterialsIdsList = SendedRawMaterialsIdsList.
                      Where(sended => !StoredRawMaterialsIdsList.Any(stored => stored == sended)).ToList();

            return InvalidRawMaterialsIdsList; 

            

        }


        


    }
}

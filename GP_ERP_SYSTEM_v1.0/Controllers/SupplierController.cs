using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domains.Interfaces.IUnitOfWork;
using ERP_Domians.Models;
using GP_ERP_SYSTEM_v1._0.DTOs;
using GP_ERP_SYSTEM_v1._0.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GP_ERP_SYSTEM_v1._0.Controllers
{
    [Route("api/[action]")]
    [ApiController]
   // [Authorize(Roles = "Admin,SCM")]
    public class SupplierController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        //used only in the API call of AddNewSupplyingMaterialToSupplier
        public class SupplierMaterialDetails {
            public List<SupplyingMaterialDetailDTO> SupplyingMaterialDetails { get; set; }      
        }
        public class SupplierMaterialId
        {
            public int SupplyierMaterialId { get; set; }
        }


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
                return StatusCode(500, new ErrorExceptionResponse(500, null, ex.Message));
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupplierById(int id)
        {

            if (id <= 0)
                return BadRequest(new ErrorValidationResponse() { Errors = new List<string> { "Id can't be 0 or less." } });

            try
            {
                var Supplier = await _unitOfWork.Supplier.GetByIdAsync(id);

                if (Supplier == null)
                    return NotFound(new ErrorApiResponse(404, "Supplier is not found"));

                return Ok(_mapper.Map<SupplierDTO>(Supplier));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorExceptionResponse(500, null, ex.Message));
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddNewSupplier([FromBody] AddSupplierDTO Supplier)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);



            try
            {
                _unitOfWork.Supplier.InsertAsync(_mapper.Map<TbSupplier>(Supplier));
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorExceptionResponse(500, null, ex.Message));
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplier(int id, [FromBody] AddSupplierDTO Supplier)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id <= 0)
                return BadRequest(new ErrorValidationResponse() { Errors = new List<string> { "Id can't be 0 or less." } });

            try
            {
                var SupplierToUpdate = await _unitOfWork.Supplier.GetByIdAsync(id);

                if (SupplierToUpdate == null)
                    return NotFound(new ErrorApiResponse(404, "Supplier Id is not found."));


                _mapper.Map(Supplier, SupplierToUpdate);

                _unitOfWork.Supplier.Update(SupplierToUpdate);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorExceptionResponse(500, null, ex.Message));
            }
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id <= 0)
                return BadRequest(new ErrorValidationResponse() { Errors = new List<string> { "Id can't be 0 or less." } });

            try
            {
                var SupplierToDelete = await _unitOfWork.Supplier.GetByIdAsync(id);

                if (SupplierToDelete == null)
                    return BadRequest(new ErrorApiResponse(400, "Supplier not found."));

                _unitOfWork.Supplier.Delete(SupplierToDelete);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorExceptionResponse(500, null, ex.Message));
            }
        }




        [HttpGet("{supplierId}")]
        public async Task<IActionResult> GetSuppliersMaterials(int supplierId)
        {
            try
            {
                var supplier = await _unitOfWork.Supplier.GetByIdAsync(supplierId);

                if (supplier == null)
                    return BadRequest(new ErrorApiResponse(400, "supplier Id is not found"));

                var supplyingMaterials = await _unitOfWork.SupplingMaterialDetails.FindRangeAsync(m => m.SupplierId == supplierId, new List<string>() { "Material" });
                return Ok(_mapper.Map<List<ReturnedSupplyingMaterialDetailDTO>>(supplyingMaterials));


            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorExceptionResponse(500, null, ex.Message));
            }

        }


        [HttpPost]
        public async Task<IActionResult> AddNewSupplyingMaterialToSupplier(int supplierId, [FromBody] SupplierMaterialDetails supplyingMaterialDetailsDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (supplierId <= 0)
                return BadRequest(new ErrorValidationResponse() { Errors = new List<string> { "Id can't be 0 or less." } });

            try
            {
                var supplier = await _unitOfWork.Supplier.GetByIdAsync(supplierId);

                if (supplier == null)
                    return BadRequest(new ErrorApiResponse(400,"Supplier Id is not found"));

                var SendedRawMaterialsIdsList = supplyingMaterialDetailsDTO.SupplyingMaterialDetails.Select(rm => rm.MaterialId).ToList();

                var StoredRawMaterialsIdsList = (await _unitOfWork.RawMaterial.GetAllAsync()).Select(i => i.MaterialId).ToList();

                var InvalidRawMaterialsIdsList = SendedRawMaterialsIdsList.
                          Where(sended => !StoredRawMaterialsIdsList.Any(stored => stored==sended )).ToList();

                if (InvalidRawMaterialsIdsList.Count > 0)
                {
                    return BadRequest(new ErrorApiResponse(400,"The following Materials ids are invalid: " 
                                                              + String.Join(", ", InvalidRawMaterialsIdsList)+" ." ));
    
                }


                var supplyingMaterialsDetails = _mapper.Map< List<TbSupplyingMaterialDetail> >(supplyingMaterialDetailsDTO.SupplyingMaterialDetails);

                supplyingMaterialsDetails.ForEach(s => s.SupplierId = supplierId);

                 _unitOfWork.SupplingMaterialDetails.InsertRangeAsync(supplyingMaterialsDetails);

                await _unitOfWork.Save();

                return NoContent();

            }catch(Exception ex)
            {
                return StatusCode(500, new ErrorExceptionResponse(500, null, ex.Message));
            }


        }


        [HttpPut]
        public async Task<IActionResult>  UpdateSupplingMaterialToSupplier(int supplierId, [FromBody] List<SupplyingMaterialDetailDTO> supplyingMaterialDetailsDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (supplierId <= 0)
                return BadRequest(new ErrorValidationResponse() { Errors = new List<string> { "Id can't be 0 or less." } });

            try
            {
                var supplier = await _unitOfWork.Supplier.GetByIdAsync(supplierId);

                if (supplier == null)
                    return BadRequest(new ErrorApiResponse(400, "Supplier is not found")) ;

  
                var SendedRawMaterialsIdsList = supplyingMaterialDetailsDTO.Select(rm => rm.MaterialId).ToList();

                var StoredRawMaterialsIdsList = (await _unitOfWork.RawMaterial.GetAllAsync()).Select(i => i.MaterialId).ToList();

                var InvalidRawMaterialsIdsList = GetInvalidRawMaterialsIdsSend(SendedRawMaterialsIdsList, StoredRawMaterialsIdsList);
              
                if (InvalidRawMaterialsIdsList.Count > 0)
                {
                    return BadRequest(new ErrorApiResponse(400, "The following Materials ids are invalid: "
                                                                           + String.Join(", ", InvalidRawMaterialsIdsList) + " ."));
                }



                var supplyingMaterialsDetailsToUpdate = await (_unitOfWork.SupplingMaterialDetails.FindRangeAsync(s=>s.SupplierId==supplierId && supplyingMaterialDetailsDTO.Select(m=>m.MaterialId).Any(m=>m==s.MaterialId) ));


                if (supplyingMaterialDetailsDTO.Count != supplyingMaterialsDetailsToUpdate.ToList().Count)
                    return BadRequest(new ErrorApiResponse(400, "The entered Material Ids dosn't exist in the current suppleir Id number : "+supplierId));


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
                return StatusCode(500, new ErrorExceptionResponse(500, null, ex.Message));
            }


        }
        
        
        [HttpDelete]
        public async Task<IActionResult>  DeleteSupplingMaterialToSupplier(int supplierId, [FromBody] List<int> supplyingMaterialsIds)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (supplierId <= 0)
                return BadRequest(new ErrorValidationResponse() { Errors = new List<string> { "Id can't be 0 or less." } });

            try
            {
                var supplier = await _unitOfWork.Supplier.GetByIdAsync(supplierId);

                if (supplier == null)
                    return NotFound(new ErrorApiResponse(404,"Supplier Id is not found"));

  

                var StoredRawMaterialsIdsList = (await _unitOfWork.RawMaterial.GetAllAsync()).Select(i => i.MaterialId).ToList();

                var InvalidRawMaterialsIdsList = GetInvalidRawMaterialsIdsSend(supplyingMaterialsIds, StoredRawMaterialsIdsList);
              
                if (InvalidRawMaterialsIdsList.Count > 0)
                {
                    return BadRequest(new ErrorApiResponse(400, "The following Materials ids are invalid: "
                                                                    + String.Join(", ", InvalidRawMaterialsIdsList) + " ."));
                }


                var supplyingMaterialsDetailsToDelete = await (_unitOfWork.SupplingMaterialDetails.FindRangeAsync(s=>s.SupplierId==supplierId && supplyingMaterialsIds.Any(m=>m==s.MaterialId) ));


                if (supplyingMaterialsIds.Count != supplyingMaterialsDetailsToDelete.ToList().Count)
                    return BadRequest(new ErrorApiResponse(400,"The entered Material Ids dosn't exist in the current suppleir Id number : "+supplierId));


                 _unitOfWork.SupplingMaterialDetails.DeleteRange(supplyingMaterialsDetailsToDelete.ToList());
                await  _unitOfWork.Save();
  

               return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorExceptionResponse(500, null, ex.Message));
            }


        }



        [HttpDelete]
        public async Task<IActionResult> DeleteSupplingMaterialToSupplier_V2(int supplierId, [FromBody] SupplierMaterialId supplyingMaterialId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (supplierId <= 0)
                return BadRequest(new ErrorValidationResponse() { Errors = new List<string> { "Id can't be 0 or less." } });

            try
            {
                var supplier = await _unitOfWork.Supplier.GetByIdAsync(supplierId);

                if (supplier == null)
                    return NotFound(new ErrorApiResponse(404, "Supplier Id is not found"));

                var supplyingRawMaterial = await _unitOfWork.RawMaterial.GetByIdAsync(supplyingMaterialId.SupplyierMaterialId);

                if (supplyingRawMaterial == null)
                {
                    return BadRequest(new ErrorApiResponse(400, "The following Material id is invalid:" + supplyingMaterialId));
                }


                var supplyingMaterialsDetailsToDelete = await (_unitOfWork.SupplingMaterialDetails.FindAsync(s => s.SupplierId == supplierId && supplyingMaterialId.SupplyierMaterialId == s.MaterialId));


                if (supplyingMaterialsDetailsToDelete == null)
                    return BadRequest(new ErrorApiResponse(400, "The entered Material Ids dosn't exist in the current suppleir Id number : " + supplierId));


                _unitOfWork.SupplingMaterialDetails.Delete(supplyingMaterialsDetailsToDelete);
                await _unitOfWork.Save();


                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorExceptionResponse(500, null, ex.Message));
            }


        }



        [HttpDelete]
        public async Task<IActionResult> DeleteSupplingMaterialToSupplier_V3(int supplierId, int supplyingMaterialId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (supplierId <= 0)
                return BadRequest(new ErrorValidationResponse() { Errors = new List<string> { "Id can't be 0 or less." } });

            try
            {
                var supplier = await _unitOfWork.Supplier.GetByIdAsync(supplierId);

                if (supplier == null)
                    return NotFound(new ErrorApiResponse(404, "Supplier Id is not found"));

                var supplyingRawMaterial = await _unitOfWork.RawMaterial.GetByIdAsync(supplyingMaterialId);

                if (supplyingRawMaterial == null)
                {
                    return BadRequest(new ErrorApiResponse(400, "The following Material id is invalid:" + supplyingMaterialId));
                }


                var supplyingMaterialsDetailsToDelete = await (_unitOfWork.SupplingMaterialDetails.FindAsync(s => s.SupplierId == supplierId && supplyingMaterialId == s.MaterialId));


                if (supplyingMaterialsDetailsToDelete == null)
                    return BadRequest(new ErrorApiResponse(400, "The entered Material Ids dosn't exist in the current suppleir Id number : " + supplierId));


                _unitOfWork.SupplingMaterialDetails.Delete(supplyingMaterialsDetailsToDelete);
                await _unitOfWork.Save();


                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorExceptionResponse(500, null, ex.Message));
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

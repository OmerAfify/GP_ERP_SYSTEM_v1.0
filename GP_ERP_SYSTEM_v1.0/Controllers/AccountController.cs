using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using ERP_Domians.IServices;
using ERP_Domians.Models;
using GP_ERP_SYSTEM_v1._0.DTOs;
using GP_ERP_SYSTEM_v1._0.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeCorner.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,
                                ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper= mapper;
        }



        [HttpPost]
        public async Task<ActionResult<UserDTO>> Register([FromBody] RegisterDTO registerUserDTO)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            try
            {


                var user = new ApplicationUser()
                {

                    Email = registerUserDTO.Email,
                    UserName = registerUserDTO.Email,
                    FirstName = registerUserDTO.FirstName,
                    LastName = registerUserDTO.LasttName
                };

                var result = await _userManager.CreateAsync(user, registerUserDTO.Password);

                if (result.Succeeded)
                {
                    return new UserDTO()
                    {
                        Email = user.Email,
                        Name = user.FirstName,
                        Token = _tokenService.CreateToken(user)
                    };

                }
                else
                    return BadRequest();

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorExceptionResponse(500, null, ex.Message));

            }

        }


        [HttpPost]
        public async Task<ActionResult<UserDTO>> Login([FromBody] LoginDTO loginUserDTO)
        {

            if (!ModelState.IsValid)
                 return BadRequest();


            try
            {
                var user = await _userManager.FindByEmailAsync(loginUserDTO.Email);
            
                if (user == null)
                    return BadRequest(new ErrorValidationResponse() { Errors = new List<string> { "Email doesn't exist." } });

                var result = await _signInManager.PasswordSignInAsync(loginUserDTO.Email, loginUserDTO.Password, false, false);

                if (!result.Succeeded)
                    return BadRequest(new ErrorValidationResponse() { Errors = new List<string> { "Password doesn't match the existing email." } });

                return new UserDTO()
                {
                    Email = loginUserDTO.Email,
                    Name = user.FirstName,
                    Token = _tokenService.CreateToken(user)
                };


            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorExceptionResponse(500, null, ex.Message));
            }

        }



        //[HttpGet]
        //[Authorize]
        //public async Task<ActionResult<UserDTO>> GetCurrentUser() {


        //  try {   
        //    var email =  User.FindFirstValue(ClaimTypes.Email);
        //    var user = await _userManager.FindByEmailAsync(email);

        //    if (user == null)
        //        return BadRequest();

        //    return Ok(new UserDTO()
        //    {
        //        Email = user.Email,
        //        Name = user.FirstName,
        //        Token = _tokenService.CreateToken(user)
        //    });     
        //  } catch (Exception ex)
        //    {
        //        return StatusCode(500, new ErrorExceptionResponse(500, null, ex.Message));
        //    }
         
        //}

    
        //[HttpGet]
        //public async Task<ActionResult<bool>> CheckIfEmailExistsAsync ([FromQuery] string email)
        //{
        //    try {

        //        return await _userManager.FindByEmailAsync(email) != null;

        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new ErrorExceptionResponse(500, null, ex.Message));
        //    }

             
        //}

    }
}

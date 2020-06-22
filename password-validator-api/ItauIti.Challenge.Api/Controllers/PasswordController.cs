using ItauIti.Challenge.Api.Dtos.Requests;
using ItauIti.Challenge.PasswordValidate;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ItauIti.Challenge.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordController : ControllerBase
    {
        private readonly IPasswordValidator _passwordValidator;

        public PasswordController(IPasswordValidator passwordValidator)
        {
            _passwordValidator = passwordValidator;
        }

        [HttpPost("validate")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Validate([FromBody] PasswordValidateRequestDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (string.IsNullOrWhiteSpace(requestDto.Password))
                return BadRequest();

            return Ok(_passwordValidator.Validate(requestDto.Password));
        }
    }
}
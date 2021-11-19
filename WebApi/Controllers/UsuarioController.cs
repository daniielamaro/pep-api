using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Servicos;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        [HttpPost("VerificarToken")]
        [AllowAnonymous]
        public IActionResult VerificarToken(string token)
        {
            try
            {
                var valido = TokenService.ValidateToken(token);

                if (valido)
                    return Ok();
                else
                    return BadRequest("Token Invalido!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

using Aplicacao;
using Dominio.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.RequestModels;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClinicaController : ControllerBase
    {
        [HttpPost("CadastroClinica")]
        //[Authorize]
        public async Task<IActionResult> CadastroExame(RequestClinica clinicaRequest)
        {
            try
            {
                var newClinica = new Clinica
                {
                   Endereco = clinicaRequest.Endereco,
                   NomeClinica = clinicaRequest.NomeClinica
                };
                var ClinicaApp = new ClinicaApp();

                await ClinicaApp.CadastrarClinica(newClinica,  clinicaRequest.IdConsulta, clinicaRequest.IdTipoExame);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("ListaClinicas")]
        //[Authorize]
        public async Task<IActionResult> ConsultarLista(Guid id)
        {
            try
            {
                var clinicaApp = new ClinicaApp();

                return Ok(await clinicaApp.ListarClinicas(id));


            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

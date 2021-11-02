using Aplicacao;
using Dominio.Entities;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> CadastroExame(RequestClinica request)
        {
            try
            {
                var newClinica = new Clinica
                {
                   Endereco = new Endereco
                   {
                        Logradouro = request.Logradouro,
                        Numero = request.Numero,
                        CEP = request.CEP,
                        Bairro = request.Bairro,
                        Localidade = request.Localidade,
                        UF = request.UF
                   },
                   NomeClinica = request.NomeClinica
                };
                var ClinicaApp = new ClinicaApp();

                await ClinicaApp.CadastrarClinica(newClinica);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("CadastroTipoConsulta")]
        public async Task<IActionResult> CadastroTipoDeConsulta(Guid IdClinica, Guid IdConsulta)
        {
            var clinicaApp = new ClinicaApp();

            await clinicaApp.CadastroTipoDeConsulta(IdClinica, IdConsulta);

            return Ok();
        }

        [HttpPost("CadastroTipoExame")]
        public async Task<IActionResult> CadastroTipoDeExame(Guid IdClinica, Guid IdExame)
        {
            var clinicaApp = new ClinicaApp();

            await clinicaApp.CadastroTipoDeExame(IdClinica, IdExame);

            return Ok();
        }


        [HttpPost("GetListaClinicaByDistancia")]
        [Authorize(Roles = "paciente")]
        public async Task<IActionResult> GetListaClinicaByDistancia(string coordenada)
        {
            string key = Startup.Configuration.GetSection("MAPSKey").Value;

            try
            {
                var clinicaApp = new ClinicaApp();

                return Ok(await clinicaApp.GetClinica(key, coordenada));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

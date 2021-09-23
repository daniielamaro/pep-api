using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Dominio.Entities;
using System.Threading.Tasks;
using WebApi.RequestModels;
using Aplicacao;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsultaController : ControllerBase
    {
    
    [HttpPost("CadastroConsulta")]
    [Authorize]
        public async Task<IActionResult> CriarConsulta(RequestCriarConsulta criarConsulta)
        {
            try
            {
                var newConsulta = new Consulta
                {
                    Observacoes = criarConsulta.Observacoes,
                    Resumo = criarConsulta.Resumo,
                    DataCriacao = DateTime.Now
                };

                var consultaApp = new ConsultaApp();

                await consultaApp.CadastroConsulta(newConsulta, criarConsulta.IdPaciente, criarConsulta.IdTipoConsulta);

                return Ok();
            }
            catch (Exception)
            {

                throw;
            }

        }


        [HttpPost("ConsultarLista")]
        [Authorize]
        public async Task<IActionResult> ConsultarLista(Guid id)
        {
            try
            {
                var consultApp = new ConsultaApp();

                return Ok(await consultApp.ConsultarLista(id));

                
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("ConsultaById")]
        [Authorize]
        public async Task<IActionResult> ConsultaById(Guid id)
        {
            try
            {
                var consultaApp = new ConsultaApp();

                return Ok(await consultaApp.ConsultaById(id));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost("ListaTiposConsultas")]
        [Authorize]
        public async Task<IActionResult> ListaTiposConsultas()
        {
            try
            {
                var consultaApp = new ConsultaApp();

                return Ok(await consultaApp.ListaTiposConsulta());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

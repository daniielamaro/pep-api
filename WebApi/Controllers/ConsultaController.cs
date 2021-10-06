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
        public async Task<IActionResult> CadastroConsulta(RequestCriarConsulta request)
        {
            try
            {
                var newConsulta = new Consulta
                {
                    Id = Guid.NewGuid(),
                    Observacoes = request.Observacoes,
                    Resumo = request.Resumo,
                    DataCriacao = DateTime.Now
                };

                var consultaApp = new ConsultaApp();

                await consultaApp.CadastroConsulta(newConsulta, request.IdPaciente, request.IdTipoConsulta);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost("ListaConsulta")]
        [Authorize]
        public async Task<IActionResult> ListaConsulta(Guid idPaciente)
        {
            try
            {
                var consultApp = new ConsultaApp();

                return Ok(await consultApp.ConsultarLista(idPaciente));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("GetConsultaById")]
        [Authorize]
        public async Task<IActionResult> GetConsultaById(Guid id)
        {
            try
            {
                var consultaApp = new ConsultaApp();

                return Ok(await consultaApp.GetConsultaById(id));
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

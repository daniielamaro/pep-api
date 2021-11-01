using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Dominio.Entities;
using System.Threading.Tasks;
using WebApi.RequestModels;
using Aplicacao;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsultaController : ControllerBase
    {
    
        [HttpPost("CadastroConsulta")]
        [Authorize(Roles = "paciente,medico,enfermeiro")]
        public async Task<IActionResult> CadastroConsulta(RequestCriarConsulta request)
        {
            var idPacienteToken = User.FindFirst(ClaimTypes.Sid)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (request.IdPaciente != Guid.Parse(idPacienteToken) && role == "paciente")
                return BadRequest("Você não tem permissão de cadastrar consultas de outro usuario!");

            try
            {
                var newConsulta = new Consulta
                {
                    Observacoes = request.Observacoes,
                    Publico = request.Publico,
                    Resumo = request.Resumo,
                    DiaRealizacao = DateTime.Parse(request.DiaRealizacao),
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
        [Authorize(Roles = "paciente,medico,enfermeiro")]
        public async Task<IActionResult> ListaConsulta(Guid idPaciente)
        {
            var idPacienteToken = User.FindFirst(ClaimTypes.Sid)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (idPaciente != Guid.Parse(idPacienteToken) && role == "paciente")
                return BadRequest("Você não tem permissão de listar consultas de outro usuario!");

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
        [Authorize(Roles = "paciente,medico,enfermeiro")]
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

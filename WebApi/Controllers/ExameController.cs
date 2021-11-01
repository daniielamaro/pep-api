using Dominio.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using Aplicacao;
using System.Threading.Tasks;
using WebApi.RequestModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExameController : ControllerBase
    {
        [HttpPost("CadastroExame")]
        [Authorize(Roles = "paciente,medico,enfermeiro")]
        public async Task<IActionResult> CadastroExame(RequestCriarExame exame)
        {
            var idPacienteToken = User.FindFirst(ClaimTypes.Sid)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (exame.IdPaciente != Guid.Parse(idPacienteToken) && role == "paciente")
                return BadRequest("Você não tem permissão de cadastrar exames de outro usuario!");


            try
            {
                var newExame = new Exame
                {
                    Resultado = new Arquivo
                    {
                        Nome = exame.Arquivo.Nome,
                        Tipo = exame.Arquivo.Tipo,
                        Binario = exame.Arquivo.Binario
                    },
                    DiaRealizacao = DateTime.Parse(exame.DataRealizacao),
                    Observacoes = exame.Observacoes,
                    Publico = exame.Publico,
                    DataCriacao = DateTime.Now
                };
                var ExameApp = new ExameApp();

                await ExameApp.CadastrarExame(newExame, exame.IdPaciente, exame.IdTipoExame);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("ConsultarListaExame")]
        [Authorize(Roles = "paciente,medico,enfermeiro")]
        public async Task<IActionResult> ConsultarListaExame(Guid id)
        {
            var idPacienteToken = User.FindFirst(ClaimTypes.Sid)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (id != Guid.Parse(idPacienteToken) && role == "paciente")
                return BadRequest("Você não tem permissão de listar exames de outro usuario!");

            try
            {
                var ExameApp = new ExameApp();

                return Ok(await ExameApp.ConsultarListaExame(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("ConsultarExameById")]
        [Authorize(Roles = "paciente,medico,enfermeiro")]
        public async Task<IActionResult> ConsultarExameById(Guid id) 
        {
            try
            {
                var ExameApp = new ExameApp();

                return Ok(await ExameApp.ConsultarExameById(id));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost("ConsultarListaTiposExame")]
        [Authorize]
        public async Task<IActionResult> ConsultarListaTiposExame()
        {
            try
            {
                var ExameApp = new ExameApp();

                return Ok(await ExameApp.ConsultarListaTiposExame());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

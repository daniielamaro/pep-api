using Dominio.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using Aplicacao;
using System.Threading.Tasks;
using WebApi.RequestModels;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExameController : ControllerBase
    {
        [HttpPost("CadastroExame")]
        [Authorize]
        public async Task<IActionResult> CadastroExame(RequestCriarExame exame)
        {
            try
            {
                var newExame = new Exame
                {
                    Resultado = new Arquivo
                    {
                        Nome = exame.Arquvio.Nome,
                        Tipo = exame.Arquvio.Tipo,
                        Binario = exame.Arquvio.Binario
                    },
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
        [Authorize]
        public async Task<IActionResult> ConsultarListaExame(Guid id)
        {
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
        [Authorize]
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
        [HttpPut("UpdateTipoExames")]
        public async Task<IActionResult> UpdateTipoExames(RequestUpdateExame updateExame)
        {
             var ExameApp = new ExameApp();

            await ExameApp.UpdateTipoExame(updateExame.Id, updateExame.Nome, updateExame.Descricao);

            return Ok();

        }

        [HttpPut("UpdateExames")]
        public async Task<IActionResult> UpdateExames(RequestExames updateExame)
        {
            var ExameApp = new ExameApp();

            await ExameApp.UpdateExame(updateExame.Id, updateExame.TipoId, updateExame.Publico, updateExame.Observacao);

            return Ok();

        }
    }
}

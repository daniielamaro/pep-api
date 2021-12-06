using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Aplicacao;
using Dominio.Entities;
using Infraestrutura;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.RequestModels;
using WebApi.Servicos;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MedicoController : ControllerBase
    {
        [HttpPost("AcessarSistema")]
        [AllowAnonymous]
        public async Task<IActionResult> AcessarSistema(string usuario, string senha)
        {
            try
            {
                var medicoApp = new MedicoApp();

                var medico = await medicoApp.AcessarSistema(usuario, senha);

                return Ok(new
                {
                    Token = TokenService.GenerateToken(medico),
                    Medico = medico
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Cadastrar")]
        [Authorize(Roles = "administrador, agente")]
        public async Task<IActionResult> Cadastrar(RequestCriarMedico request)
        {
            try
            {
                var newMedico = new Medico
                {
                    Nome = request.Nome,
                    CRM = request.CRM,
                    Email = request.Email,
                    Senha = new Random().Next(100000, 999999).ToString(),
                    DataCriacao = DateTime.Now
                };

                var medicoApp = new MedicoApp();

                await medicoApp.Cadastrar(newMedico, request.IdClinica);

                return Ok(newMedico);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpPost("Update")]
        [Authorize(Roles = "administrador, agente")]
        public async Task<IActionResult> Update(RequestUpdateMedico request)
        {
            try
            {
                var medico = new Medico
                {
                    Id = request.Id,
                    Nome = request.Nome,
                    CRM = request.CRM,
                    Email = request.Email
                };

                var medicoApp = new MedicoApp();

                await medicoApp.Update(medico);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPost("ResetSenha")]
        [Authorize(Roles = "administrador, agente")]
        public async Task<IActionResult> ResetSenha(Guid id)
        {
            try
            {
                var medicoApp = new MedicoApp();

                return Ok(await medicoApp.ResetSenha(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPost("TrocarSenha")]
        [Authorize(Roles = "medico")]
        public async Task<IActionResult> TrocarSenha(Guid id, string senhaAtual, string novaSenha)
        {
            var idToken = User.FindFirst(ClaimTypes.Sid)?.Value;

            if (id != Guid.Parse(idToken))
                return BadRequest("Você não tem permissão de alterar a senha de outro médico!");

            try
            {
                var medicoApp = new MedicoApp();

                return Ok(await medicoApp.TrocarSenha(id, senhaAtual, novaSenha));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpDelete("DeleteAgente")]
        [Authorize(Roles = "administrador, agente")]
        public async Task<IActionResult> DeleteAgente(Guid id)
        {
            try
            {
                var medicoApp = new MedicoApp();

                await medicoApp.DeleteAgente(id);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPost("GetMedicoById")]
        [Authorize(Roles = "administrador, agente")]
        public async Task<IActionResult> GetMedicoById(Guid id)
        {
            try
            {
                var medicoApp = new MedicoApp();

                return Ok(await medicoApp.GetMedicoById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPost("GetHistoricoAtendimento")]
        [Authorize(Roles = "medico")]
        public async Task<IActionResult> GetHistoricoAtendimento()
        {
            var id = User.FindFirst(ClaimTypes.Sid)?.Value;

            try
            {
                var medicoApp = new MedicoApp();

                return Ok(await medicoApp.GetHistoricoAtendimento(Guid.Parse(id)));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPost("GetHistoricoPrescricoes")]
        [Authorize(Roles = "medico")]
        public async Task<IActionResult> GetHistoricoPrescricoes()
        {
            var id = User.FindFirst(ClaimTypes.Sid)?.Value;

            try
            {
                var medicoApp = new MedicoApp();

                return Ok(await medicoApp.GetHistoricoPrescricoes(Guid.Parse(id)));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPost("AlterarFotoPerfil")]
        [Authorize(Roles = "medico")]
        public async Task<IActionResult> AlterarFotoPerfil(RequestSetFotoPerfil request)
        {
            var idPacienteToken = User.FindFirst(ClaimTypes.Sid)?.Value;

            if (request.Id != Guid.Parse(idPacienteToken))
                return BadRequest("Você não tem permissão de alterar a foto de outro usuario!");

            try
            {
                var pacienteApp = new PacienteApp();

                

                var paciente = await pacienteApp.AlterarFotoPerfil(
                    request.Id,
                    new Arquivo {
                        Nome = request.Foto.Nome,
                        Tipo = request.Foto.Tipo,
                        Binario = request.Foto.Binario,
                        DataCriacao = DateTime.Now
                    }
                );

                return Ok(paciente);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("DeletarFotoPerfil")]
        [Authorize(Roles = "medico")]
        public async Task<IActionResult> DeletarFotoPerfil(Guid id)
        {
            var idPacienteToken = User.FindFirst(ClaimTypes.Sid)?.Value;

            if (id != Guid.Parse(idPacienteToken))
                return BadRequest("Você não tem permissão de deletar a foto de outro usuario!");

            try
            { 
                var pacienteApp = new PacienteApp();

                await pacienteApp.DeletarFotoPerfil(id);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

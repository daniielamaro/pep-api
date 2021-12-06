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
    public class EnfermeiroController : ControllerBase
    {
        [HttpPost("AcessarSistema")]
        [AllowAnonymous]
        public async Task<IActionResult> AcessarSistema(string usuario, string senha)
        {
            try
            {
                var enfermeiroApp = new EnfermeiroApp();

                var enfermeiro = await enfermeiroApp.AcessarSistema(usuario, senha);

                return Ok(new
                {
                    Token = TokenService.GenerateToken(enfermeiro),
                    Enfermeiro = enfermeiro
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Cadastrar")]
        [Authorize(Roles = "administrador, agente")]
        public async Task<IActionResult> Cadastrar(RequestCriarEnfermeiro request)
        {
            try
            {
                var newEnfermeiro = new Enfermeiro
                {
                    Nome = request.Nome,
                    COREM = request.COREM,
                    Email = request.Email,
                    Senha = new Random().Next(100000, 999999).ToString(),
                    DataCriacao = DateTime.Now
                };

                var enfermeiroApp = new EnfermeiroApp();

                await enfermeiroApp.Cadastrar(newEnfermeiro, request.IdClinica);

                return Ok(newEnfermeiro);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpPost("Update")]
        [Authorize(Roles = "administrador, agente")]
        public async Task<IActionResult> Update(RequestUpdateEnfermeiro request)
        {
            try
            {
                var enfermeiro = new Enfermeiro
                {
                    Id = request.Id,
                    Nome = request.Nome,
                    COREM = request.COREM,
                    Email = request.Email
                };

                var enfermeiroApp = new EnfermeiroApp();

                await enfermeiroApp.Update(enfermeiro);

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
                var enfermeiroApp = new EnfermeiroApp();

                return Ok(await enfermeiroApp.ResetSenha(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPost("TrocarSenha")]
        [Authorize(Roles = "enfermeiro")]
        public async Task<IActionResult> TrocarSenha(Guid id, string senhaAtual, string novaSenha)
        {
            var idToken = User.FindFirst(ClaimTypes.Sid)?.Value;

            if (id != Guid.Parse(idToken))
                return BadRequest("Você não tem permissão de alterar a senha de outro enfermeiro!");

            try
            {
                var enfermeiroApp = new EnfermeiroApp();

                return Ok(await enfermeiroApp.TrocarSenha(id, senhaAtual, novaSenha));
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
                var enfermeiroApp = new EnfermeiroApp();

                await enfermeiroApp.DeleteAgente(id);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPost("GetEnfermeiroById")]
        [Authorize(Roles = "administrador, agente")]
        public async Task<IActionResult> GetEnfermeiroById(Guid id)
        {
            try
            {
                var enfermeiroApp = new EnfermeiroApp();

                return Ok(await enfermeiroApp.GetEnfermeiroById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPost("GetHistoricoAtendimento")]
        [Authorize(Roles = "enfermeiro")]
        public async Task<IActionResult> GetHistoricoAtendimento()
        {
            var id = User.FindFirst(ClaimTypes.Sid)?.Value;

            try
            {
                var enfermeiroApp = new EnfermeiroApp();

                return Ok(await enfermeiroApp.GetHistoricoAtendimento(Guid.Parse(id)));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPost("GetHistoricoPrescricoes")]
        [Authorize(Roles = "enfermeiro")]
        public async Task<IActionResult> GetHistoricoPrescricoes()
        {
            var id = User.FindFirst(ClaimTypes.Sid)?.Value;

            try
            {
                var enfermeiroApp = new EnfermeiroApp();

                return Ok(await enfermeiroApp.GetHistoricoPrescricoes(Guid.Parse(id)));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPost("AlterarFotoPerfil")]
        [Authorize(Roles = "enfermeiro")]
        public async Task<IActionResult> AlterarFotoPerfil(RequestSetFotoPerfil request)
        {
            var idToken = User.FindFirst(ClaimTypes.Sid)?.Value;

            if (request.Id != Guid.Parse(idToken))
                return BadRequest("Você não tem permissão de alterar a foto de outro usuario!");

            try
            {
                var enfermeiroApp = new EnfermeiroApp();

                var medico = await enfermeiroApp.AlterarFotoPerfil(
                    request.Id,
                    new Arquivo {
                        Nome = request.Foto.Nome,
                        Tipo = request.Foto.Tipo,
                        Binario = request.Foto.Binario,
                        DataCriacao = DateTime.Now
                    }
                );

                return Ok(medico);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("DeletarFotoPerfil")]
        [Authorize(Roles = "paciente")]
        public async Task<IActionResult> DeletarFotoPerfil(Guid id)
        {
            var idToken = User.FindFirst(ClaimTypes.Sid)?.Value;

            if (id != Guid.Parse(idToken))
                return BadRequest("Você não tem permissão de deletar a foto de outro usuario!");

            try
            { 
                var enfermeiroApp = new EnfermeiroApp();

                await enfermeiroApp.DeletarFotoPerfil(id);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

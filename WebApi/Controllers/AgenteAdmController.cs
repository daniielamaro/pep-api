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
    public class AgenteAdmController : ControllerBase
    {
        [HttpPost("AcessarSistema")]
        [AllowAnonymous]
        public async Task<IActionResult> AcessarSistema(string usuario, string senha)
        {
            try
            {
                var agenteAdmApp = new AgenteAdmApp();

                var agente = await agenteAdmApp.AcessarSistema(usuario, senha);

                return Ok(new
                {
                    Token = TokenService.GenerateToken(agente),
                    Agente = agente
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("GetListaFuncionario")]
        [Authorize(Roles = "administrador, agente")]
        public async Task<IActionResult> GetListaFuncionario(Guid idClinica)
        {
            try
            {
                var AgenteAdmApp = new AgenteAdmApp();

                return Ok(await AgenteAdmApp.GetListaFuncionario(idClinica));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPost("Cadastrar")]
        [Authorize(Roles = "administrador, agente")]
        public async Task<IActionResult> Cadastrar(RequestCriarAgente request)
        {
            try
            {
                var agente = new AgenteAdministrativo
                {
                    Nome = request.Nome,
                    CPF = request.CPF,
                    Email = request.Email,
                    Senha = new Random().Next(100000, 999999).ToString(),
                    DataCriacao = DateTime.Now
                };

                var AgenteAdmApp = new AgenteAdmApp();

                return Ok(await AgenteAdmApp.Cadastrar(agente, request.IdClinica));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpPost("Update")]
        [Authorize(Roles = "administrador, agente")]
        public async Task<IActionResult> Update(RequestUpdateAgente request)
        {
            try
            {
                var agente = new AgenteAdministrativo
                {
                    Id = request.Id,
                    Nome = request.Nome,
                    CPF = request.CPF,
                    Email = request.Email
                };

                var AgenteAdmApp = new AgenteAdmApp();

                await AgenteAdmApp.Update(agente);

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
                var AgenteAdmApp = new AgenteAdmApp();

                return Ok(await AgenteAdmApp.ResetSenha(id));
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
                var AgenteAdmApp = new AgenteAdmApp();

                await AgenteAdmApp.DeleteAgente(id);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPost("GetAgenteById")]
        [Authorize(Roles = "administrador, agente")]
        public async Task<IActionResult> GetAgenteById(Guid id)
        {
            try
            {
                var agenteAdmApp = new AgenteAdmApp();

                return Ok(await agenteAdmApp.GetAgenteById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPost("AlterarFotoPerfil")]
        [Authorize(Roles = "agente")]
        public async Task<IActionResult> AlterarFotoPerfil(RequestSetFotoPerfil request)
        {
            var idToken = User.FindFirst(ClaimTypes.Sid)?.Value;

            if (request.Id != Guid.Parse(idToken))
                return BadRequest("Você não tem permissão de alterar a foto de outro usuario!");

            try
            {
                var AgenteAdmApp = new AgenteAdmApp();

                var agente = await AgenteAdmApp.AlterarFotoPerfil(
                    request.Id,
                    new Arquivo {
                        Nome = request.Foto.Nome,
                        Tipo = request.Foto.Tipo,
                        Binario = request.Foto.Binario,
                        DataCriacao = DateTime.Now
                    }
                );

                return Ok(agente);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("DeletarFotoPerfil")]
        [Authorize(Roles = "agente")]
        public async Task<IActionResult> DeletarFotoPerfil(Guid id)
        {
            var idToken = User.FindFirst(ClaimTypes.Sid)?.Value;

            if (id != Guid.Parse(idToken))
                return BadRequest("Você não tem permissão de deletar a foto de outro usuario!");

            try
            { 
                var AgenteAdmApp = new AgenteAdmApp();

                await AgenteAdmApp.DeletarFotoPerfil(id);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

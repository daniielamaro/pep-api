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

        [HttpPost("VerificarToken")]
        [AllowAnonymous]
        public IActionResult VerificarToken(string token)
        {
            try
            {
                var valido = TokenService.ValidateToken(token);

                if (valido)
                    return Ok();
                else
                    return BadRequest("Token Invalido!");
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

                await AgenteAdmApp.Cadastrar(agente, request.IdClinica);

                return Ok(agente);
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

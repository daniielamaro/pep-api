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
    public class PacienteController : ControllerBase
    {
        [HttpPost("AcessarSistema")]
        [AllowAnonymous]
        public async Task<IActionResult> AcessarSistema(string usuario, string senha)
        {
            try
            {
                var pacienteApp = new PacienteApp();

                var paciente = await pacienteApp.AcessarSistema(usuario, senha);

                return Ok(new
                {
                    Token = TokenService.GenerateToken(paciente),
                    Paciente = paciente
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Cadastrar")]
        [AllowAnonymous]
        public async Task<IActionResult> Cadastrar(RequestCriarPaciente request)
        {
            try
            {
                var newPaciente = new Paciente
                {
                    Nome = request.Nome,
                    Cpf = request.CPF,
                    Rg = request.RG,
                    Email = request.Email,
                    DataNasc = DateTime.Parse(request.DataNasc),
                    Endereco = request.Endereco,
                    Senha = request.Senha,
                    DataCriacao = DateTime.Now
                };

                var pacienteApp = new PacienteApp();

                await pacienteApp.CadastrarPaciente(newPaciente);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpPost("AlterarFotoPerfil")]
        [Authorize(Roles = "paciente")]
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
        [Authorize(Roles = "paciente")]
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

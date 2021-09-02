﻿using System;
using System.Threading.Tasks;
using Aplicacao;
using Dominio.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
                    DataNasc = DateTime.Parse(request.DataNasc),
                    Endereco = request.Endereco,
                    Senha = request.Senha
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
        [Authorize]
        public async Task<IActionResult> AlterarFotoPerfil(RequestSetFotoPerfil request)
        {
            try
            {
                var pacienteApp = new PacienteApp();

                var paciente = await pacienteApp.AlterarFotoPerfil(
                    request.Id,
                    new Arquivo {
                        Nome = request.Foto.Nome,
                        Tipo = request.Foto.Tipo,
                        Binario = request.Foto.Binario
                    }
                );

                return Ok(paciente);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
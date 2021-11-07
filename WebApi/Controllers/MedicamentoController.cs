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
    public class MedicamentoController : ControllerBase
    {
        [HttpPost("Cadastro")]
        [Authorize(Roles = "paciente,medico,enfermeiro")]
        public async Task<IActionResult> Cadastro(RequestCriarMedicamento request)
        {
            var idPacienteToken = User.FindFirst(ClaimTypes.Sid)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (request.IdPaciente != Guid.Parse(idPacienteToken) && role == "paciente")
                return BadRequest("Você não tem permissão de cadastrar medicamentos de outro usuario!");

            Arquivo receita = null;

            if(request.Receita != null)
            {
                receita = new Arquivo
                {
                    Nome = request.Receita.Nome,
                    Tipo = request.Receita.Tipo,
                    Binario = request.Receita.Binario
                };
            }

            try
            {
                var newMedicamento = new Medicamento
                {
                    Nome = request.Nome,
                    NumQuantidade = request.NumQuantidade,
                    TipoQuantidade = request.TipoQuantidade,
                    OutraQuantidade = request.OutraQuantidade,
                    NumIntervalo = request.NumIntervalo,
                    TipoIntervalo = request.TipoIntervalo,
                    OutroIntervalo = request.OutroIntervalo,
                    Publico = request.Publico,
                    TipoCadastro = request.TipoCadastro,
                    Receita = receita,
                    DataInicio = DateTime.Parse(request.DataInicio),
                    UsoContinuo = request.UsoContinuo,
                    DataCriacao = DateTime.Now
                };

                if (!request.UsoContinuo)
                    newMedicamento.DataTermino = DateTime.Parse(request.DataTermino);

                var servico = new MedicamentoApp();

                await servico.Cadastrar(newMedicamento, request.IdPaciente);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("ConsultarListaMedicamento")]
        [Authorize(Roles = "paciente,medico,enfermeiro")]
        public async Task<IActionResult> ConsultarListaMedicamento(Guid idPaciente)
        {
            var idPacienteToken = User.FindFirst(ClaimTypes.Sid)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (idPaciente != Guid.Parse(idPacienteToken) && role == "paciente")
                return BadRequest("Você não tem permissão de listar medicamentos de outro usuario!");

            try
            {
                var servico = new MedicamentoApp();

                return Ok(await servico.ConsultarListaMedicamento(idPaciente));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("ConsultarMedicamentoById")]
        [Authorize(Roles = "paciente,medico,enfermeiro")]
        public async Task<IActionResult> ConsultarMedicamentoById(Guid idMedicamento) 
        {
            try
            {
                var servico = new MedicamentoApp();

                return Ok(await servico.ConsultarMedicamentoById(idMedicamento));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("EditarMedicamento")]
        [Authorize(Roles = "paciente,medico,enfermeiro")]
        public async Task<IActionResult> EditarMedicamento(RequestEditarMedicamento request)
        {
            var idPacienteToken = User.FindFirst(ClaimTypes.Sid)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            try
            {
                var newMedicamento = new Medicamento
                {
                    Id = request.IdMedicamento,
                    Nome = request.Nome,
                    NumQuantidade = request.NumQuantidade,
                    TipoQuantidade = request.TipoQuantidade,
                    OutraQuantidade = request.OutraQuantidade,
                    NumIntervalo = request.NumIntervalo,
                    TipoIntervalo = request.TipoIntervalo,
                    OutroIntervalo = request.OutroIntervalo,
                    Publico = request.Publico,
                    TipoCadastro = request.TipoCadastro,
                    Receita = request.Receita,
                    DataInicio = DateTime.Parse(request.DataInicio),
                    UsoContinuo = request.UsoContinuo
                };

                if (!request.UsoContinuo)
                    newMedicamento.DataTermino = DateTime.Parse(request.DataTermino);

                var servico = new MedicamentoApp();

                await servico.Editar(newMedicamento, role, Guid.Parse(idPacienteToken));

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

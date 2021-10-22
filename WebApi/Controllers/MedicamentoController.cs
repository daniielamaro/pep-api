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
    public class MedicamentoController : ControllerBase
    {
        [HttpPost("Cadastro")]
        [Authorize]
        public async Task<IActionResult> Cadastro(RequestCriarMedicamento request)
        {
            try
            {
                var newMedicamento = new Medicamento
                {
                    Id = Guid.NewGuid(),
                    Nome = request.Nome,
                    Quantidade = request.Quantidade,
                    Intervalo = request.Intervalo,
                    DataTermino = request.DataTermino,
                    UsoContinuo = request.UsoContinuo,
                    DataCriacao = DateTime.Now
                };

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
        [Authorize]
        public async Task<IActionResult> ConsultarListaMedicamento(Guid idPaciente)
        {
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
        [Authorize]
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
        [HttpPut("UpdateMedicamento")]
        public async Task<IActionResult> UpdateMedicamento(RequestMedicamentoUpdate request)
        {
            var medicamentoApp = new MedicamentoApp();

            await medicamentoApp.UpdateMedicamento( request.Id,request.Nome, request.Quantidade, request.Intervalo, request.UsoContinuo);

            return Ok();
        }
    }
}

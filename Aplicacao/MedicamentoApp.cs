using Dominio.Entities;
using Infraestrutura;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao
{
    public class MedicamentoApp
    {
        public async Task Cadastrar(Medicamento medicamento, Guid idPaciente)
        {
            using var context = new ApiContext();

            var paciente = await context.Pacientes
                .Include(x => x.Medicamentos)
                    .ThenInclude(x => x.Receita)
                .Where(x => x.Id == idPaciente).FirstOrDefaultAsync();

            if (paciente == null)
                throw new Exception("Paciente não encontrado!");

            if (paciente.Medicamentos == null)
                paciente.Medicamentos = new List<Medicamento>();

            paciente.Medicamentos.Add(medicamento);

            context.Pacientes.Update(paciente);

            await context.SaveChangesAsync();
        }

        public async Task<List<Medicamento>> ConsultarListaMedicamento(Guid idPaciente)
        {
            using var context = new ApiContext();

            var paciente = await context.Pacientes.AsNoTracking()
                .Include(x => x.Medicamentos)
                .Where(x => x.Id == idPaciente).FirstOrDefaultAsync();

            if (paciente == null)
                throw new Exception("Paciente não encontrado!");

            return paciente.Medicamentos;
        }

        public async Task<Medicamento> ConsultarMedicamentoById(Guid idMedicamento)
        {
            using var context = new ApiContext();

            var medicamento = await context.Medicamentos.AsNoTracking().Include(x => x.Receita).Where(x => x.Id == idMedicamento).FirstOrDefaultAsync();

            if (medicamento == null)
                throw new Exception("Medicamento não encontrado!");

            return medicamento;

        }

        public async Task Editar(Medicamento medicamento, string role, Guid idUser = new Guid())
        {
            using var context = new ApiContext();
            Medicamento med = null;

            if(role == "paciente")
            {
                var paciente = await context.Pacientes
                    .Include(x => x.Medicamentos)
                        .ThenInclude(x => x.Receita)
                    .Where(x => x.Id == idUser)
                    .FirstOrDefaultAsync();

                if (paciente == null)
                    throw new Exception("Paciente não encontrado!");

                med = paciente.Medicamentos.Where(x => x.Id == medicamento.Id).FirstOrDefault();

                if (med == null)
                    throw new Exception("Você não tem permissão de editar medicamentos de outro usuario!");
            }

            med.Nome = medicamento.Nome;
            med.NumQuantidade = medicamento.NumQuantidade;
            med.TipoQuantidade = medicamento.TipoQuantidade;
            med.OutraQuantidade = medicamento.OutraQuantidade;
            med.NumIntervalo = medicamento.NumIntervalo;
            med.TipoIntervalo = medicamento.TipoIntervalo;
            med.OutroIntervalo = medicamento.OutroIntervalo;
            med.Publico = medicamento.Publico;
            med.TipoCadastro = medicamento.TipoCadastro;

            if(medicamento.Receita != null && med.Receita != null)
            {
                med.Receita.Nome = medicamento.Receita.Nome;
                med.Receita.Tipo = medicamento.Receita.Tipo;
                med.Receita.Binario = medicamento.Receita.Binario;
            }
            else if (medicamento.Receita == null && med.Receita != null)
            {
                context.Arquivos.Remove(med.Receita);
                med.Receita = null;
            }
            else if (medicamento.Receita != null && med.Receita == null)
            {
                Arquivo receita = new Arquivo
                {
                    Nome = medicamento.Receita.Nome,
                    Tipo = medicamento.Receita.Tipo,
                    Binario = medicamento.Receita.Binario
                };

                med.Receita = receita;
            }

            med.DataInicio = medicamento.DataInicio;
            med.DataTermino = medicamento.DataTermino;
            med.UsoContinuo = medicamento.UsoContinuo;

            context.Medicamentos.Update(med);

            await context.SaveChangesAsync();
        }
    }
}

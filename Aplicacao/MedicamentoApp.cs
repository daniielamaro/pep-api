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

            var paciente = await context.Pacientes.Include(x => x.Medicamentos).Where(x => x.Id == idPaciente).FirstOrDefaultAsync();

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

            var medicamento = await context.Medicamentos.AsNoTracking().Where(x => x.Id == idMedicamento).FirstOrDefaultAsync();

            if (medicamento == null)
                throw new Exception("Medicamento não encontrado!");

            return medicamento;

        }
    }
}

using Dominio.Entities;
using Infraestrutura;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacao
{
    public class ExameApp
    {
        public async Task CadastrarExame(Exame exames, Guid id, Guid idTipoExame)
        {
            using var context = new ApiContext();

            var paciente = await context.Pacientes.Include(x => x.Exames).Where(x => x.Id == id).FirstOrDefaultAsync();

            var tipoExames = await context.TiposExames.Where(x => x.Id == idTipoExame).FirstOrDefaultAsync();

            if (paciente == null)
                throw new Exception("Usuario não encontrado");
            else if (tipoExames == null)
                throw new Exception("Tipo de Exame não encontrado");

            exames.Tipo =  tipoExames;

            paciente.Exames.Add(exames);
            
            context.Pacientes.Update(paciente);

            await context.SaveChangesAsync();
        }

        public async Task<List<Exame>> ConsultarListaExame(Guid id)
        {
            using var context = new ApiContext();

            var paciente = await context.Pacientes.AsNoTracking()
                .Include(x => x.Exames)
                    .ThenInclude(e => e.Tipo)
                .Where(x => x.Id == id).FirstOrDefaultAsync();

            if (paciente == null)
                throw new Exception("Paciente não encontrado!");

            var exames = paciente.Exames.OrderByDescending(x => x.DiaRealizacao).ToList();

            return exames;
        }

        public async Task<object> ConsultarExameById(Guid Id)
        {
            using var context = new ApiContext();

            var ConsultaId = await context.Exames.AsNoTracking().Include(x => x.Tipo).Where(x => x.Id == Id).SingleOrDefaultAsync();
              
            return ConsultaId;

        }

        public async Task<List<ExameTipo>> ConsultarListaTiposExame()
        {
            using var context = new ApiContext();

            return await context.TiposExames.AsNoTracking().OrderBy(x => x.Nome).ToListAsync();
        }
    }
}

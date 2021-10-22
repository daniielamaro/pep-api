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
    public class ConsultaApp
    {
        public async Task CadastroConsulta( Consulta consultas, Guid idPaciente, Guid idTipoConsulta)
        {
            using var context = new ApiContext();

            var paciente = await context.Pacientes.Include(x => x.Consultas).Where(x => x.Id == idPaciente).FirstOrDefaultAsync();

            var tipoConsulta = await context.TiposConsultas.Where(x => x.Id == idTipoConsulta).FirstOrDefaultAsync();

            if(paciente == null)
                throw new Exception("Usuario não encontrado");
            else if (tipoConsulta == null)
                throw new Exception("Tipo de Consulta não encontrado");

            consultas.Tipo = tipoConsulta;

            //if (paciente.Consultas == null)
                //paciente.Consultas = new List<Consulta>();

            paciente.Consultas.Add(consultas);

            context.Pacientes.Update(paciente);

            await context.SaveChangesAsync();
        }


        public async Task<List<Consulta>> ConsultarLista(Guid id)
        {
            using var context = new ApiContext();

            var paciente = await context.Pacientes.AsNoTracking()
                .Include(x => x.Consultas)
                    .ThenInclude(x => x.Tipo)
                .Where(x => x.Id == id).FirstOrDefaultAsync();

             if (paciente == null)
                throw new Exception("Paciente não encontrado!");

            return paciente.Consultas;
        }

        public async Task<object> GetConsultaById(Guid Id)
        {
            using var context = new ApiContext();

            var consulta = await context.Consultas.AsNoTracking().Include(x => x.Tipo).Where(x => x.Id == Id).FirstOrDefaultAsync();

            if (consulta == null)
                throw new Exception("Consulta não encontrada!");

            return consulta;
        }

        public async Task<List<ConsultaTipo>> ListaTiposConsulta()
        {
            using var context = new ApiContext();

            return await context.TiposConsultas.AsNoTracking().ToListAsync();
        }

        /*public async Task<object> UpdateConsultaTipo()
        {

        }*/

    }
}

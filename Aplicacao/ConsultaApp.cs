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
        public async Task CadastroConsulta( Consulta consultas, Guid Id, Guid TipoId )
        {
            using var context = new ApiContext();

            var Pacientes = await context.Pacientes.Include(x => x.Consultas).Where(x => x.Id == Id).FirstOrDefaultAsync();

            var TipoConsultas = await context.TiposConsultas.Where(x => x.Id == TipoId).FirstOrDefaultAsync();

            if(Pacientes == null)
            
                throw new Exception("Usuario não encontrado");

            else if (TipoConsultas == null)

                    throw new Exception("Tipo de Consulta não encontrado");

            consultas.Tipo = TipoConsultas;

            Pacientes.Consultas.Add(consultas);

            context.Pacientes.Update(Pacientes);

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

            var consulta = paciente.Consultas;

            return consulta;
        }

        public async Task<object> ConsultaById(Guid Id)
        {
            using var context = new ApiContext();

            var ConsultaId = await context.Consultas.AsNoTracking().Include(x => x.Tipo).Where(x => x.Id == Id).SingleOrDefaultAsync();

            return ConsultaId;

        }

        public async Task<List<ConsultaTipo>> ListaTiposConsulta()
        {
            using var context = new ApiContext();

            return await context.TiposConsultas.AsNoTracking().ToListAsync();
        }

    }
}

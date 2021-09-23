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
    public class ClinicaApp
    {
        public async Task CadastrarClinica(Clinica clinica, Guid idTipoConsulta, Guid idTipoExame)
        {
            using var context = new ApiContext();

            var tipoConsulta = await context.TiposConsultas.Where(x => x.Id == idTipoConsulta).FirstOrDefaultAsync();

            var tipoExames = await context.TiposExames.Where(x => x.Id == idTipoExame).FirstOrDefaultAsync();

            var newClinica = new Clinica
            {
                NomeClinica = clinica.NomeClinica,
                Endereco = clinica.Endereco,
                ConsultaTipos = new List<ClinicaConsultaTipo>(),
                ExameTipos = new List<ClinicaTipoExames>(),
                DataCriacao = DateTime.Now
                
                
            };

            context.Clinicas.Add(newClinica);

            await context.SaveChangesAsync();
        }


        public async Task<object> ListarClinicas(Guid id)
        {
            using var context = new ApiContext();

            /* var tipoConsulta = await context.ClinicaConsultaTipos.AsNoTracking()
                                .Include(x =>x.Clinicas)
                                .Where(x => x.Clinicas.Id == id).ToListAsync();

             var tipoExame = await context.ClinicaExameTipos.AsNoTracking()
                                 .Include(c => c.Clinica)
                                 .Where(c => c.Clinica.Id == id).ToListAsync();

             var clinicas = await context.Clinicas.AsNoTracking()
                 .Where(x => x.Id == id).FirstOrDefaultAsync();*/


            var clinica = await context.ClinicaConsultaTipos.AsNoTracking()
                            .Include(x => x.Clinicas)
                                .ThenInclude(x => x.ExameTipos)
                             .Where(c => c.Clinicas.Id == id)
                            .ToListAsync();

            if (clinica == null)
                throw new Exception("Clinica não encontrado!");

            //clinicas.ExameTipos = tipoExame;
            //clinicas.ConsultaTipos = tipoConsulta;


            var consulta = clinica;

            
            

            

            return consulta;

        }

    }
}

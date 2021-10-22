using Dominio.Entities;
using Infraestrutura;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacao
{
    public class ClinicaApp
    {
        public async Task CadastrarClinica(Clinica clinica)
        {
            using var context = new ApiContext();


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



        public async Task CadastroTipoDeConsulta(Guid IdClinica, Guid IdConsulta)
        {
            using var context = new ApiContext();

            var Clinica = await context.Clinicas.Where(x => x.Id == IdClinica).FirstOrDefaultAsync();

            var Consulta = await context.TiposConsultas.Where(x => x.Id == IdConsulta).FirstOrDefaultAsync();
           


            if (Clinica == null)
                throw new Exception("Clinica não encontrada");
            else if (Consulta == null)
                throw new Exception("Tipo de Consulta não encontrado");


            var newConsulta = new ClinicaConsultaTipo
            {
                ClinicaId = Clinica.Id,
                ConsultaId = Consulta.Id
            };


            context.ClinicaConsultaTipos.Add(newConsulta);

            await context.SaveChangesAsync();
        }


        public async Task CadastroTipoDeExame(Guid IdClinica, Guid IdExame)
        {
            using var context = new ApiContext();

            var Clinica = await context.Clinicas.Where(x => x.Id == IdClinica).FirstOrDefaultAsync();

            var Exame = await context.TiposExames.Where(x => x.Id == IdExame).FirstOrDefaultAsync();

            if (Clinica == null)
                throw new Exception("Clinica não encontrada");
            else if (Exame == null)
                throw new Exception("Tipo de Exame não encontrado");

            var NewExame = new ClinicaTipoExames
            {
                ClinicaId = Clinica.Id,
                ExameId = Exame.Id
            };

            context.ClinicaExameTipos.Add(NewExame);

            await context.SaveChangesAsync();

        }

        public async Task<object> GetClinica(Guid id)
        {
            using var context = new ApiContext();

            var clinica = await context.Clinicas.AsNoTracking()
                                .Include(x => x.ConsultaTipos)
                                    .ThenInclude(x => x.ConsultaTipo)
                                .Include(x => x.ExameTipos)
                                    .ThenInclude(x => x.ExameTipo)
                                .Where(c => c.Id == id)
                                .FirstOrDefaultAsync();

            if (clinica == null)
                throw new Exception("Clinica não encontrada!");

            return new
            {
                clinica.NomeClinica,
                clinica.Endereco,
                TiposConsulta = clinica.ConsultaTipos.Select(x => x.ConsultaTipo).Select(x => new { x.Nome, x.Descricao } ).ToList(),
                TiposExame = clinica.ExameTipos.Select(x => x.ExameTipo).Select(x => new { x.Nome, x.Descricao }).ToList()
            };
        }
    }
}

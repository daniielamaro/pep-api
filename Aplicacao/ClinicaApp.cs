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

using Dominio.Entities;
using Infraestrutura;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacao
{
    public class ExameApp
    {
        public async Task CadastrarExame(Exame exames, Guid id, Guid idTipoExame)
        {


            using var contex = new ApiContext();

            var paciente = await contex.Pacientes.Include(x => x.Exames).Where(x => x.Id == id).FirstOrDefaultAsync();

            var tipoExames = await contex.TiposExames.Where(x => x.Id == idTipoExame).FirstOrDefaultAsync();

            if (paciente == null)
            {
                throw new Exception("Usuario não encontrado");
            }
            else if (tipoExames == null)
            {
                throw new Exception("Tipo de Exame não encontrado");
            }

            exames.Tipo =  tipoExames;

            paciente.Exames.Add(exames);
            
            contex.Pacientes.Update(paciente);
            

            await contex.SaveChangesAsync();
        }
    }
}

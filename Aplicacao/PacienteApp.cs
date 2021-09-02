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
    public class PacienteApp
    {
        public async Task<Paciente> AcessarSistema(string usuario, string senha)
        {
            using var context = new ApiContext();

            Paciente paciente;

            if(usuario.Contains("@"))
                paciente = await context.Pacientes.Include(x => x.FotoPerfil).Where(x => x.Email == usuario).FirstOrDefaultAsync();
            else
                paciente = await context.Pacientes.Include(x => x.FotoPerfil).Where(x => x.Cpf == usuario).FirstOrDefaultAsync();

            if (paciente != null)
            {
                if (paciente.Senha == senha)
                    return paciente;
                else
                    throw new Exception("Senha incorreta!");
            }
            else
                throw new Exception("Paciente não encontrado!");
        }

        public async Task CadastrarPaciente(Paciente paciente)
        {
            using var context = new ApiContext();

            await context.Pacientes.AddAsync(paciente);
            await context.SaveChangesAsync();
        }

        public async Task<Paciente> AlterarFotoPerfil(Guid Id, Arquivo Foto)
        {
            using var context = new ApiContext();

            var paciente = await context.Pacientes.Include(x => x.FotoPerfil).Where(x => x.Id == Id).FirstOrDefaultAsync();

            if (paciente == null)
                throw new Exception("Usuario não encontrado!");

            paciente.FotoPerfil = Foto;

            context.Pacientes.Update(paciente);
            await context.SaveChangesAsync();

            return paciente;
        }
    }
}

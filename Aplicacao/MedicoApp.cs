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
    public class MedicoApp
    {
        public async Task<Medico> AcessarSistema(string usuario, string senha)
        {
            using var context = new ApiContext();

            Medico medico;

            if(usuario.Contains("@"))
                medico = await context.Medicos
                    .Include(x => x.FotoPerfil)
                    .Include(x => x.Clinica)
                    .Where(x => x.Email.ToLower() == usuario.ToLower())
                    .FirstOrDefaultAsync();
            else
                medico = await context.Medicos
                    .Include(x => x.FotoPerfil)
                    .Include(x => x.Clinica)
                    .Where(x => x.CRM.Trim().ToLower().Replace(".", "").Replace("-", "") == usuario.Trim().ToLower().Replace(".", "").Replace("-", ""))
                    .FirstOrDefaultAsync();

            if (medico != null)
            {
                if (medico.Senha == senha)
                    return medico;
                else
                    throw new Exception("Senha incorreta!");
            }
            else
                throw new Exception("Medico não encontrado!");
        }

        public async Task Cadastrar(Medico medico, Guid idClinica)
        {
            using var context = new ApiContext();

            medico.Clinica = await context.Clinicas.Where(x => x.Id == idClinica).FirstOrDefaultAsync();

            await context.Medicos.AddAsync(medico);
            await context.SaveChangesAsync();
        }

        public async Task<Medico> AlterarFotoPerfil(Guid Id, Arquivo Foto)
        {
            using var context = new ApiContext();

            var medico = await context.Medicos.
                Include(x => x.FotoPerfil)
                .Include(x => x.Clinica)
                .Where(x => x.Id == Id).FirstOrDefaultAsync();

            if (medico == null)
                throw new Exception("Usuario não encontrado!");

            if(medico.FotoPerfil == null)
            {
                medico.FotoPerfil = Foto;
                context.Medicos.Update(medico);
            }
            else
            {
                var foto = await context.Arquivos.Where(x => x.Id == medico.FotoPerfil.Id).FirstOrDefaultAsync();

                foto.Nome = Foto.Nome;
                foto.Tipo = Foto.Tipo;
                foto.Binario = Foto.Binario;
                foto.DataAtualizacao = DateTime.Now;

                medico.FotoPerfil = foto;

                context.Arquivos.Update(foto);
            }
            
            await context.SaveChangesAsync();

            return medico;
        }

        /*public async Task<object> UpdateMedico(string email, string endereco, string senha, Guid id)
        {
            using var context = new ApiContext();

            var pacienteOld = await context.Pacientes.AsNoTracking().Where(x => x.Id == id).SingleOrDefaultAsync();

            pacienteOld.Email = email;
            pacienteOld.Endereco = endereco;
            pacienteOld.Senha = senha;
            pacienteOld.DataAtualizacao = DateTime.Now;


            context.Pacientes.Update(pacienteOld);

            await context.SaveChangesAsync();

            return pacienteOld;
            
        }*/

        public async Task DeletarFotoPerfil(Guid Id)
        {
            using var context = new ApiContext();

            var medico = await context.Medicos.Include(x => x.FotoPerfil).Where(x => x.Id == Id).FirstOrDefaultAsync();

            if (medico == null)
                throw new Exception("Usuario não encontrado!");

            if (medico.FotoPerfil != null)
            {
                var foto = await context.Arquivos.Where(x => x.Id == medico.FotoPerfil.Id).FirstOrDefaultAsync();

                context.Arquivos.Remove(foto);
            }

            await context.SaveChangesAsync();
        }
    }
}

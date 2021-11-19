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
    public class EnfermeiroApp
    {
        public async Task<Enfermeiro> AcessarSistema(string usuario, string senha)
        {
            using var context = new ApiContext();

            Enfermeiro enfermeiro;

            if(usuario.Contains("@"))
                enfermeiro = await context.Enfermeiros
                    .Include(x => x.FotoPerfil)
                    .Include(x => x.Clinica)
                    .Where(x => x.Email.ToLower() == usuario.ToLower())
                    .FirstOrDefaultAsync();
            else
                enfermeiro = await context.Enfermeiros
                    .Include(x => x.FotoPerfil)
                    .Include(x => x.Clinica)
                    .Where(x => x.COREM.Trim().ToLower().Replace(".", "").Replace("-", "") == usuario.Trim().ToLower().Replace(".", "").Replace("-", ""))
                    .FirstOrDefaultAsync();

            if (enfermeiro != null)
            {
                if (enfermeiro.Senha == senha)
                    return enfermeiro;
                else
                    throw new Exception("Senha incorreta!");
            }
            else
                throw new Exception("Enfermeiro não encontrado!");
        }

        public async Task Cadastrar(Enfermeiro enfermeiro, Guid idClinica)
        {
            using var context = new ApiContext();

            enfermeiro.Clinica = await context.Clinicas.Where(x => x.Id == idClinica).FirstOrDefaultAsync();

            await context.Enfermeiros.AddAsync(enfermeiro);
            await context.SaveChangesAsync();
        }

        public async Task<Enfermeiro> AlterarFotoPerfil(Guid Id, Arquivo Foto)
        {
            using var context = new ApiContext();

            var enfermeiro = await context.Enfermeiros
                .Include(x => x.FotoPerfil)
                .Include(x => x.Clinica)
                .Where(x => x.Id == Id).FirstOrDefaultAsync();

            if (enfermeiro == null)
                throw new Exception("Usuario não encontrado!");

            if(enfermeiro.FotoPerfil == null)
            {
                enfermeiro.FotoPerfil = Foto;
                context.Enfermeiros.Update(enfermeiro);
            }
            else
            {
                var foto = await context.Arquivos.Where(x => x.Id == enfermeiro.FotoPerfil.Id).FirstOrDefaultAsync();

                foto.Nome = Foto.Nome;
                foto.Tipo = Foto.Tipo;
                foto.Binario = Foto.Binario;
                foto.DataAtualizacao = DateTime.Now;

                enfermeiro.FotoPerfil = foto;

                context.Arquivos.Update(foto);
            }
            
            await context.SaveChangesAsync();

            return enfermeiro;
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

            var enfermeiro = await context.Enfermeiros.Include(x => x.FotoPerfil).Where(x => x.Id == Id).FirstOrDefaultAsync();

            if (enfermeiro == null)
                throw new Exception("Usuario não encontrado!");

            if (enfermeiro.FotoPerfil != null)
            {
                var foto = await context.Arquivos.Where(x => x.Id == enfermeiro.FotoPerfil.Id).FirstOrDefaultAsync();

                context.Arquivos.Remove(foto);
            }

            await context.SaveChangesAsync();
        }
    }
}

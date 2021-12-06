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
                        .ThenInclude(x => x.Endereco)
                    .Where(x => x.Email.ToLower() == usuario.ToLower())
                    .FirstOrDefaultAsync();
            else
                enfermeiro = await context.Enfermeiros
                    .Include(x => x.FotoPerfil)
                    .Include(x => x.Clinica)
                        .ThenInclude(x => x.Endereco)
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

        public async Task Update(Enfermeiro enfermeiro)
        {
            using var context = new ApiContext();

            var oldEnfermeiro = await context.Enfermeiros.Where(x => x.Id == enfermeiro.Id).FirstOrDefaultAsync();

            if (oldEnfermeiro == null) throw new Exception("Enfermeiro não encontrado!");

            oldEnfermeiro.Nome = enfermeiro.Nome;
            oldEnfermeiro.COREM = enfermeiro.COREM;
            oldEnfermeiro.Email = enfermeiro.Email;
            oldEnfermeiro.DataAtualizacao = DateTime.Now;

            context.Enfermeiros.Update(oldEnfermeiro);
            await context.SaveChangesAsync();
        }

        public async Task<Enfermeiro> ResetSenha(Guid id)
        {
            using var context = new ApiContext();

            var enfermeiro = await context.Enfermeiros.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (enfermeiro == null) throw new Exception("Enfermeiro não encontrado!");

            enfermeiro.Senha = new Random().Next(100000, 999999).ToString();

            context.Enfermeiros.Update(enfermeiro);
            await context.SaveChangesAsync();

            return enfermeiro;
        }

        public async Task<Enfermeiro> TrocarSenha(Guid id, string senhaAtual, string novaSenha)
        {
            using var context = new ApiContext();

            var enfermeiro = await context.Enfermeiros.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (enfermeiro == null) throw new Exception("Enfermeiro(a) não encontrado!");

            if (enfermeiro.Senha != senhaAtual) throw new Exception("Senha incorreta!");

            enfermeiro.Senha = novaSenha;
            enfermeiro.DataAtualizacao = DateTime.Now;

            context.Enfermeiros.Update(enfermeiro);
            await context.SaveChangesAsync();

            return enfermeiro;
        }

        public async Task DeleteAgente(Guid id)
        {
            using var context = new ApiContext();

            var enfermeiro = await context.Enfermeiros.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (enfermeiro == null) throw new Exception("Enfermeiro não encontrado!");

            context.Enfermeiros.Remove(enfermeiro);
            await context.SaveChangesAsync();
        }

        public async Task<List<object>> GetHistoricoAtendimento(Guid id)
        {
            using var context = new ApiContext();

            var enfermeiro = await context.Enfermeiros.AsNoTracking()
                .Include(x => x.Consultas)
                    .ThenInclude(x => x.Tipo)
                .Where(x => x.Id == id).FirstOrDefaultAsync();

            if (enfermeiro == null) throw new Exception("Enfermeiro não localizado!");

            List<object> response = new List<object>();

            foreach (var consulta in enfermeiro.Consultas)
            {
                var paciente = await context.Pacientes.AsNoTracking().Where(x => x.Consultas.Contains(consulta)).FirstOrDefaultAsync();

                if (paciente != null)
                {
                    response.Add(new
                    {
                        paciente,
                        consulta
                    });
                }
            }

            return response;
        }

        public async Task<List<object>> GetHistoricoPrescricoes(Guid id)
        {
            using var context = new ApiContext();

            var enfermeiro = await context.Enfermeiros.AsNoTracking().Include(x => x.Medicamentos).Where(x => x.Id == id).FirstOrDefaultAsync();

            if (enfermeiro == null) throw new Exception("Enfermeiro não localizado!");

            List<object> response = new List<object>();

            foreach (var medicamento in enfermeiro.Medicamentos)
            {
                var paciente = await context.Pacientes.AsNoTracking().Where(x => x.Medicamentos.Contains(medicamento)).FirstOrDefaultAsync();

                if (paciente != null)
                {
                    response.Add(new
                    {
                        paciente,
                        medicamento
                    });
                }
            }

            return response;
        }

        public async Task<Enfermeiro> GetEnfermeiroById(Guid id)
        {
            using var context = new ApiContext();

            var enfermeiro = await context.Enfermeiros.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (enfermeiro == null) throw new Exception("Enfemeiro não localizado!");

            return enfermeiro;
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

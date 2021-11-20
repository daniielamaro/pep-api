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
    public class AgenteAdmApp
    {
        public async Task<AgenteAdministrativo> AcessarSistema(string usuario, string senha)
        {
            using var context = new ApiContext();

            AgenteAdministrativo agente;

            if(usuario.Contains("@"))
                agente = await context.AgentesAdministrativos
                    .Include(x => x.FotoPerfil)
                    .Include(x => x.Clinica)
                    .Where(x => x.Email.ToLower() == usuario.ToLower())
                    .FirstOrDefaultAsync();
            else
                agente = await context.AgentesAdministrativos
                    .Include(x => x.FotoPerfil)
                    .Include(x => x.Clinica)
                    .Where(x => x.CPF.Trim().ToLower().Replace(".", "").Replace("-", "") == usuario.Trim().ToLower().Replace(".", "").Replace("-", ""))
                    .FirstOrDefaultAsync();

            if (agente != null)
            {
                if (agente.Senha == senha)
                    return agente;
                else
                    throw new Exception("Senha incorreta!");
            }
            else
                throw new Exception("Agente administrativo não encontrado!");
        }

        public async Task<object> GetListaFuncionario(Guid id)
        {
            using var context = new ApiContext();

            var medicos = await context.Medicos.AsNoTracking().Where(x => x.Clinica.Id == id).ToListAsync();
            var enfermeiros = await context.Enfermeiros.AsNoTracking().Where(x => x.Clinica.Id == id).ToListAsync();
            var agentes = await context.AgentesAdministrativos.AsNoTracking().Where(x => x.Clinica.Id == id).ToListAsync();

            List<object> retorno = new List<object>();

            foreach (var medico in medicos)
            {
                retorno.Add(new
                {
                    medico.Id,
                    medico.Nome,
                    medico.Email,
                    Funcao = "medico"
                });
            }

            foreach (var enfermeiro in enfermeiros)
            {
                retorno.Add(new
                {
                    enfermeiro.Id,
                    enfermeiro.Nome,
                    enfermeiro.Email,
                    Funcao = "enfermeiro"
                });
            }

            foreach (var agente in agentes)
            {
                retorno.Add(new
                {
                    agente.Id,
                    agente.Nome,
                    agente.Email,
                    Funcao = "agente"
                });
            }

            return retorno;
        }

        public async Task<AgenteAdministrativo> Cadastrar(AgenteAdministrativo agente, Guid idClinica)
        {
            using var context = new ApiContext();

            agente.Clinica = await context.Clinicas.Where(x => x.Id == idClinica).FirstOrDefaultAsync();

            await context.AgentesAdministrativos.AddAsync(agente);
            await context.SaveChangesAsync();

            return agente;
        }

        public async Task Update(AgenteAdministrativo agente)
        {
            using var context = new ApiContext();

            var oldAgente = await context.AgentesAdministrativos.Where(x => x.Id == agente.Id).FirstOrDefaultAsync();

            if (oldAgente == null) throw new Exception("Agente administrativo não encontrado!");

            oldAgente.Nome = agente.Nome;
            oldAgente.CPF = agente.CPF;
            oldAgente.Email = agente.Email;
            oldAgente.DataAtualizacao = DateTime.Now;

            context.AgentesAdministrativos.Update(oldAgente);
            await context.SaveChangesAsync();
        }

        public async Task<AgenteAdministrativo> ResetSenha(Guid id)
        {
            using var context = new ApiContext();

            var oldAgente = await context.AgentesAdministrativos.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (oldAgente == null) throw new Exception("Agente administrativo não encontrado!");

            oldAgente.Senha = new Random().Next(100000, 999999).ToString();

            context.AgentesAdministrativos.Update(oldAgente);
            await context.SaveChangesAsync();

            return oldAgente;
        }

        public async Task DeleteAgente(Guid id)
        {
            using var context = new ApiContext();

            var oldAgente = await context.AgentesAdministrativos.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (oldAgente == null) throw new Exception("Agente administrativo não encontrado!");

            context.AgentesAdministrativos.Remove(oldAgente);
            await context.SaveChangesAsync();
        }

        public async Task<AgenteAdministrativo> GetAgenteById(Guid id)
        {
            using var context = new ApiContext();

            var agente = await context.AgentesAdministrativos.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (agente == null) throw new Exception("Agente administrativo não localizado!");

            return agente;
        }

        public async Task<AgenteAdministrativo> AlterarFotoPerfil(Guid Id, Arquivo Foto)
        {
            using var context = new ApiContext();

            var agente = await context.AgentesAdministrativos
                .Include(x => x.FotoPerfil)
                .Include(x => x.Clinica)
                .Where(x => x.Id == Id).FirstOrDefaultAsync();

            if (agente == null)
                throw new Exception("Usuario não encontrado!");

            if(agente.FotoPerfil == null)
            {
                agente.FotoPerfil = Foto;
                context.AgentesAdministrativos.Update(agente);
            }
            else
            {
                var foto = await context.Arquivos.Where(x => x.Id == agente.FotoPerfil.Id).FirstOrDefaultAsync();

                foto.Nome = Foto.Nome;
                foto.Tipo = Foto.Tipo;
                foto.Binario = Foto.Binario;
                foto.DataAtualizacao = DateTime.Now;

                agente.FotoPerfil = foto;

                context.Arquivos.Update(foto);
            }
            
            await context.SaveChangesAsync();

            return agente;
        }

        public async Task DeletarFotoPerfil(Guid Id)
        {
            using var context = new ApiContext();

            var agente = await context.AgentesAdministrativos.Include(x => x.FotoPerfil).Where(x => x.Id == Id).FirstOrDefaultAsync();

            if (agente == null)
                throw new Exception("Usuario não encontrado!");

            if (agente.FotoPerfil != null)
            {
                var foto = await context.Arquivos.Where(x => x.Id == agente.FotoPerfil.Id).FirstOrDefaultAsync();

                context.Arquivos.Remove(foto);
            }

            await context.SaveChangesAsync();
        }
    }
}

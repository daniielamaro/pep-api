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

        public async Task Update(Medico medico)
        {
            using var context = new ApiContext();

            var oldMedico = await context.Medicos.Where(x => x.Id == medico.Id).FirstOrDefaultAsync();

            if (oldMedico == null) throw new Exception("Medico não encontrado!");

            oldMedico.Nome = medico.Nome;
            oldMedico.CRM = medico.CRM;
            oldMedico.Email = medico.Email;
            oldMedico.DataAtualizacao = DateTime.Now;

            context.Medicos.Update(oldMedico);
            await context.SaveChangesAsync();
        }

        public async Task<Medico> ResetSenha(Guid id)
        {
            using var context = new ApiContext();

            var medico = await context.Medicos.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (medico == null) throw new Exception("Medico não encontrado!");

            medico.Senha = new Random().Next(100000, 999999).ToString();

            context.Medicos.Update(medico);
            await context.SaveChangesAsync();

            return medico;
        }

        public async Task DeleteAgente(Guid id)
        {
            using var context = new ApiContext();

            var medico = await context.Medicos.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (medico == null) throw new Exception("Medico não encontrado!");

            context.Medicos.Remove(medico);
            await context.SaveChangesAsync();
        }

        public async Task<Medico> GetMedicoById(Guid id)
        {
            using var context = new ApiContext();

            var medico = await context.Medicos.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (medico == null) throw new Exception("Medico não localizado!");

            return medico;
        }

        public async Task<List<object>> GetHistoricoAtendimento(Guid id)
        {
            using var context = new ApiContext();

            var medico = await context.Medicos.AsNoTracking().Include(x => x.Consultas).Where(x => x.Id == id).FirstOrDefaultAsync();

            if (medico == null) throw new Exception("Medico não localizado!");

            List<object> response = new List<object>();

            foreach (var consulta in medico.Consultas)
            {
                var paciente = await context.Pacientes.AsNoTracking().Where(x => x.Consultas.Contains(consulta)).FirstOrDefaultAsync();

                if(paciente != null)
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

            var medico = await context.Medicos.AsNoTracking().Include(x => x.Medicamentos).Where(x => x.Id == id).FirstOrDefaultAsync();

            if (medico == null) throw new Exception("Medico não localizado!");

            List<object> response = new List<object>();

            foreach (var medicamento in medico.Medicamentos)
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

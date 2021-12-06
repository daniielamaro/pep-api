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

            if(paciente.FotoPerfil == null)
            {
                paciente.FotoPerfil = Foto;
                context.Pacientes.Update(paciente);
            }
            else
            {
                var foto = await context.Arquivos.Where(x => x.Id == paciente.FotoPerfil.Id).FirstOrDefaultAsync();

                foto.Nome = Foto.Nome;
                foto.Tipo = Foto.Tipo;
                foto.Binario = Foto.Binario;
                foto.DataAtualizacao = DateTime.Now;

                paciente.FotoPerfil = foto;

                context.Arquivos.Update(foto);
            }
            
            await context.SaveChangesAsync();

            return paciente;
        }

        public async Task<object> UpdatePaciente(string email, string endereco, string senha, Guid id)
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
            
        }

        public async Task<object> GetAllPacientes()
        {
            using var context = new ApiContext();

            var paciente = await context.Pacientes.AsNoTracking().ToListAsync();

            return paciente;
        }

        public async Task<object> GetPacienteById(Guid id)
        {
            using var context = new ApiContext();

            var paciente = await context.Pacientes.AsNoTracking().Include(x => x.FotoPerfil).Where(x => x.Id == id).FirstOrDefaultAsync();

            return new
            {
                paciente.Id,
                paciente.FotoPerfil,
                paciente.Nome,
                paciente.Cpf,
                Idade = DateTime.Now >= paciente.DataNasc ? (DateTime.Now.Year - paciente.DataNasc.Year) : (DateTime.Now.Year - paciente.DataNasc.Year) -1
            };
        }

        public async Task<object> GetPacienteByFilter(string nome, string cpf, string dataNasc)
        {
            using var context = new ApiContext();

            var paciente = await context.Pacientes
                .AsNoTracking()
                .Include(x => x.FotoPerfil)
                .Where(x => (!string.IsNullOrWhiteSpace(nome) ? x.Nome.ToUpper().Contains(nome.ToUpper()) : true) &&
                            (!string.IsNullOrWhiteSpace(cpf) ? x.Cpf.Replace(".","").Replace("-","") == cpf.Replace(".", "").Replace("-", "") : true) &&
                            (!string.IsNullOrWhiteSpace(dataNasc) ? x.DataNasc.Date == DateTime.Parse(dataNasc).Date : true))
                .ToListAsync();

            return paciente.Select(x => new { x.Id, x.Nome, x.FotoPerfil, x.Cpf }).ToList();
        }

        public async Task DeletarFotoPerfil(Guid Id)
        {
            using var context = new ApiContext();

            var paciente = await context.Pacientes.Include(x => x.FotoPerfil).Where(x => x.Id == Id).FirstOrDefaultAsync();

            if (paciente == null)
                throw new Exception("Usuario não encontrado!");

            if (paciente.FotoPerfil != null)
            {
                var foto = await context.Arquivos.Where(x => x.Id == paciente.FotoPerfil.Id).FirstOrDefaultAsync();

                context.Arquivos.Remove(foto);
            }

            await context.SaveChangesAsync();
        }
    }
}

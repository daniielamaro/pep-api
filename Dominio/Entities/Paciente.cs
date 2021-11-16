using System;
using System.Collections.Generic;

namespace Dominio.Entities
{
    public class Paciente : Entidade
    {
        public string Nome { get; set; }
        public Arquivo FotoPerfil { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Email { get; set; }
        public DateTime DataNasc { get; set; }
        public string Endereco { get; set; }
        public string Senha { get; set; }
        public List<Medicamento> Medicamentos { get; set; }
        public List<Exame> Exames { get; set; }
        public List<Consulta> Consultas { get; set; }
    }
}

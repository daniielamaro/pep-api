using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entities
{
    public class Medico : Entidade
    {
        public string Nome { get; set; }
        public Arquivo FotoPerfil { get; set; }
        public string CRM { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public Clinica Clinica { get; set; }
        public List<Medicamento> Medicamentos { get; set; }
        public List<Exame> Exames { get; set; }
        public List<Consulta> Consultas { get; set; }
    }
}

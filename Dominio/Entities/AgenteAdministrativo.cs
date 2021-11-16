using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entities
{
    public class AgenteAdministrativo : Entidade
    {
        public string Nome { get; set; }
        public Arquivo FotoPerfil { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public Clinica Clinica { get; set; }
    }
}

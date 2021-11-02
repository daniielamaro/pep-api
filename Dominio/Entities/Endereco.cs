using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entities
{
    public class Endereco : Entidade
    {
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string CEP { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string UF { get; set; }
    }
}

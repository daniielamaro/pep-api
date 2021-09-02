using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.RequestModels
{
    public class RequestCriarPaciente
    {
        public string Nome { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public string DataNasc { get; set; }
        public string Email { get; set; }
        public string Endereco { get; set; }
        public string Senha { get; set; }
    }
}

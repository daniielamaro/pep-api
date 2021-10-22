using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.RequestModels
{
    public class RequestUpdatePaciente
    {
        public string Email { get; set; }
        public string Endereco { get; set; }
        public string Senha { get; set; }
        public Guid Id { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.RequestModels
{
    public class RequestCriarAgente
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public Guid IdClinica { get; set; }
    }
}

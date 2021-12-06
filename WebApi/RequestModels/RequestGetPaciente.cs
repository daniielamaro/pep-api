using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.RequestModels
{
    public class RequestGetPaciente
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string DataNasc { get; set; }
    }
}

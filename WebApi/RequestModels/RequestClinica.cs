using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.RequestModels
{
    public class RequestClinica
    {
        public string NomeClinica { get; set; }
        public string Endereco { get; set; }
        public Guid IdConsulta { get; set; }
        public Guid IdTipoExame { get; set; }

    }
}

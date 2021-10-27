using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.RequestModels
{
    public class RequestCriarConsulta
    {
        public Guid IdPaciente { get; set; }
        public Guid IdTipoConsulta { get; set; }
        public string DiaRealizacao { get; set; }
        public bool Publico { get; set; }
        public string Resumo { get; set; }
        public string Observacoes { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.RequestModels
{
    public class RequestExames
    {
        public Guid Id { get; set; }
        public Guid TipoId { get; set; }
        public bool Publico { get; set; }
        public string Observacao { get; set; }
    }
}

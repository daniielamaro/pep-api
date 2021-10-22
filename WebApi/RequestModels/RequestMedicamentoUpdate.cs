using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.RequestModels
{
    public class RequestMedicamentoUpdate
    {
        public string Nome { get; set; }
        public string Quantidade { get; set; }
        public string Intervalo { get; set; }
        public Guid Id { get; set; }
        public bool UsoContinuo { get; set; }
    }
}

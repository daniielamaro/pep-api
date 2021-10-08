using Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.RequestModels
{
    public class RequestCriarMedicamento
    {
        public Guid IdPaciente { get; set; }
        public string Nome { get; set; }
        public string Quantidade { get; set; }
        public string Intervalo { get; set; }
        public DateTime? DataTermino { get; set; }
        public bool UsoContinuo { get; set; }
    }
}

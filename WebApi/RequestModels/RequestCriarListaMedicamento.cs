using Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.RequestModels
{
    public class RequestCriarListaMedicamento
    {
        public Guid IdPaciente { get; set; }
        public List<RequestCriarMedicamento> Medicamentos { get; set; }
    }
}

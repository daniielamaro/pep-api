using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entities
{
    public class ClinicaConsultaTipo
    {
        public Guid ConsultaId { get; set; }
        public ConsultaTipo ConsultaTipo { get; set; }
        public Guid ClinicaId { get; set; }
        public Clinica Clinica { get; set; }
    }
}

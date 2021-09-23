using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entities
{
    public class ClinicaConsultaTipo: Entidade
    {
        public ConsultaTipo ConsultaTipo { get; set; }
        public Clinica Clinicas { get; set; }
    }
}

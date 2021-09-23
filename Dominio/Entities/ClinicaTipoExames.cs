using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entities
{
    public class ClinicaTipoExames: Entidade
    {
        public ExameTipo ExameTipo { get; set; }
        public Clinica Clinica { get; set; }
    }
}

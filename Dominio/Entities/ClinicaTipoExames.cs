using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entities
{
    public class ClinicaTipoExames: Entidade
    {
        public Guid ExameId { get; set; }
        public ExameTipo ExameTipo { get; set; }
        public Guid ClinicaId { get; set; }
        public Clinica Clinica { get; set; }
    }
}

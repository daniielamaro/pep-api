using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entities
{
    public class Clinica : Entidade
    {
        public string NomeClinica { get; set; }
        public Endereco Endereco { get; set; }
        public List<ClinicaConsultaTipo> ConsultaTipos { get; set; }
        public List<ClinicaTipoExames> ExameTipos { get; set; }
    }
}

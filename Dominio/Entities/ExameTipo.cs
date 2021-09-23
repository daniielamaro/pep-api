using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entities
{
    public class ExameTipo: Entidade
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public List<ClinicaTipoExames> ClinicasExame { get; set; }
    }
}

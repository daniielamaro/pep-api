using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entities
{
    public class ConsultaTipo : Entidade
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public List<ClinicaConsultaTipo> Clinicas { get; set; }
    

}
}

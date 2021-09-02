using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entities
{
    public class Consulta : Entidade
    {
        public ConsultaTipo Tipo { get; set; }
        public string Resumo { get; set; }
        public string Observacoes { get; set; }
    }
}

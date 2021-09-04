using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entities
{
    public class Exame : Entidade
    {
        public Arquivo Resultado { get; set; }
        public ExameTipo Tipo { get; set; }
        public bool Publico { get; set; }
        public string Observacoes { get; set; }
    }
}

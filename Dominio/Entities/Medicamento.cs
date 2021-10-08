using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entities
{
    public class Medicamento : Entidade
    {
        public string Nome { get; set; }
        public string Quantidade { get; set; }
        public string Intervalo { get; set; }
        public DateTime? DataTermino { get; set; }
        public bool UsoContinuo { get; set; }
    }
}

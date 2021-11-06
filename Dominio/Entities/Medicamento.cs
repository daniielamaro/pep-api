using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entities
{
    public class Medicamento : Entidade
    {
        public string Nome { get; set; }
        public int NumQuantidade { get; set; }
        public string TipoQuantidade { get; set; }
        public string OutraQuantidade { get; set; }
        public int NumIntervalo { get; set; }
        public string TipoIntervalo { get; set; }
        public string OutroIntervalo { get; set; }
        public bool Publico { get; set; }
        public bool InputByUser { get; set; }
        public int TipoCadastro { get; set; }
        public Arquivo Receita { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataTermino { get; set; }
        public bool UsoContinuo { get; set; }
    }
}

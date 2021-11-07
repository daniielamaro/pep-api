using Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.RequestModels
{
    public class RequestEditarMedicamento
    {
        public Guid IdMedicamento { get; set; }
        public string Nome { get; set; }
        public int NumQuantidade { get; set; }
        public string TipoQuantidade { get; set; }
        public string OutraQuantidade { get; set; }
        public int NumIntervalo { get; set; }
        public string TipoIntervalo { get; set; }
        public string OutroIntervalo { get; set; }
        public bool Publico { get; set; }
        public int TipoCadastro { get; set; }
        public Arquivo Receita { get; set; }
        public string DataInicio { get; set; }
        public string DataTermino { get; set; }
        public bool UsoContinuo { get; set; }
    }
}

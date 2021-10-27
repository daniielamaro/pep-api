using Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.RequestModels
{
    public class RequestCriarExame
    {
        public RequestCriarArquivo Arquivo { get; set; }
        public Guid IdPaciente { get; set; }
        public Guid IdTipoExame { get; set; }
        public string DataRealizacao { get; set; }
        public bool Publico { get; set; }
        public string Observacoes { get; set; }
            
    }
}

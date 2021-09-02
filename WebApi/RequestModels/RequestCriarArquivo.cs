using Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.RequestModels
{
    public class RequestCriarArquivo
    {
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public byte[] Binario { get; set; }
    }
}

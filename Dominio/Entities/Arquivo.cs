using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entities
{
    public class Arquivo : Entidade
    {
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public byte[] Binario { get; set; }
    }
}

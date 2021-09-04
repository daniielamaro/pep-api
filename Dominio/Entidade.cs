using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Entidade
    {
        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
    }
}

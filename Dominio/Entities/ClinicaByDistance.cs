using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entities
{
    public class ClinicaByDistance
    {
        public string Origem { get; set; }
        public Clinica Clinica { get; set; }
        public int Distancia { get; set; }
        public int Duracao { get; set; }
    }
}

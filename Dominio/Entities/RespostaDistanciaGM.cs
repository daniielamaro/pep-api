using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entities
{
    public class RespostaDistanciaGM
    {
        public List<string> Origin_addresses { get; set; }
        public List<RespostaDistanciaElementGM> Rows { get; set; }
    }

    public class RespostaDistanciaElementGM
    {
        public List<RespostaDistanciaElementItemGM> Elements { get; set; }
    }

    public class RespostaDistanciaElementItemGM
    {
        public RespostaDistanciaElementItemDistanceGM Distance { get; set; }
        public RespostaDistanciaElementItemDurationGM Duration { get; set; }
    }

    public class RespostaDistanciaElementItemDistanceGM
    {
        public string Text { get; set; }
        public int Value { get; set; }
    }

    public class RespostaDistanciaElementItemDurationGM
    {
        public string Text { get; set; }
        public int Value { get; set; }
    }
}

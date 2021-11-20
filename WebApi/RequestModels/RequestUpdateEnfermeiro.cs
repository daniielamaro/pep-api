using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.RequestModels
{
    public class RequestUpdateEnfermeiro
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string COREM { get; set; }
        public string Email { get; set; }
    }
}

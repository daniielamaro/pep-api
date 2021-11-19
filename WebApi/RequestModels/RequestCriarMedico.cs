using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.RequestModels
{
    public class RequestCriarMedico
    {
        public string Nome { get; set; }
        public string CRM { get; set; }
        public string Email { get; set; }
        public Guid IdClinica { get; set; }
    }
}

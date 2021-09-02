using Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.RequestModels
{
    public class RequestSetFotoPerfil
    {
        public RequestCriarArquivo Foto { get; set; }
        public Guid Id { get; set; }
    }
}

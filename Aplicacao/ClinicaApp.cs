using Dominio.Entities;
using Infraestrutura;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Aplicacao
{
    public class ClinicaApp
    {
        public async Task CadastrarClinica(Clinica clinica)
        {
            using var context = new ApiContext();

            var newClinica = new Clinica
            {
                NomeClinica = clinica.NomeClinica,
                Endereco = clinica.Endereco,
                ConsultaTipos = new List<ClinicaConsultaTipo>(),
                ExameTipos = new List<ClinicaTipoExames>(),
                DataCriacao = DateTime.Now
            };

            context.Clinicas.Add(newClinica);

            await context.SaveChangesAsync();
        }

        public async Task CadastroTipoDeConsulta(Guid IdClinica, Guid IdConsulta)
        {
            using var context = new ApiContext();

            var Clinica = await context.Clinicas.Where(x => x.Id == IdClinica).FirstOrDefaultAsync();

            var Consulta = await context.TiposConsultas.Where(x => x.Id == IdConsulta).FirstOrDefaultAsync();
           


            if (Clinica == null)
                throw new Exception("Clinica não encontrada");
            else if (Consulta == null)
                throw new Exception("Tipo de Consulta não encontrado");


            var newConsulta = new ClinicaConsultaTipo
            {
                ClinicaId = Clinica.Id,
                ConsultaId = Consulta.Id
            };


            context.ClinicaConsultaTipos.Add(newConsulta);

            await context.SaveChangesAsync();
        }


        public async Task CadastroTipoDeExame(Guid IdClinica, Guid IdExame)
        {
            using var context = new ApiContext();

            var Clinica = await context.Clinicas.Where(x => x.Id == IdClinica).FirstOrDefaultAsync();

            var Exame = await context.TiposExames.Where(x => x.Id == IdExame).FirstOrDefaultAsync();

            if (Clinica == null)
                throw new Exception("Clinica não encontrada");
            else if (Exame == null)
                throw new Exception("Tipo de Exame não encontrado");

            var NewExame = new ClinicaTipoExames
            {
                ClinicaId = Clinica.Id,
                ExameId = Exame.Id
            };

            context.ClinicaExameTipos.Add(NewExame);

            await context.SaveChangesAsync();

        }

        public async Task<List<ClinicaByDistance>> GetClinica(string key, string coordenadaOrigem)
        {
            using var context = new ApiContext();

            var clinicas = await context.Clinicas.Include(x => x.Endereco).AsNoTracking().ToListAsync();

            List<ClinicaByDistance> resposta = new List<ClinicaByDistance>();

            string endDestinos = "";

            for (int i = 0; i < clinicas.Count; i++)
            {
                var endereco = clinicas[i].Endereco;

                endDestinos += endereco.Logradouro + 
                    (string.IsNullOrWhiteSpace(endereco.Numero) ? ", S/N" : ", " + endereco.Numero) + 
                    " - " +
                    endereco.Bairro + ", " +
                    endereco.Localidade + " - " + endereco.UF + ", Brazil";

                if (i + 1 < clinicas.Count) endDestinos += "|";
            }

            using HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync("https://maps.googleapis.com/maps/api/distancematrix/json?mode=walking&language=pt-BR&destinations=" + endDestinos+ "&origins="+coordenadaOrigem+"&key="+key);
            if (response.IsSuccessStatusCode)
            {
                var respostaStr = await response.Content.ReadAsStringAsync();
                var respostaObj = JsonConvert.DeserializeObject<RespostaDistanciaGM>(respostaStr);

                for (int i = 0; i < clinicas.Count; i++)
                {
                    resposta.Add(new ClinicaByDistance
                    {
                        Origem = respostaObj.Origin_addresses[0],
                        Clinica = clinicas[i],
                        Distancia = respostaObj.Rows[0].Elements[i].Distance.Value,
                        Duracao = respostaObj.Rows[0].Elements[i].Duration.Value
                    });
                }
            }

            return resposta.OrderBy(x => x.Distancia).ToList();
        }
    }
}

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
                DataCriacao = DateTime.Now
            };

            context.Clinicas.Add(newClinica);

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

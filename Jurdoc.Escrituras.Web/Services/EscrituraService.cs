using Jurdoc.Escrituras.Web.Interfaces;
using Jurdoc.Escrituras.Web.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Jurdoc.Escrituras.Web.Services
{
    public class EscrituraService : IEscrituraService
    {
        private readonly string _connectionString;
        public EscrituraService(IConfiguration _configuratio)
        {
            _connectionString = _configuratio.GetConnectionString("EscriturasApiConnection");
        }

        IEnumerable<Escritura> IEscrituraService.GetEscrituras()
        {
            var escrituras = new List<Escritura>();

            using (var client = new HttpClient())
            {
                var uri = new Uri(_connectionString);

                var response = client.GetAsync(uri).Result;

                if (!response.IsSuccessStatusCode)
                    throw new Exception(response.ToString());

                var responseContent = response.Content;
                var responseString = responseContent.ReadAsStringAsync().Result;

                dynamic authors = JArray.Parse(responseString) as JArray;

                foreach (var obj in authors)
                {
                    Escritura dto = obj.ToObject<Escritura>();

                    escrituras.Add(dto);
                }
            }

            return escrituras;
        }

        Escritura IEscrituraService.GetEscritura(int ID_ESCRITURA)
        {
            Escritura escritura;

            using (var client = new HttpClient())
            {
                var uri = new Uri(_connectionString + "/" + ID_ESCRITURA);
                HttpResponseMessage getResponseMessage = client.GetAsync(uri).Result;

                if (!getResponseMessage.IsSuccessStatusCode)
                    throw new Exception(getResponseMessage.ToString());

                var responsemessage = getResponseMessage.Content.ReadAsStringAsync().Result;

                dynamic project = JsonConvert.DeserializeObject(responsemessage);

                escritura = project.ToObject<Escritura>();
            }

            return escritura;
        }

        void IEscrituraService.AddEscritura(Escritura escritura)
        {
            using (var client = new HttpClient { BaseAddress = new Uri(_connectionString) })
            {
                string serailizeddto = JsonConvert.SerializeObject(escritura);

                var inputMessage = new HttpRequestMessage
                {
                    Content = new StringContent(serailizeddto, Encoding.UTF8, "application/json")
                };

                inputMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage message =
                    client.PostAsync("/api/Escrituras", inputMessage.Content).Result;

                if (!message.IsSuccessStatusCode)
                    throw new Exception(message.ToString());
            }
        }

        void IEscrituraService.EditEscritura(Escritura escritura)
        {
            try
            {

            
            using (var client = new HttpClient { BaseAddress = new Uri(_connectionString) })
            {
                string serailizeddto = JsonConvert.SerializeObject(escritura);

                var inputMessage = new HttpRequestMessage
                {
                    Content = new StringContent(serailizeddto, Encoding.UTF8, "application/json")
                };

                inputMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage message =
                    client.PutAsync("/api/Escrituras", inputMessage.Content).Result;

                if (!message.IsSuccessStatusCode)
                    throw new Exception(message.ToString());
            }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        void IEscrituraService.DeleteEscritura(Escritura escritura)
        {
            throw new NotImplementedException();
        }
    }
}

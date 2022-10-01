using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

using Newtonsoft.Json;
using System.Runtime.Remoting;
using Newtonsoft.Json.Linq;

namespace Negocio
{
    public class NBecas
    {
        List<Becas> _ListBecas = new List<Becas>();

        public List<Becas> ConsultarTodos(string token)
        {
            try
            {
                using (var cliente = new HttpClient())
                {

                    cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    Task<HttpResponseMessage> respondeHTTP = cliente.GetAsync("https://localhost:44312/api/Becas");
                    respondeHTTP.Wait();
                    HttpResponseMessage responde = respondeHTTP.Result; 



                    if(responde.IsSuccessStatusCode)
                    {
                        Task<string> asincronoresponde = responde.Content.ReadAsStringAsync();
                        asincronoresponde.Wait();

                        string json = asincronoresponde.Result;

                        _ListBecas = JsonConvert.DeserializeObject<List<Becas>>(json);
                    }
                    else
                    {
                        throw new Exception($"Web Api. Repondio con error.{responde.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Api no respondio correctamente:{ex.Message}");
            }
            return _ListBecas;
        }
    }
}

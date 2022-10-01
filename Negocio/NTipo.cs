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
using System.Runtime.InteropServices;
using Newtonsoft.Json.Linq;

namespace Negocio
{
    public class NTipo
    {
        List<TipoBecas> _TipoBecas = new List<TipoBecas>();

        public List<TipoBecas> ConsultarTodos(string token)
        {
            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    Task<HttpResponseMessage> respondehttp = cliente.GetAsync("https://localhost:44312/api/TipoBecas");
                    respondehttp.Wait();

                    HttpResponseMessage leerHttp = respondehttp.Result;

                    if (leerHttp.IsSuccessStatusCode)
                    {
                        Task<string> asincronorespondehttp = leerHttp.Content.ReadAsStringAsync();
                        asincronorespondehttp.Wait();

                        string json = asincronorespondehttp.Result;

                        _TipoBecas = JsonConvert.DeserializeObject<List<TipoBecas>>(json);
                    }
                    else
                    {
                        throw new Exception($"Web Api. Repondio con error.{leerHttp.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Api no respondio correctamente:{ex.Message}");
            }


            return _TipoBecas;
        }



    }
}

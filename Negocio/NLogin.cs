using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;

namespace Negocio
{
    public class NLogin
    {


        public Respuesta Login(Login login)

        {
            Respuesta res = new Respuesta();
            try
            {
                using (var cliente = new HttpClient())
                {
                    HttpContent contenido = new StringContent(JsonConvert.SerializeObject(login),Encoding.UTF8);
                    contenido.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    Task<HttpResponseMessage> tarearespuesta = cliente.PostAsync("https://localhost:44312/api/User/login",contenido);
                    tarearespuesta.Wait();

                    var httprespondemessage  = tarearespuesta.Result;
                    if (httprespondemessage.IsSuccessStatusCode)
                    {
                        Task<string> tareastring = httprespondemessage.Content.ReadAsStringAsync();
                        tareastring.Wait();
                        string json = tareastring.Result;
                        res = JsonConvert.DeserializeObject<Respuesta>(json);   

                    }
                    else
                        throw new Exception($"Api error {httprespondemessage.StatusCode}");
                }
            }
            catch (Exception ex )
            {
                res.Mensaje = "Usuario o Contraseña incorrecta";
            }


            return res;
        }

    }
}

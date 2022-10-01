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
using System.Net.NetworkInformation;
using System.Configuration;

namespace Negocio
{
    public class NAlumnos
    {
        List<Alumnos> _ListAlumnos = new List<Alumnos>();
     
        Alumnos _alumnos = new Alumnos();   
        public List<Alumnos> ConsultarTodos(string token)
        {
            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    Task<HttpResponseMessage> respondeHTTP = cliente.GetAsync("https://localhost:44312/api/Alumnos");
                    respondeHTTP.Wait();

                    HttpResponseMessage leerHTTP = respondeHTTP.Result; 

                    if(leerHTTP.IsSuccessStatusCode)
                    {
                        Task<string> asincronoleerhttp = leerHTTP.Content.ReadAsStringAsync();
                        asincronoleerhttp.Wait();

                        string json = asincronoleerhttp.Result; 
                        _ListAlumnos = JsonConvert.DeserializeObject<List<Alumnos>>(json);
                    }
                    else
                    {
                        throw new Exception($"Web Api. Repondio con error.{leerHTTP.StatusCode}");
                    }
                }                                                                                                                                                                                                    
            }
            catch(Exception ex)
            {
                throw new Exception($"Api no respondio correctamente:{ex.Message}");

                
            }


            return _ListAlumnos;

        }

        public Alumnos ConsultarUno(int id)
        {
            Alumnos Alumnos = null;

            using (var client = new HttpClient())
            {
                var responseTask = client.GetAsync("https://localhost:44312/api/Alumnos" + $"/{id}");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    string json = readTask.Result;

                    Alumnos = JsonConvert.DeserializeObject<Alumnos>(json);

                }
                else
                {

                    throw new Exception($"WebAPI. Respondio con error.{result.StatusCode}");
                }
            }
            return Alumnos;
        }

        public Alumnos Agregar(Alumnos alumnos)
        {
            using (var client = new HttpClient())
            {
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(alumnos), Encoding.UTF8);

                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var postTask = client.PostAsync("https://localhost:44312/api/Alumnos", httpContent);

                postTask.Wait();

                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();

                    readTask.Wait();

                    string json = readTask.Result;

                    alumnos = JsonConvert.DeserializeObject<Alumnos>(json);
                }
                else
                {
                    throw new Exception($"WebAPI. Respondio con error.{result.StatusCode}");
                }
            }


            return alumnos;
        }
        public void Actualizar(Alumnos estatus)
        {

            using (var client = new HttpClient())
            {
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(estatus), Encoding.UTF8);

                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                var postTask = client.PutAsync("https://localhost:44312/api/Alumnos" + $"/{estatus.id}", httpContent);

                postTask.Wait();
                var result = postTask.Result;
                if (!result.IsSuccessStatusCode)
                {
                    throw new Exception($"WebAPI. Respondio con error.{result.StatusCode}");
                }

            }
        }
        public void Eliminar(int id)
        {
            using (var client = new HttpClient())
            {
                var responseTask = client.DeleteAsync("https://localhost:44312/api/Alumnos" + $"/{id}");


                responseTask.Wait();

                var result = responseTask.Result;

                if (!result.IsSuccessStatusCode)
                {
                    throw new Exception($"WebAPI. Respondio con error.{result.StatusCode}");
                }
            }
        }

    }
}

using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Configuration;
using Newtonsoft.Json;
using System.Runtime.Remoting;
using Newtonsoft.Json.Linq;
using System.Net.NetworkInformation;

namespace Negocio
{
    
    public  class NBecasAlumnos
    {

       
        public  List<BecasAlumnos> ConsultarTodos(string token)
        {
            List<BecasAlumnos> _listBecas = null;
            try
            {
                using (var cliente = new HttpClient())
                {

                    cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
                    Task<HttpResponseMessage> httpResponse = cliente.GetAsync("https://localhost:44312/api/BecasAlumnos");

                    httpResponse.Wait();

                    HttpResponseMessage httpResponseMessage = httpResponse.Result;

                    if(httpResponseMessage.IsSuccessStatusCode)
                    {
                        Task<string> asincronoleerhttp = httpResponseMessage.Content.ReadAsStringAsync();   
                        asincronoleerhttp.Wait();   

                        string json= asincronoleerhttp.Result;

                        _listBecas = JsonConvert.DeserializeObject<List<BecasAlumnos>>(json);
                    }
                    else
                    {
                       
                        throw new Exception($"Web Api. Repondio con error.{httpResponseMessage.StatusCode}");
                    }


                }
            }
            catch(Exception  ex)
            {
                 throw new Exception($"Api no respondio correctamente:{ex.Message}");
            }

            return _listBecas;
        }

        public CustomResponse Agregar(BecasAlumnos becasAlumnos, string token)
        {
            CustomResponse cr = new CustomResponse();
            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(becasAlumnos),Encoding.UTF8);
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var postTask = cliente.PostAsync("https://localhost:44312/api/BecasAlumnos", httpContent);
                    postTask.Wait();
                    var result = postTask.Result;
                    if (postTask.IsCompleted)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        string json = readTask.Result;

                        cr = JsonConvert.DeserializeObject<CustomResponse>(json);
                    }
                    else
                    {
                        throw new Exception($"{result.StatusCode}");
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }



            return cr;
        }


        public void Eliminar(int id, string token)
        {
            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var respondeTask = cliente.DeleteAsync("https://localhost:44312/api/BecasAlumnos" + $"/{id}");

                    respondeTask.Wait();
                    var resultado = respondeTask.Result;
                    if(!resultado.IsSuccessStatusCode)
                    {
                        throw new Exception ($"WebAPI. Respondio con error: {resultado.StatusCode}");
                    }
                    

                }
            }
            catch (Exception ex)
            {
                throw new Exception ($"WebAPI. Respondio con error: {ex.Message}");
            }

        }

        public BecasAlumnos Consultar(int id, string token)
        {
            BecasAlumnos ba = new BecasAlumnos();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var responseTask = client.GetAsync("https://localhost:44312/api/BecasAlumnos" + $"/{id}");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    string json = readTask.Result;

                    ba = JsonConvert.DeserializeObject<BecasAlumnos>(json);

                }
                else
                {

                    throw new Exception($"WebAPI. Respondio con error.{result.StatusCode}");
                }
            }
            return ba;
        }

        public void Actualizar(BecasAlumnos becasalumnos, string token)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(becasalumnos), Encoding.UTF8);

                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                var postTask = client.PutAsync("https://localhost:44312/api/BecasAlumnos" + $"/{becasalumnos.id}", httpContent);

                postTask.Wait();
                var result = postTask.Result;
                if (!result.IsSuccessStatusCode)
                {
                    throw new Exception($"WebAPI. Respondio con error.{result.StatusCode}");
                }

            }
        }
    }
    
}

using Newtonsoft.Json;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplicationPruebaDesarrolloCliente.Models;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace WebApplicationPruebaDesarrolloCliente.Service
{
    public class TipoClienteService : ControllerBase
    {
        private readonly string URLbase = "https://localhost:44361/api/";

        public async Task<IActionResult> CrearNuevoTipoCliente(TipoCliente nuevoTipoCliente)
        {
            var httpClient = new HttpClient();

            string URL = URLbase + "TipoClientes/";

            string jsonString = System.Text.Json.JsonSerializer.Serialize(nuevoTipoCliente);

            var content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");

           var response = await httpClient.PostAsync(URL, content);
            string apiResponse = await response.Content.ReadAsStringAsync();
           

            return NoContent();
          
        }

        public async Task<IActionResult> EliminarTipoCliente(int idTipoCliente)
        {
            var httpClient = new HttpClient();

            string URL = URLbase + "TipoClientes/" + idTipoCliente.ToString();

            var response = await httpClient.DeleteAsync(URL);
            string apiResponse = await response.Content.ReadAsStringAsync();


            return NoContent();

        }



        public async Task<List<TipoCliente>> ObtenerTodosLosTiposAsync()
        {
            List<TipoCliente> tipoClienteList = new List<TipoCliente>();


            try
            {
                var httpClient = new HttpClient();

                string URL = URLbase + "TipoClientes/";

                //httpClient.BaseAddress = new System.Uri(URLbase);

                var response = await httpClient.GetAsync(URL);
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (response.Content.Headers.Count() < 1)
                {
                    return tipoClienteList;
                }




                tipoClienteList = JsonConvert.DeserializeObject<List<TipoCliente>>(apiResponse).Select(
                    s => new TipoCliente
                    {
                        IdTipoCliente = s.IdTipoCliente,
                        CodigoTipoCliente = s.CodigoTipoCliente,
                        NombreTipoCliente = s.NombreTipoCliente,
                        FechaRegistro = s.FechaRegistro,
                        FechaModificado = s.FechaModificado,
                        IdUsuario = s.IdUsuario
                    }
                    ).ToList();
            }
            catch { }
          
            return tipoClienteList;
        }
    }
}

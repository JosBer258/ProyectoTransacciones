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
    public class TransaccionesServicios : ControllerBase
    {
        private readonly string URLbase = "https://localhost:44361/api/";


        public async Task<List<Transacciones>> BusquedaPorFecha(Transacciones busquedaTransacciones)
        {
           

            List<Transacciones> buquedaTransacciones = new List<Transacciones>();


            try
            {
                var httpClient = new HttpClient();

                string URL = URLbase + "Transacciones/TransaccionesDetallesFecha/";

                //httpClient.BaseAddress = new System.Uri(URLbase);


                string jsonString = System.Text.Json.JsonSerializer.Serialize(busquedaTransacciones);

                var content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");

                //var response = await httpClient.GetAsync(URL, content);

                var response = await httpClient.GetAsync(URL);
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (response.Content.Headers.Count() < 1)
                {
                    return buquedaTransacciones.ToList();
                }




                buquedaTransacciones = JsonConvert.DeserializeObject<List<Transacciones>>(apiResponse).Select(
                    s => new Transacciones
                    {
                        IdTransaccion = s.IdTransaccion,
                        IdMotivoTransaccion = s.IdMotivoTransaccion,
                        IdAgencia = s.IdAgencia,
                        IdCliente = s.IdCliente,
                        FechaTransaccion = s.FechaTransaccion,
                        MontoTransaccion = s.MontoTransaccion,
                        IdUsuario = s.IdUsuario
                    }
                    ).ToList();
            }
            catch { }

            return buquedaTransacciones;
        }

    }
    }


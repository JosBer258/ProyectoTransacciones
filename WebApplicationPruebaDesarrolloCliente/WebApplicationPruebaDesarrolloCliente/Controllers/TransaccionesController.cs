using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationPruebaDesarrolloCliente.Models;
using WebApplicationPruebaDesarrolloCliente.Service;

namespace WebApplicationPruebaDesarrolloCliente.Controllers
{
    public class TransaccionesController : Controller
    {
        public IActionResult Index()
        {
            List<Transacciones> transacciones = new List<Transacciones>();

            return View(transacciones);
        }

        public IActionResult ControlCreacion()
        {
            List<Transacciones> transacciones = new List<Transacciones>();

            return View(transacciones);
        }

        public IActionResult BusquedaPorFecha()
        {
            List<Transacciones> transacciones = new List<Transacciones>();

            return View();
        }

        public async Task<IActionResult> PasoBusquedaPorFecha(Transacciones transacciones)
        { 
            TransaccionesServicios listaTransaccionesServicios = new TransaccionesServicios();

            List<Transacciones> Lista = await listaTransaccionesServicios.BusquedaPorFecha(transacciones);

            return View(Lista);
        }





    }
}

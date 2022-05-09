using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplicationPruebaDesarrolloCliente.Models;


namespace WebApplicationPruebaDesarrolloCliente.Controllers
{
    public class AgenciasController : Controller
    {
        public IActionResult Index()
        {
            List<Agencias> Agencias = new List<Agencias>();


            return View(Agencias);
        }
    }
}

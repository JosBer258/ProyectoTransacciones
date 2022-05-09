using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplicationPruebaDesarrolloCliente.Models;

namespace WebApplicationPruebaDesarrolloCliente.Controllers
{
    public class ClientesController : Controller
    {
        public IActionResult Index()
        {
            List<Clientes> clientes = new List<Clientes>();

            return View(clientes);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplicationPruebaDesarrolloCliente.Models;


namespace WebApplicationPruebaDesarrolloCliente.Controllers
{
    public class UsuariosController : Controller
    {
        public IActionResult Index()
        {
            List<Usuarios> Usuarios = new List<Usuarios>();

            return View(Usuarios);
        }
    }
}

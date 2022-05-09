using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplicationPruebaDesarrolloCliente.Models;

namespace WebApplicationPruebaDesarrolloCliente.Controllers
{
    public class CanalServiciosController : Controller
    {
        public IActionResult Index()
        {
            List<CanalServicio> canalServicios = new List<CanalServicio>();

            return View(canalServicios);
        }
    }
}

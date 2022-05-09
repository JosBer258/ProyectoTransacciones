using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplicationPruebaDesarrolloCliente.Models;


namespace WebApplicationPruebaDesarrolloCliente.Controllers
{
    public class MotivoTransaccionController : Controller
    {
        public IActionResult Index()
        {
            List<MotivoTransaccion> motivoTransaccion = new List<MotivoTransaccion>();

            return View(motivoTransaccion);
        }
    }
}

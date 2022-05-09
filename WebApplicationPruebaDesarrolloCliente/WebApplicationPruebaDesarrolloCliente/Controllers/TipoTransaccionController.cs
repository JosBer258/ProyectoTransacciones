using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplicationPruebaDesarrolloCliente.Models;

namespace WebApplicationPruebaDesarrolloCliente.Controllers
{
    public class TipoTransaccionController : Controller
    {
        public IActionResult Index()
        {
            List<TipoTransaccion> tipoTrans = new List<TipoTransaccion>();


            return View(tipoTrans);
        }
    }
}

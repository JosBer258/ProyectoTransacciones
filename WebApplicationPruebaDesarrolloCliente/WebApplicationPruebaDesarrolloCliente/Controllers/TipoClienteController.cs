using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationPruebaDesarrolloCliente.Models;
using WebApplicationPruebaDesarrolloCliente.Service;

namespace WebApplicationPruebaDesarrolloCliente.Controllers
{
    public class TipoClienteController : Controller
    {
        public async Task<IActionResult> IndexAsync()
        {


                TipoClienteService listaTipoCliente = new TipoClienteService();

                List<TipoCliente> Lista = await listaTipoCliente.ObtenerTodosLosTiposAsync();

            

            return View(Lista);
        }

        public ActionResult Crear()
        {
            
            return View();
        }

        public ActionResult CrearNuevo(TipoCliente nuevoTipoCliente)
        {
            nuevoTipoCliente.FechaRegistro = System.DateTime.Now;

            TipoClienteService listaTipoCliente = new TipoClienteService();

            var result = listaTipoCliente.CrearNuevoTipoCliente(nuevoTipoCliente);

            return RedirectToAction("Crear");
        }

        public ActionResult Delete(int id)
        {

            TipoClienteService listaTipoCliente = new TipoClienteService();

            var result = listaTipoCliente.EliminarTipoCliente(id);

            return RedirectToAction("Index");
        }




    }
}

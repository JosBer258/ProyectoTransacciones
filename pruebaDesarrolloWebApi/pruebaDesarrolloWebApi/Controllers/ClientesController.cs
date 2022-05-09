using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pruebaDesarrolloWebApi.Data;
using pruebaDesarrolloWebApi.Models;

namespace pruebaDesarrolloWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly pruebaDesarrolloWebApiContext _context;

        public ClientesController(pruebaDesarrolloWebApiContext context)
        {
            _context = context;
        }

        // GET: api/Clientes
        //Resumen: Busqueda de todos los clientes registrados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clientes>>> GetClientes()
        {
            return await _context.Clientes.ToListAsync();
        }

        // GET: api/Clientes/5
        //Resumen: Busqueda de los Clientes en Base a ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Clientes>> GetClientes(int id)
        {
            var clientes = await _context.Clientes.FindAsync(id);

            if (clientes == null)
            {
                return NotFound();
            }

            return clientes;
        }

        // POST: api/Clientes
        // Resumen: Creacion de Nuevos Clientes
        [HttpPost]
        public async Task<ActionResult<Clientes>> PostClientes(Clientes clientes)
        {
            clientes.IdCliente = 0;
            clientes.FechaRegistro = DateTime.Now;
            clientes.FechaModificado = null;

            string resultado = clientes.CodigoCliente.Substring(0, 18);
            clientes.CodigoCliente = resultado;
            //Recorte de la cadena de caracteres en caso de sobrepasar el limite permitido




            TipoClientesController TipoClient = new TipoClientesController(_context);

            if (TipoClient.ExternoTipoClienteExiste(clientes.IdTipoCliente) == false) //Resumen:Validacion de existencia del ID de Tipo de Cliente Seleccionado.
            {
                return ValidationProblem("No existe el tipo de clientes seleccionado");
            }

            if (ClientesIDExists(clientes.NumeroIdentidad) == true)
            {
                return ValidationProblem("Ya existen clientes con el numero de identidad nacional ingresado");
            }

            _context.Clientes.Add(clientes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClientes", new { id = clientes.IdCliente }, clientes);
        }

        // PUT: api/Clientes/5
        // Modificacion de Clientes
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientes(int id, Clientes clientes)
        {
            if (id != clientes.IdCliente)
            {
                return BadRequest();
            }

            TipoClientesController TipoClient = new TipoClientesController(_context);

            if (TipoClient.ExternoTipoClienteExiste(clientes.IdTipoCliente) == false)
            {
                return ValidationProblem("No existe el tipo de clientes seleccionado");  //Resumen:Validacion de existencia del ID de Tipo de Cliente Seleccionado.
            }



            clientes.FechaModificado = DateTime.Now;

            _context.Entry(clientes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        
        // DELETE: api/Clientes/5
        //Resumen: Eliminacion de Clientes
        [HttpDelete("{id}")]
        public async Task<ActionResult<Clientes>> DeleteClientes(int id)
        {
            var clientes = await _context.Clientes.FindAsync(id);
            if (clientes == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(clientes);
            await _context.SaveChangesAsync();

            return clientes;
        }

        private bool ClientesExists(int id)
        {
            return _context.Clientes.Any(e => e.IdCliente == id);
        }

        /// <summary>
        /// Funcion Publica la cual devuelve TRUE o FALSE dependiendo si el cliente ingresado exista
        /// </summary>
        /// <param name="idCliente"></param>
        /// <returns>bool</returns>
        public bool ExternoClienteExiste(int idCliente)
        {
            return ClientesExists(idCliente);
        }

        private bool ClientesIDExists(string IdNacional)
        {
            return _context.Clientes.Any(e => e.NumeroIdentidad.Equals(IdNacional));
        }

    }
}

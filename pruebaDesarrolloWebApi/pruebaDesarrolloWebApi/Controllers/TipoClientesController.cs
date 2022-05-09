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
    public class TipoClientesController : ControllerBase
    {
        private readonly pruebaDesarrolloWebApiContext _context;

        public TipoClientesController(pruebaDesarrolloWebApiContext context)
        {
            _context = context;
        }

        // GET: api/TipoClientes
        //Busqueda de todos los tipos de clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoCliente>>> GetTipoCliente()
        {
            return await _context.TipoCliente.ToListAsync();
        }

        // GET: api/TipoClientes/5
        //Busqueda de los tipos de clientes por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoCliente>> GetTipoCliente(int id)
        {
            var tipoCliente = await _context.TipoCliente.FindAsync(id);

            if (tipoCliente == null)
            {
                return NotFound();
            }

            return tipoCliente;
        }


        // POST: api/TipoClientes
        // Ingreso de Nuevo Tipo Cliente
        [HttpPost]
        public async Task<ActionResult<TipoCliente>> PostTipoCliente(TipoCliente tipoCliente)
        {
            tipoCliente.IdTipoCliente = 0;
            tipoCliente.FechaRegistro = DateTime.Now;



            if (tipoCliente.CodigoTipoCliente.Length > 3)
            {
                string recorteTipoCliente = tipoCliente.CodigoTipoCliente.Substring(0, 3);
                tipoCliente.CodigoTipoCliente = recorteTipoCliente;
            }

            if(TipoClienteCodeExists(tipoCliente.CodigoTipoCliente) == true)
            { 
                return ValidationProblem("Ya existe un tipo de cliente con ese codigo"); //Se valida que no exista un tipo de cliente con ese codigo previamente
            }

            _context.TipoCliente.Add(tipoCliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoCliente", new { id = tipoCliente.IdTipoCliente }, tipoCliente);
        }

        // PUT: api/TipoClientes/5
        // Modificacion de Tipo de Cliente
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoCliente(int id, TipoCliente tipoCliente)
        {
            if (id != tipoCliente.IdTipoCliente)
            {
                return BadRequest();
            }

            tipoCliente.FechaModificado = DateTime.Now;


            _context.Entry(tipoCliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoClienteExists(id))
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

        

        // DELETE: api/TipoClientes/5
        //Eliminacion de Tipo de Cliente
        [HttpDelete("{id}")]
        public async Task<ActionResult<TipoCliente>> DeleteTipoCliente(int id)
        {
            var tipoCliente = await _context.TipoCliente.FindAsync(id);
            if (tipoCliente == null)
            {
                return NotFound();
            }

            _context.TipoCliente.Remove(tipoCliente);
            await _context.SaveChangesAsync();

            return tipoCliente;
        }

        private bool TipoClienteExists(int id)
        {
            return _context.TipoCliente.Any(e => e.IdTipoCliente == id);
        }

        public bool ExternoTipoClienteExiste(int uniqueId)
        {
            return TipoClienteExists(uniqueId);
        }

        //Resumen: Validacion si existe un tipo de cliente registrado con ese codigo previamente
        private bool TipoClienteCodeExists(string codigo)
        {
            return _context.TipoCliente.Any(e => e.CodigoTipoCliente.Equals(codigo));
        }
    }
}

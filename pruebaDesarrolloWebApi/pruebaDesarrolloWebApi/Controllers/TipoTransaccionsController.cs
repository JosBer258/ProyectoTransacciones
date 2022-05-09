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
    public class TipoTransaccionsController : ControllerBase
    {
        private readonly pruebaDesarrolloWebApiContext _context;

        public TipoTransaccionsController(pruebaDesarrolloWebApiContext context)
        {
            _context = context;
        }

        // DELETE: api/TipoTransaccions/5
        //Resumen: Eliminacion de transacciones
        [HttpDelete("{id}")]
        public async Task<ActionResult<TipoTransaccion>> DeleteTipoTransaccion(int id)
        {
            var tipoTransaccion = await _context.TipoTransaccion.FindAsync(id);
            if (tipoTransaccion == null)
            {
                return NotFound();
            }

            _context.TipoTransaccion.Remove(tipoTransaccion);
            await _context.SaveChangesAsync();

            return tipoTransaccion;
        }


        // GET: api/TipoTransaccions
        //Resumen: Muestra de todas las transacciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoTransaccion>>> GetTipoTransaccion()
        {
            return await _context.TipoTransaccion.ToListAsync();
        }

        // GET: api/TipoTransaccions/5
        //Resumen: Muestra de todas las transacciones por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoTransaccion>> GetTipoTransaccion(int id)
        {
            var tipoTransaccion = await _context.TipoTransaccion.FindAsync(id);

            if (tipoTransaccion == null)
            {
                return NotFound();
            }

            return tipoTransaccion;
        }




        // PUT: api/TipoTransaccions/5
        // Resumen: Aplicacion de modificacion de un tipo de transaccion
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoTransaccion(int id, TipoTransaccion tipoTransaccion)
        {

            if (id != tipoTransaccion.IdTipoTransaccion)
            {
                return BadRequest();
            }

            tipoTransaccion.FechaModificacion = DateTime.Now;


            _context.Entry(tipoTransaccion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoTransaccionExists(id))
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

        // POST: api/TipoTransaccions
        // Resumen: Creacion de un nuevo Tipo Transacciones
        [HttpPost]
        public async Task<ActionResult<TipoTransaccion>> PostTipoTransaccion(TipoTransaccion tipoTransaccion)
        {
            tipoTransaccion.IdTipoTransaccion = 0;
            tipoTransaccion.FechaRegistro = DateTime.Now;
            tipoTransaccion.FechaModificacion = null;

            if (TipoTransaccionCodigoExist(tipoTransaccion.CodigoTipoTransaccion))
            {
                return ValidationProblem("Ya existe un Tipo de Transaccion con este codigo");
            }


            if (tipoTransaccion.CodigoTipoTransaccion.Equals("DB") == false || tipoTransaccion.CodigoTipoTransaccion.Equals("CR") == false)
            {
                return ValidationProblem("Los Codigos de Transaccion solo pueden ser DB Debito o CR Credito");
            }

            _context.TipoTransaccion.Add(tipoTransaccion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoTransaccion", new { id = tipoTransaccion.IdTipoTransaccion }, tipoTransaccion);
        }

        //Resumen: Validacion de existencia del codigo de transacciones, devolviendo true en caso de que ya exista una transaccion con ese codigo
        private bool TipoTransaccionCodigoExist(int cod)
        {
            return _context.TipoTransaccion.Any(e => e.CodigoTipoTransaccion == cod);
        }

        private bool TipoTransaccionExists(int id)
        {
            return _context.TipoTransaccion.Any(e => e.IdTipoTransaccion == id);
        }

        public bool ValidarExternoIDTipoTransaccion(int idTransaccion)
        {
            return TipoTransaccionExists(idTransaccion);

        }
    }
}

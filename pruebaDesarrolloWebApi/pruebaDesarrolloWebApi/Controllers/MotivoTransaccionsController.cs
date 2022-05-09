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
    public class MotivoTransaccionsController : ControllerBase
    {
        private readonly pruebaDesarrolloWebApiContext _context;

        public MotivoTransaccionsController(pruebaDesarrolloWebApiContext context)
        {
            _context = context;
        }

        // GET: api/MotivoTransaccions
        //Busqueda de todos los Motivos de Transaccion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MotivoTransaccion>>> GetMotivoTransaccion()
        {
            return await _context.MotivoTransaccion.ToListAsync();
        }

        // GET: api/MotivoTransaccions/5
        //Resumen: Busqueda de todos los motivos de transaccion por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<MotivoTransaccion>> GetMotivoTransaccion(int id)
        {
            var motivoTransaccion = await _context.MotivoTransaccion.FindAsync(id);

            if (motivoTransaccion == null)
            {
                return NotFound();
            }

            return motivoTransaccion;
        }

        // DELETE: api/MotivoTransaccions/5
        //Eliminacion de Motivos de Transaccion
        [HttpDelete("{id}")]
        public async Task<ActionResult<MotivoTransaccion>> DeleteMotivoTransaccion(int id)
        {
            var motivoTransaccion = await _context.MotivoTransaccion.FindAsync(id);
            if (motivoTransaccion == null)
            {
                return NotFound();
            }

            _context.MotivoTransaccion.Remove(motivoTransaccion);
            await _context.SaveChangesAsync();

            return motivoTransaccion;
        }




        // POST: api/MotivoTransaccions
        // 
        [HttpPost]
        public async Task<ActionResult<MotivoTransaccion>> PostMotivoTransaccion(MotivoTransaccion motivoTransaccion)
        {
            TipoTransaccionsController tipoTransaccions = new TipoTransaccionsController(_context);


            motivoTransaccion.IdMotivoTransaccion = 0;
            motivoTransaccion.FechaRegistro = DateTime.Now;
            motivoTransaccion.FechaModificado = null;

            if(MotivoTransaccionExists(motivoTransaccion.CodigoMotivoTransaccion) == true)
            {
                return ValidationProblem("Ya existe registro de Motivo de Transaccion Ingresado Actualmente");
            }

            if (tipoTransaccions.ValidarExternoIDTipoTransaccion(motivoTransaccion.IdTipoTransaccion) == false)
            {
                return ValidationProblem("No Existe el Codigo Seleccionado de Tipo Transaccion");
            }

            _context.MotivoTransaccion.Add(motivoTransaccion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMotivoTransaccion", new { id = motivoTransaccion.IdMotivoTransaccion }, motivoTransaccion);
        }






        // PUT: api/MotivoTransaccions/5
        // Modificacion de Motivos de Transaccion
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMotivoTransaccion(int id, MotivoTransaccion motivoTransaccion)
        {
            TipoTransaccionsController tipoTransaccions = new TipoTransaccionsController(_context);

            if (id != motivoTransaccion.IdMotivoTransaccion)
            {
                return BadRequest();
            }

            motivoTransaccion.FechaModificado = DateTime.Now;


            if (tipoTransaccions.ValidarExternoIDTipoTransaccion(motivoTransaccion.IdTipoTransaccion) == false)
            {
                return ValidationProblem("No Existe el Codigo Seleccionado de Tipo Transaccion");
            }



            _context.Entry(motivoTransaccion).State = EntityState.Modified;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MotivoTransaccionExists(id))
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

        //Validacion para evitar el registro de multiples motivos de transaccion con el mismo codigo
        private bool MotivoTransaccionExists(string codigoMT)
        {
            return _context.MotivoTransaccion.Any(e => e.CodigoMotivoTransaccion.Equals(codigoMT));
        }

        //Resumen: Funcion que devuelve verdadero o falso dependiendo de que exista o no un motivo de transaccion con el id ingresado
        public bool ExternoMotivoTransaccionExiste(int IdMotivo)
        {
            return MotivoTransaccionExists(IdMotivo);
        }


        private bool MotivoTransaccionExists(int id)
        {
            return _context.MotivoTransaccion.Any(e => e.IdMotivoTransaccion == id);
        }
    }
}

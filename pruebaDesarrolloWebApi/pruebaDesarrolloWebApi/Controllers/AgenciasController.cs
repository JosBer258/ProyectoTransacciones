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
    public class AgenciasController : ControllerBase
    {
        private readonly pruebaDesarrolloWebApiContext _context;

        public AgenciasController(pruebaDesarrolloWebApiContext context)
        {
            _context = context;
        }

        // GET: api/Agencias
        //Resumen: Busqueda de todas las Agencias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Agencias>>> GetAgencias()
        {
            return await _context.Agencias.ToListAsync();
        }

        // GET: api/Agencias/5
        //Resumen: Busqueda de las agencias por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Agencias>> GetAgencias(int id)
        {
            var agencias = await _context.Agencias.FindAsync(id);

            if (agencias == null)
            {
                return NotFound();
            }

            return agencias;
        }

        //Adqueriri el listado de las agencias con los canales de servicio
        [HttpGet("Canales")]
        public async Task<ActionResult<IEnumerable<Agencias>>> GetAgenciasCanalesServicio()
        {
            return await _context.Agencias.Include(x => x.CanalServicio).ToListAsync();
        }

        //Busqueda de las agencias Exitentes
         private bool AgenciasExists(int id)
        {
            return _context.Agencias.Any(e => e.IdAgencia == id);
        }

        private bool ExisteCanalServicio(int id)
        {
            CanalServiciosController canalServicio = new CanalServiciosController(_context);



            return canalServicio.CanalServicioExistePublico(id);
        }

        //Resumen: Funcion que devuelve verdadero o falso dependiendo de que exista o no una agencia con el id ingresado
        public bool ExternoExisteAgencia(int idAgencia)
        {
            return AgenciasExists(idAgencia);
        }


        // PUT: api/Agencias/5
        // Modificacion de las Agencias Existentes
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAgencias(int id, Agencias agencias)
        {
            if (id != agencias.IdAgencia)
            {
                return BadRequest();
            }

            agencias.FechaModificado = DateTime.Now;

            if (!ExisteCanalServicio(agencias.IdCanalServicio)) //Validar si existe el canal de servicio al seleccionar
            {
                return ValidationProblem("No existe el canal de servicio seleccionado");
            }

            _context.Entry(agencias).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgenciasExists(id))
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

        // POST: api/Agencias
        // Creacion de Nueva Agencia
        [HttpPost]
        public async Task<ActionResult<Agencias>> PostAgencias(Agencias agencias)
        {
            agencias.IdAgencia = 0;

            if (!ExisteCanalServicio(agencias.IdCanalServicio)) //Validar si existe el canal de servicio al seleccionar
            {
                return ValidationProblem("No existe el canal de servicio seleccionado");
            }

            if (ExisteAgenciaPreviamente(agencias.CodigoAgencia))
            {
                return ValidationProblem("Ya existe una agencia con ese codigo");
            }

            agencias.FechaRegistro = DateTime.Now; //La fecha de registro sera la fecha y hora en que se ejecute este metodo
            agencias.FechaModificado = null;


            _context.Agencias.Add(agencias);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAgencias", new { id = agencias.IdAgencia }, agencias);
        }

        public bool ExisteAgenciaPreviamente(string codigoAgencia) //Resumen: Validar si la agencia existe previamente antes de ingresarla, en caso de que exista debe presentar error de validacion
        {
            bool existeAgencia = false;

            var AgenciaPorCodigo = from st in _context.Agencias
                                   where st.CodigoAgencia.Equals(codigoAgencia)
                                         select st;

            if (AgenciaPorCodigo.Count() > 0)
            {
                existeAgencia = true;

            }

            return existeAgencia;
        }




        // DELETE: api/Agencias/5
        //Elimiacion de Agencia por ID
        [HttpDelete("{id}")]
        public async Task<ActionResult<Agencias>> DeleteAgencias(int id)
        {
            var agencias = await _context.Agencias.FindAsync(id);
            if (agencias == null)
            {
                return NotFound();
            }

            _context.Agencias.Remove(agencias);
            await _context.SaveChangesAsync();

            return agencias;
        }

       
    }
}

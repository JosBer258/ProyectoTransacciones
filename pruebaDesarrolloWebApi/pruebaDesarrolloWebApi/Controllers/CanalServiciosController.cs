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
    public class CanalServiciosController : ControllerBase
    {
        private readonly pruebaDesarrolloWebApiContext _context;

        public CanalServiciosController(pruebaDesarrolloWebApiContext context)
        {
            _context = context;
        }

        // GET: api/CanalServicios
        //Resumen: Devuelve todos los canales de servicio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CanalServicio>>> GetCanalServicio()
        {
            return await _context.CanalServicio.ToListAsync();
        }

        // GET: api/CanalServicios/5
        //Resumen: Devuelve los canales de servicio con el codigo ingresado
        [HttpGet("{id}")]
        public async Task<ActionResult<CanalServicio>> GetCanalServicio(int id)
        {
            var canalServicio = await _context.CanalServicio.FindAsync(id);

            if (canalServicio == null)
            {
                return NotFound();
            }

            return canalServicio;
        }

        //Resumen: Devuelve todos los canales de servicio cuyo codigo contenga el valor ingresado
        [HttpGet("codigo/{codigo}")]
        public async Task<List<CanalServicio>> GetCanalServicioCodgio(string codigo)
        {
           var CanalServicioPorCodigo =  from st in _context.CanalServicio
                        where st.CodigoCanalServicio.Contains(codigo)
                        select st;

           if (CanalServicioPorCodigo == null)
            {
                Console.WriteLine("No Existe Canal de Servicio");

            }

            return CanalServicioPorCodigo.ToList();
        }


        // PUT: api/CanalServicios/5
        //Resumen: Ejecucion de modificacion en base a codigo Ingresado
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCanalServicio(int id, CanalServicio canalServicio)
        {


            if (id != canalServicio.IdCanalServicio)
            {
                return BadRequest();
            }

            canalServicio.FechaModificado = DateTime.Now;


            _context.Entry(canalServicio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CanalServicioExiste(id))
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




        // POST: api/CanalServicios
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CanalServicio>> PostCanalServicio(CanalServicio canalServicio)
        {

            canalServicio.IdCanalServicio = 0;

            if (ObtenerCanalServicioCodgio(canalServicio.CodigoCanalServicio))
            {
                return ValidationProblem("Ya existe un canal de servicio con este nombre"); //Resumen: Validacion para que no se repita el nombre de los Canales
            }

            if(canalServicio.CodigoCanalServicio.Length < 0 || canalServicio.CodigoCanalServicio.Length > 3)
            {
                return ValidationProblem("El valor no puede tener mas de 3 caracteres"); //Se valida la longitud del codigo
            }

            canalServicio.FechaRegistro = DateTime.Now; //La fecha de registro sera la fecha y hora en que se ejecute este metodo
            canalServicio.FechaModificado = null;

            _context.CanalServicio.Add(canalServicio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCanalServicio", new { id = canalServicio.IdCanalServicio }, canalServicio);
        }

        //Resumen: Validacion para que no se repita el nombre de los Canales, comparando el valor a ingresar con los canales ya existentes
        public bool ObtenerCanalServicioCodgio(string codigo)
        {
            bool existeCanalServicio = false;

            var CanalServicioPorCodigo = from st in _context.CanalServicio
                                         where st.CodigoCanalServicio.Equals(codigo)
                                         select st;

            if (CanalServicioPorCodigo.Count() > 0)
            {
                existeCanalServicio = true;

            }

            return existeCanalServicio;
        }







        // DELETE: api/CanalServicios/5
        //Eliminacion del canal de servicio por id
        [HttpDelete("{id}")]
        public async Task<ActionResult<CanalServicio>> DeleteCanalServicio(int id)
        {

            var canalServicio = await _context.CanalServicio.FindAsync(id);
            if (canalServicio == null)
            {
                return NotFound();
            }

            _context.CanalServicio.Remove(canalServicio);
            await _context.SaveChangesAsync();

            return canalServicio;
        }

        //Resumen: Busqueda de Canal de Servicio en Base a Codigo
        private bool CanalServicioExiste(int id)
        {
            return _context.CanalServicio.Any(e => e.IdCanalServicio == id);
        }

        public bool CanalServicioExistePublico(int id_pub) //Funcion Externa que permite devolver true o false dependiendo de que si el canal de servicio seleccionado exista o no
        {
            return CanalServicioExiste(id_pub);
        }
    }
}

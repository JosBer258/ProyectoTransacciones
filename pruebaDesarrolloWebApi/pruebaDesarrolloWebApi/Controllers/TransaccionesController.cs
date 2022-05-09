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
    public class TransaccionesController : ControllerBase
    {
        private readonly pruebaDesarrolloWebApiContext _context;

        public TransaccionesController(pruebaDesarrolloWebApiContext context)
        {
            _context = context;
        }

        // GET: api/Transacciones
        //Resumen: Busqueda de todas las transacciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transacciones>>> GetTransacciones()
        {
            return await _context.Transacciones.ToListAsync();
        }

        // GET: api/Transacciones/TransaccionesDetalles/
        //Resumen: Busqueda de todas las transacciones con sus detalles
        [HttpGet("TransaccionesDetalles")]
        public async Task<ActionResult<IEnumerable<Transacciones>>> GetTransaccionesDetalles()
        {
            return await _context.Transacciones.Include(x => x.MotivoTransaccion)
                                .ThenInclude(e => e.TipoTransaccion)
                .Include(x => x.Agencias)
                    .ThenInclude(e => e.CanalServicio)
                .Include(x => x.Clientes)
                    .ThenInclude(e => e.TipoCliente)
                .ToListAsync();
        }

        // GET: api/Transacciones/TransaccionesFecha
        //Resumen: Busqueda de todas las transacciones con sus detalles por fecha
        [HttpGet("TransaccionesFecha")]
        public async Task<ActionResult<IEnumerable<Transacciones>>> GetTransaccionesFecha(Transacciones transacciones)
        {
           
            var fechaDiaMesAño = transacciones.FechaTransaccion.Value.ToShortDateString();

            var fechaAnno = transacciones.FechaTransaccion.Value.Year;
            var fechaMes = transacciones.FechaTransaccion.Value.Month;
            var fechaDia = transacciones.FechaTransaccion.Value.Day;

            return await _context.Transacciones.Where(x => x.FechaTransaccion.Value.Year.Equals(fechaAnno)
            && x.FechaTransaccion.Value.Month.Equals(fechaMes)
            && x.FechaTransaccion.Value.Day.Equals(fechaDia)).ToListAsync();

        }

        //GET: api/Transacciones/TransaccionesDetallesFecha/
        [HttpGet("TransaccionesDetallesFecha")]
        public async Task<ActionResult<IEnumerable<Transacciones>>> GetTransaccionesDetallesFecha(Transacciones transacciones)
        {

            var fechaDiaMesAño = transacciones.FechaTransaccion.Value.ToShortDateString();

            var fechaAnno = transacciones.FechaTransaccion.Value.Year;
            var fechaMes = transacciones.FechaTransaccion.Value.Month;
            var fechaDia = transacciones.FechaTransaccion.Value.Day;

            return await _context.Transacciones
                .Include(x => x.MotivoTransaccion)
                                .ThenInclude(e => e.TipoTransaccion)
                .Include(x => x.Agencias)
                    .ThenInclude(e => e.CanalServicio)
                .Include(x => x.Clientes)
                    .ThenInclude(e => e.TipoCliente)
                .Where(x => x.FechaTransaccion.Value.Year.Equals(fechaAnno)
            && x.FechaTransaccion.Value.Month.Equals(fechaMes)
            && x.FechaTransaccion.Value.Day.Equals(fechaDia)).ToListAsync();

        }


        // GET: api/Transacciones/5
        //Busqueda de transaccion por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Transacciones>> GetTransacciones(int id)
        {
            var transacciones = await _context.Transacciones.FindAsync(id);

            if (transacciones == null)
            {
                return NotFound();
            }

            return transacciones;
        }

        //Busqueda de transacciones por ID con Detalles
        // GET: api/Transacciones/Detalles/5
        [HttpGet("Detalles/{id}")]
        public async Task<ActionResult<Transacciones>> GetTransaccionesDetalles(int id)
        {
            var transacciones = await _context.Transacciones
                .Include(x => x.MotivoTransaccion)
                                .ThenInclude(e => e.TipoTransaccion)
                .Include(x => x.Agencias)
                    .ThenInclude(e => e.CanalServicio)
                .Include(x => x.Clientes)
                    .ThenInclude(e => e.TipoCliente)
                .Where(x => x.IdTransaccion.Equals(id)).ToListAsync();


            if (transacciones.Count < 1)
            {
                return NotFound();
            }

            return transacciones[0];
        }



        // PUT: api/Transacciones/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransacciones(int id, Transacciones transacciones)
        {
            if (id != transacciones.IdTransaccion)
            {
                return BadRequest();
            }

            if (ValidarDetallesTransaccionAgencia(transacciones.IdAgencia) == false) return ValidationProblem("No existe una agencia con el ID selecionado");
            if (ValidarDetallesTransaccionMotivo(transacciones.IdMotivoTransaccion) == false) return ValidationProblem("No existe un motivo de transaccion con el ID selecionado");
            if (ValidarDetallesTransaccionCliente(transacciones.IdCliente) == false) return ValidationProblem("No existe un cliente con el ID seleccionado");

            _context.Entry(transacciones).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransaccionesExists(id))
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

        // POST: api/Transacciones
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Transacciones>> PostTransacciones(Transacciones transacciones)
        {
            transacciones.FechaTransaccion = DateTime.Now;
            transacciones.IdTransaccion = 0;


            if (ValidarDetallesTransaccionAgencia(transacciones.IdAgencia) == false) return ValidationProblem("No existe una agencia con el ID selecionado");
            if (ValidarDetallesTransaccionMotivo(transacciones.IdMotivoTransaccion) == false) return ValidationProblem("No existe un motivo de transaccion con el ID selecionado");
            if (ValidarDetallesTransaccionCliente(transacciones.IdCliente) == false) return ValidationProblem("No existe un cliente con el ID seleccionado");

            if (transacciones.MontoTransaccion == null || transacciones.MontoTransaccion <= 0)
            {
                return ValidationProblem("Debe ingresar un monto valido");
            }

            _context.Transacciones.Add(transacciones);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransacciones", new { id = transacciones.IdTransaccion }, transacciones);
        }

        // DELETE: api/Transacciones/5
        // Resumen: Eliminacion de Transacciones
        [HttpDelete("{id}")]
        public async Task<ActionResult<Transacciones>> DeleteTransacciones(int id)
        {
            var transacciones = await _context.Transacciones.FindAsync(id);
            if (transacciones == null)
            {
                return NotFound();
            }

            _context.Transacciones.Remove(transacciones);
            await _context.SaveChangesAsync();

            return transacciones;
        }

        private bool TransaccionesExists(int id)
        {
            return _context.Transacciones.Any(e => e.IdTransaccion == id);
        }

        private bool ValidarDetallesTransaccionAgencia(int idBusqueda)
        {
            AgenciasController agencia = new AgenciasController(_context);
            return agencia.ExternoExisteAgencia(idBusqueda);
        }

        private bool ValidarDetallesTransaccionMotivo(int idBusqueda)
        {
            MotivoTransaccionsController motivoTran = new MotivoTransaccionsController(_context);

            return motivoTran.ExternoMotivoTransaccionExiste(idBusqueda);
        }

        private bool ValidarDetallesTransaccionCliente(int idBusqueda)
        {
            ClientesController cliente = new ClientesController(_context);
            return cliente.ExternoClienteExiste(idBusqueda); 
        }
    }
}

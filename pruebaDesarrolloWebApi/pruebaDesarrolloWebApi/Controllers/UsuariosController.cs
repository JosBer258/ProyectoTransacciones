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
    public class UsuariosController : ControllerBase
    {
        private readonly pruebaDesarrolloWebApiContext _context;

        public UsuariosController(pruebaDesarrolloWebApiContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuarios>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuarios>> GetUsuarios(int id)
        {
            var usuarios = await _context.Usuarios.FindAsync(id);

            if (usuarios == null)
            {
                return NotFound();
            }

            return usuarios;
        }

        //GET: api/Usuarios/Validacion
        //Resumen: Validar si existe una convinacion de Contraseña y Codigo de Usuario devolviendo el objeto Usuario
        [HttpGet("Validacion")]
        public async Task<ActionResult<Usuarios>> VerificarUsuarioContraseña(Usuarios usuarioContra)
        {
            Usuarios usuarioMensaje = new Usuarios();
            usuarioMensaje.IdUsuario = 0;
            usuarioMensaje.CodigoUsuario = "ERR";
            usuarioMensaje.NombreUsuario = "";

            var usuariocont = await _context.Usuarios.Where(x => x.CodigoUsuario.Equals(usuarioContra.CodigoUsuario) && x.PasswordUsuario.Equals(usuarioContra.PasswordUsuario)).ToListAsync();

            if (usuariocont.Count < 1) //en caso de que no exista un usuario con esa contraseña devolvera error
            {
                usuarioMensaje.NombreUsuario = "El usuario o la contraseña son incorrectos";
                return Ok(usuarioMensaje);
            }

            if (VerificarUsuarioActivo(usuarioContra) == false) //si el usuario tiene FALSE como isActive entonces devolvera mensaje de error
            {
                usuarioMensaje.NombreUsuario = "El usuario esta desactivado";
                return Ok(usuarioMensaje);
            }

           EstablecerFechaUltimaConexion(usuariocont[0]);

            return Ok(usuariocont[0]);
        }

        //GET: api/Usuarios/ValidacionActivo/5
        //Resumen: Validar si el usuario esta activo

        public bool VerificarUsuarioActivo(Usuarios usuarioEstado)
        {

            var UsuarioEstado = from st in _context.Usuarios
                                where st.CodigoUsuario.Equals(usuarioEstado.CodigoUsuario) && st.IsActivo == false
                                select st;

            if (UsuarioEstado.Count() >= 1)
            {
                return false;

            }

            return true;
        }

        //Modificacion de ultima conexion
        public void EstablecerFechaUltimaConexion(Usuarios usuarioCmbio)
        {

            usuarioCmbio.UltimaConexion =  DateTime.Now;

            _context.Entry(usuarioCmbio).State = EntityState.Modified;

            try
            {
               _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuariosExists(usuarioCmbio.IdUsuario))
                {
                    return;
                }
                else
                {
                    throw;
                }
            }
        }
            // PUT: api/Usuarios/5
            // Resumen: Modificacion del usuario por ID
            [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarios(int id, Usuarios usuarios)
        {
            if (id != usuarios.IdUsuario)
            {
                return BadRequest();
            }

            usuarios.FechaModificado = DateTime.Now;

            _context.Entry(usuarios).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuariosExists(id))
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





        // POST: api/Usuarios
        // Resumen: Creacion de Nuevos Usuarios
        [HttpPost]
        public async Task<ActionResult<Usuarios>> PostUsuarios(Usuarios usuarios)
        {
            usuarios.FechaRegistro = DateTime.Now;

            if (ValidacionNombreUsuario(usuarios.CodigoUsuario.ToString().ToLower()))
            {
                return ValidationProblem("El nombre del usuario ya esta en uso");
            }

            _context.Usuarios.Add(usuarios);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuarios", new { id = usuarios.IdUsuario }, usuarios);
        }

        //Resumen: Verificara si el nombre del usuario ya esta en uso
        private bool ValidacionNombreUsuario(string usuarioValidacion)
        {

            var usuarioPorCodigo = from st in _context.Usuarios
                                   where st.CodigoUsuario.ToLower().Equals(usuarioValidacion)
                                   select st;

            if (usuarioPorCodigo.Count() > 0)
            {
                return true;

            }

            return false;
        }







        // DELETE: api/Usuarios/5
        //Resumen: Funcion de eliminar modificada de tal manera que, al tratar de eliminar un usuario, el estado se convierta de ACTIVO en DESACTIVADO, o FALSE en todo caso.
        [HttpDelete("{id}")]
        public async Task<ActionResult<Usuarios>> DeleteUsuarios(int id)
        {
            var usuarios =  await _context.Usuarios.FindAsync(id);

            if (id != usuarios.IdUsuario)
            {
                return BadRequest();
            }

            _context.Entry(usuarios).State = EntityState.Modified;

            usuarios.IsActivo = false;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuariosExists(id))
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







        private bool UsuariosExists(int id)
        {
            return _context.Usuarios.Any(e => e.IdUsuario == id);
        }
    }
}

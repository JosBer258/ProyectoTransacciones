using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationPruebaDesarrolloCliente.Models
{
    public class Usuarios
    {

        private int idUsuario;
        private string codigoUsuario;
        private string nombreUsuario;
        private string passwordUsuario;
        private bool? isActivo;
        private DateTime? ultimaConexion;
        private DateTime? fechaRegistro;
        private DateTime? fechaModificado;
        private int? idUsuarioRegistro;

        public int IdUsuario { get { return idUsuario; } set { idUsuario = value; } }
        public string CodigoUsuario { get { return codigoUsuario; } set { codigoUsuario = value; } }
        public string NombreUsuario { get { return nombreUsuario; } set { nombreUsuario = value; } }
        public string PasswordUsuario { get { return passwordUsuario; } set { passwordUsuario = value; } }
        public bool? IsActivo { get { return isActivo; } set { isActivo = value; } }
        public DateTime? UltimaConexion { get { return ultimaConexion; } set { ultimaConexion = value; } }
        public DateTime? FechaRegistro { get { return fechaRegistro; } set { fechaRegistro = value; } }
        public DateTime? FechaModificado { get { return fechaModificado; } set { fechaModificado = value; } }
        public int? IdUsuarioRegistro { get { return idUsuarioRegistro; } set { idUsuarioRegistro = value; } }

    }
}

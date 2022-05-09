using System;
using System.ComponentModel.DataAnnotations;



namespace WebApplicationPruebaDesarrolloCliente.Models
{
    public class Clientes
    {

        private int idCliente;
        private int idTipoCliente;
        private string codigoCliente;
        private string numeroIdentidad;
        private string nombreCliente;
        private DateTime? fechaRegistro;
        private DateTime? fechaModificado;
        private int? idUsuario;

        public int IdCliente { get { return idCliente; } set { idCliente = value; } }
        public int IdTipoCliente { get { return idTipoCliente; } set { idTipoCliente = value; } }
        public string CodigoCliente { get { return codigoCliente; } set { codigoCliente = value; } }
        public string NumeroIdentidad { get { return numeroIdentidad; } set { numeroIdentidad = value; } }
        public string NombreCliente { get { return nombreCliente; } set { nombreCliente = value; } }
        public DateTime? FechaRegistro { get { return fechaRegistro; } set { fechaRegistro = value; } }
        public DateTime? FechaModificado { get { return fechaModificado; } set { fechaModificado = value; } }
        public int? IdUsuario { get { return idUsuario; } set { idUsuario = value; } }

        public virtual TipoCliente TipoCliente { get; set; }
    }
}

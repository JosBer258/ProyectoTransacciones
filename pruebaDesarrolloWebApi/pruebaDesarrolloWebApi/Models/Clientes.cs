using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pruebaDesarrolloWebApi.Models
{
    [Table("Clientes", Schema = "General")]
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

        [Key]
        public int IdCliente { get { return idCliente; } set { idCliente = value; } }

        [ForeignKey("TipoCliente")]
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

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pruebaDesarrolloWebApi.Models
{
    [Table("TipoCliente", Schema = "General")]
    public class TipoCliente
    {

        private int idTipoCliente;
        private string codigoTipoCliente;
        private string nombreTipoCliente;
        private DateTime? fechaRegistro;
        private DateTime? fechaModificado;
        private int? idUsuario;

        [Key]
        public int IdTipoCliente { get { return idTipoCliente; } set { idTipoCliente = value; } }
        public string CodigoTipoCliente { get { return codigoTipoCliente; } set { codigoTipoCliente = value; } }
        public string NombreTipoCliente { get { return nombreTipoCliente; } set { nombreTipoCliente = value; } }

        public DateTime? FechaRegistro { get { return fechaRegistro; } set { fechaRegistro = value; } }
        public DateTime? FechaModificado { get { return fechaModificado; } set { fechaModificado = value; } }
        public int? IdUsuario { get { return idUsuario; } set { idUsuario = value; } }
    }
}

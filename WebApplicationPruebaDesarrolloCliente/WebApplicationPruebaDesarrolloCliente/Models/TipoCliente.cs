using System;
using System.ComponentModel.DataAnnotations;
namespace WebApplicationPruebaDesarrolloCliente.Models
{
    public class TipoCliente
    {
        private int idTipoCliente;
        private string codigoTipoCliente;
        private string nombreTipoCliente;
        private DateTime? fechaRegistro;
        private DateTime? fechaModificado;
        private int? idUsuario;

        [Display(Name = "ID Unico del Codigo Tipo")]
        public int IdTipoCliente { get { return idTipoCliente; } set { idTipoCliente = value; } }
        [Display(Name = "Codigo Tipo")]
        public string CodigoTipoCliente { get { return codigoTipoCliente; } set { codigoTipoCliente = value; } }
        [Display(Name = "Nombre del Tipo de Cliente")]
        public string NombreTipoCliente { get { return nombreTipoCliente; } set { nombreTipoCliente = value; } }
        [Display(Name = "Fecha de Registro")]
        public DateTime? FechaRegistro { get { return fechaRegistro; } set { fechaRegistro = value; } }
        [Display(Name = "Fecha Ultima Modificacion")]
        public DateTime? FechaModificado { get { return fechaModificado; } set { fechaModificado = value; } }
        [Display(Name = "ID Usuario")]
        public int? IdUsuario { get { return idUsuario; } set { idUsuario = value; } }
    }
}

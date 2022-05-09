using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace pruebaDesarrolloWebApi.Models
{
    [Table("TipoTransaccion", Schema = "Parametros")]
    public class TipoTransaccion
    {

        private int idTipoTransaccion;
        private string codigoTipoMovimiento;
        private int codigoTipoTransaccion;
        private string nombreTipoTransaccion;
        private DateTime? fechaRegistro;
        private DateTime? fechaModificacion;
        private int? idUsuario;

        [Key]
        public int IdTipoTransaccion { get { return idTipoTransaccion; } set { idTipoTransaccion = value; } }
        public string CodigoTipoMovimiento { get { return codigoTipoMovimiento; } set { codigoTipoMovimiento = value; } }
        public int CodigoTipoTransaccion { get { return codigoTipoTransaccion; } set { codigoTipoTransaccion = value; } }
        public string NombreTipoTransaccion { get { return nombreTipoTransaccion; } set { nombreTipoTransaccion = value; } }
        public DateTime? FechaRegistro { get { return fechaRegistro; } set { fechaRegistro = value; } }
        public DateTime? FechaModificacion { get { return fechaModificacion; } set { fechaModificacion= value; } }
        public int? IdUsuario { get { return idUsuario; } set { idUsuario = value; } }
    }
}

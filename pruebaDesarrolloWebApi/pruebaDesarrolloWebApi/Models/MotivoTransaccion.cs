using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace pruebaDesarrolloWebApi.Models
{
    [Table("MotivoTransaccion", Schema = "Parametros")]
    public class MotivoTransaccion
    {
        private int idMotivoTransaccion;
        private int idTipoTransaccion;
        private string codigoMotivoTransaccion;
        private string nombreMotivoTransaccion;
        private DateTime? fechaRegistro;
        private DateTime? fechaModificado;
        private int? idUsuario;



        [Key]
        public int IdMotivoTransaccion { get { return idMotivoTransaccion; } set { idMotivoTransaccion = value; } }

        [ForeignKey("TipoTransaccion")]
        public int IdTipoTransaccion { get { return idTipoTransaccion; } set { idTipoTransaccion = value; } }
        public string CodigoMotivoTransaccion { get { return codigoMotivoTransaccion; } set { codigoMotivoTransaccion = value; } }
        public string NombreMotivoTransaccion { get { return nombreMotivoTransaccion; } set { nombreMotivoTransaccion = value; } }
        public DateTime? FechaRegistro { get { return fechaRegistro; } set { fechaRegistro = value; } }
        public DateTime? FechaModificado { get { return fechaModificado; } set { fechaModificado = value; } }
        public int? IdUsuario { get { return idUsuario; } set { idUsuario = value; } }

        public virtual TipoTransaccion TipoTransaccion { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationPruebaDesarrolloCliente.Models
{
    public class Transacciones
    {
        private int idTransaccion;
        private int idMotivoTransaccion;
        private int idAgencia;
        private int idCliente;
        private DateTime? fechaTransaccion;
        private Decimal? montoTransaccion;
        private int? idUsuario;
        public int IdTransaccion { get { return idTransaccion; } set { idTransaccion = value; } }

        public int IdMotivoTransaccion { get { return idMotivoTransaccion; } set { idMotivoTransaccion = value; } }

        public int IdAgencia { get { return idAgencia; } set { idAgencia = value; } }

        public int IdCliente { get { return idCliente; } set { idCliente = value; } }
        public DateTime? FechaTransaccion { get { return fechaTransaccion; } set { fechaTransaccion = value; } }
        public Decimal? MontoTransaccion { get { return montoTransaccion; } set { montoTransaccion = value; } }
        public int? IdUsuario { get { return idUsuario; } set { idUsuario = value; } }

        public virtual MotivoTransaccion MotivoTransaccion { get; set; }
        public virtual Agencias Agencias { get; set; }
        public virtual Clientes Clientes { get; set; }
    }
}

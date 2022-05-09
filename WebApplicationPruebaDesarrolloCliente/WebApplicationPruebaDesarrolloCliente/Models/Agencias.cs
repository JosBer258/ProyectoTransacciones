using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationPruebaDesarrolloCliente.Models
{
    public class Agencias
    {
        private int idAgencia;
        private int idCanalServicio;
        private string? codigoAgencia;
        private string? nombreAgencia;
        private string? direccionAgencia;
        private string? telefonoAgencia;
        private DateTime? fechaRegistro;
        private DateTime? fechaModificado;
        private int idUsuario;


        public int IdAgencia { get { return idAgencia; } set { idAgencia = value; } }

      
        public int IdCanalServicio { get { return idCanalServicio; } set { idCanalServicio = value; } }

        [StringLength(5)]
        public string? CodigoAgencia { get { return codigoAgencia; } set { codigoAgencia = value; } }
        public string? NombreAgencia { get { return nombreAgencia; } set { nombreAgencia = value; } }
        public string? DireccionAgencia { get { return direccionAgencia; } set { direccionAgencia = value; } }
        public string? TelefonoAgencia { get { return telefonoAgencia; } set { telefonoAgencia = value; } }
        public DateTime? FechaRegistro { get { return fechaRegistro; } set { fechaRegistro = value; } }
        public DateTime? FechaModificado { get { return fechaModificado; } set { fechaModificado = value; } }
        public int IdUsuario { get { return idUsuario; } set { idUsuario = value; } }

        public virtual CanalServicio CanalServicio { get; set; }
    }
}

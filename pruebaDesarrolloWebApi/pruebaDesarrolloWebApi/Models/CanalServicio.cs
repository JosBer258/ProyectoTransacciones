using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace pruebaDesarrolloWebApi.Models
{
    [Table("CanalServicio", Schema = "General")]
    public class CanalServicio
    {
        private int idCanalServicio;
        private string codigoCanalServicio;
        private string nombreCanalServicio;
        private DateTime fechaRegistro;
        private DateTime? fechaModificado;
        private int idUsuario;

     


        [Key]
        public int IdCanalServicio { get { return idCanalServicio; } set { idCanalServicio = value; } }
        public string CodigoCanalServicio { get { return codigoCanalServicio; } set { codigoCanalServicio = value; } }
        public string NombreCanalServicio { get { return nombreCanalServicio; } set { nombreCanalServicio = value; } }
        public DateTime FechaRegistro { get { return fechaRegistro; } set { fechaRegistro = value; } }
        public DateTime? FechaModificado { get { return fechaModificado; } set { fechaModificado = value; } }
        public int IdUsuario { get { return idUsuario; } set { idUsuario = value; } }

    }
}

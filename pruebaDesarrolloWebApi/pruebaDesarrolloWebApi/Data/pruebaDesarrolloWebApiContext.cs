using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pruebaDesarrolloWebApi.Models;

namespace pruebaDesarrolloWebApi.Data
{
    public class pruebaDesarrolloWebApiContext : DbContext
    {
        public pruebaDesarrolloWebApiContext (DbContextOptions<pruebaDesarrolloWebApiContext> options)
            : base(options)
        {
        }

        public DbSet<pruebaDesarrolloWebApi.Models.CanalServicio> CanalServicio { get; set; }

        public DbSet<pruebaDesarrolloWebApi.Models.Agencias> Agencias { get; set; }

        public DbSet<pruebaDesarrolloWebApi.Models.Usuarios> Usuarios { get; set; }

        public DbSet<pruebaDesarrolloWebApi.Models.TipoCliente> TipoCliente { get; set; }

        public DbSet<pruebaDesarrolloWebApi.Models.Clientes> Clientes { get; set; }

        public DbSet<pruebaDesarrolloWebApi.Models.TipoTransaccion> TipoTransaccion { get; set; }

        public DbSet<pruebaDesarrolloWebApi.Models.MotivoTransaccion> MotivoTransaccion { get; set; }

        public DbSet<pruebaDesarrolloWebApi.Models.Transacciones> Transacciones { get; set; }
    }
}

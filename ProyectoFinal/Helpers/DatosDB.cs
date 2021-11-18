using System;
using System.Collections.Generic;
using System.Linq;
using ProyectoFinal.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ProyectoFinal.Helpers
{
    public class DatosDB : DbContext
    {
        public DbSet<Cliente> clientes { get; set; }
        public DbSet<Rol> roles { get; set; }
        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<Categoria> categorias { get; set; }
        public DbSet<Producto> productos { get; set; }
        public DbSet<Venta> ventas { get; set; }
        public DbSet<DetallesVenta> detallesventas { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string Conexion = @"Server = apiproyecto.database.windows.net;
                                Database = ProyFinal;
                                User = erionero;
                                Password = Er135ick;";
            optionsBuilder.UseSqlServer(Conexion);
        }
    }
}

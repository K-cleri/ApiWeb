using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal.Models
{
    public class Producto
    {
        public int ID { get; set; }
        public int IDCategoria { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Existencia { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public Producto(int iDCategoria, string codigo, string nombre, decimal precioVenta, int existencia, string descripcion, string estado)
        {
            IDCategoria = iDCategoria;
            Codigo = codigo;
            Nombre = nombre;
            PrecioVenta = precioVenta;
            Existencia = existencia;
            Descripcion = descripcion;
            Estado = estado;
        }
    }
}

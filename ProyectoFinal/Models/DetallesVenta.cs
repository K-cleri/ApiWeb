using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal.Models
{
    public class DetallesVenta
    {
        public int ID { get; set; }
        public int IDVenta { get; set; }
        public int IDProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
        public string Estado { get; set; }
        public DetallesVenta(int iDVenta, int iDProducto, int cantidad, decimal precio, decimal descuento, string estado)
        {
            IDVenta = iDVenta;
            IDProducto = iDProducto;
            Cantidad = cantidad;
            Precio = precio;
            Descuento = descuento;
            Estado = estado;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal.Models
{
    public class Venta
    {
        public int ID { get; set; }
        public int IDCliente { get; set; }
        public int IDUsuario { get; set; }
        public string NumFactura { get; set; }
        public DateTime FechaHora { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        public Venta(int iDCliente, int iDUsuario, string numFactura, DateTime fechaHora, decimal impuesto, decimal total)
        {
            IDCliente = iDCliente;
            IDUsuario = iDUsuario;
            NumFactura = numFactura;
            FechaHora = fechaHora;
            Impuesto = impuesto;
            Total = total;
        }
    }
}

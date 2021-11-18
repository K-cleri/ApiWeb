using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal.Models
{
    public class Cliente
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Rfc { get; set; }
        public string Estado { get; set; }
        public Cliente(string nombre,string direccion,string telefono,string email, string rfc, string estado)
        {
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
            Email = email;
            Rfc = rfc;
            Estado = estado;
        }
    }
}

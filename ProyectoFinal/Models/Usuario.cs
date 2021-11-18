using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal.Models
{
    public class Usuario
    {
        public int ID { get; set; }
        public int IDRol { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Estado { get; set; }
        
        public Usuario(int iDRol, string nombre, string direccion, string telefono, string email, string estado)
        {
            IDRol = iDRol;
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
            Email = email;
            Estado = estado;            
        }
    }
}

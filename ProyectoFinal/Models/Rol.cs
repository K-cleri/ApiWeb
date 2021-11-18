using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal.Models
{
    public class Rol
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public Rol(string nombre, string descripcion,string estado)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            Estado = estado;
        }
    }
}

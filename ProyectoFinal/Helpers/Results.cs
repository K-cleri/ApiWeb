using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal.Helpers
{
    public class Results
    {
        public bool Estado { get; set; }
        public string Mensaje { get; set; }
        public object Datos { get; set; }
        public Results()
        {
            Estado = true;
            Mensaje = "--- Sin Error ---";
            Datos = null;
        }
        public Results(bool estado, string mensaje, object datos)
        {
            this.Estado = estado;
            this.Mensaje = mensaje;
            this.Datos = datos;
        }
    }
}

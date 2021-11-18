using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal.Helpers
{
    public class Exceptions : ApplicationException
    {
        public Exceptions(string Mensaje) : base(Mensaje)
        {
        }
    }
}

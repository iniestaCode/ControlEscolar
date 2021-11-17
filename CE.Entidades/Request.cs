using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE.Entidades
{
    public class Request <T>
    {

        public bool Exito { get; set; }

        public string Mensaje { get; set; }

        public T Respuesta { get; set; }

        public string Error { get; set; }

        public Request()
        {
            Exito = true;
            Mensaje = string.Empty;
            Respuesta = default(T);
            Error = string.Empty;
        }
    }
}

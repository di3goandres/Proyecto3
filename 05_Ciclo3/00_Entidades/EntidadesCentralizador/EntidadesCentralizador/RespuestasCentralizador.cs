using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Centralizador.Entity
{
    /// <summary>
    /// Clase que representa las respuestas posibles que se enviaran desde el centralizador
    /// </summary>
    public class RespuestasCentralizador
    {

        public bool Exitoso { get; set; }

        public bool Existe { get; set; }

        public bool MismoOperador { get; set; }

        public bool Error { get; set; }

        public String Message { get; set; }

        
    }
}

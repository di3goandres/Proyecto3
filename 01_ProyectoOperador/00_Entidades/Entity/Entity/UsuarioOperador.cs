using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Operador.Entity
{
    public class UsuarioOperador
    {
        public long id { get; set; }
        public string userIdApplicacion { get; set; }
        public string userIdCentralizador { get; set; }
        public string respositorioKey { get; set; }
        public string CarpetaInicial { get; set; }

    }
}

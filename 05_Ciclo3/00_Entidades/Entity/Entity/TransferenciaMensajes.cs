using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Operador.Entity
{
    public class TransferenciaMensajes
    {
        public List<ListaDestinatarios> destinatarios { get; set; }
        public ListaDestinatarios Origen { get; set; }
        public String Asunto { get; set; }
        public String Mensaje { get; set; }
        public List<Archivo> archivo { get; set; }
        public string NombreEnvia { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Operador.Entity
{
    public class TransferenciaMensajes
    {
        [XmlElementAttribute(Order = 6)]
        public List<ListaDestinatarios> destinatarios { get; set; }
        [XmlElementAttribute(Order = 4)]

        public ListaDestinatarios Origen { get; set; }
        [XmlElementAttribute(Order = 1)]
        public String Asunto { get; set; }
        [XmlElementAttribute(Order = 2)]

        public String Mensaje { get; set; }

        [XmlElementAttribute(Order = 5)]


        public List<Archivo> archivos { get; set; }
        [XmlElementAttribute(Order = 3)]

        public string NombreEnvia { get; set; }

    }
}

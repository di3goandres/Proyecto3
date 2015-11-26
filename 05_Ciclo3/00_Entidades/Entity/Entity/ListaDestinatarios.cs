using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Operador.Entity
{
    public class ListaDestinatarios
    {

        [XmlElementAttribute(Order = 2)]

        public int tipoIdentificacion { get; set; }
        [XmlElementAttribute(Order = 1)]

        public string NumeroIdentificacion { get; set; }

     
    }
}

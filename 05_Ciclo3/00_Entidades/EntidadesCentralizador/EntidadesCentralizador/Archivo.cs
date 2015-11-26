using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Centralizador.Entity
{

    public class Archivo
    {
        [XmlElementAttribute(Order = 2)]
        public string Contenido { get; set; }


        [XmlElementAttribute(Order = 6)]
        public string NombreArchivo { get; set; }

        [XmlElementAttribute(Order = 4)]
        public Nullable<DateTime> FechaExpedicionArchivo { get; set; }

        [XmlElementAttribute(Order = 3)]
        public Nullable<DateTime> FechaCargueArchivo { get; set; }
        
        
        [XmlElementAttribute(Order = 1)]
        public string ArchivoExpedidoPor { get; set; }


        [XmlElementAttribute(Order = 5)]
        public Nullable<DateTime> FechaVigencia { get; set; }

    }
}

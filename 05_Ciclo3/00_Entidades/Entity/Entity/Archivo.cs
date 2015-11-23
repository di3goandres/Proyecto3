using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Operador.Entity
{
   public class Archivo
    {
       public string Contenido { get; set; }
       public string NombreArchivo { get; set; }
       public Nullable<DateTime> FechaExpedicionArchivo { get; set; }
       public Nullable<DateTime> FechaCargueArchivo { get; set; }
       public string ArchivoExpedidoPor { get; set; }
       public Nullable<DateTime> FechaVigencia { get; set; }

    }
}

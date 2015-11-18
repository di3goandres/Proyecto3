using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Operador.Entity
{
    public class MetadataArchivos
    {
        public Guid idDMtadataArchivo { get; set; }
        public string userIdApplicacion { get; set; }
        public Nullable<Decimal> idCarpetaPersonal { get; set; }
        public string nombre { get; set; }
        public string nombre_generado { get; set; }

        public string extension { get; set; }
        public string autor { get; set; }
        public DateTime fecha_cargue { get; set; }
        public DateTime fecha_modificacion { get; set; }

        public Decimal idTipoDocumento { get; set; }
        public string tamanio { get; set; }
    }
}

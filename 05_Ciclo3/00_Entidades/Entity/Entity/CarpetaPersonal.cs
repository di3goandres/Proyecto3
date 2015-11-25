using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Operador.Entity
{
    public class CarpetaPersonal
    {
        public Decimal IdCarpetaPersonal { get; set; }
        public String userIdAplicacion { get; set; }
        public Nullable<Decimal> idCarpetaPadre { get; set; }
        public String NombreCarpeta { get; set; }
        public String PathTotal { get; set; }
        public Nullable<Boolean> esSistema { get; set; }

       public  List<CarpetaPersonal> Hijos { get; set; }


    }
}

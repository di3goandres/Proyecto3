using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uniandes.Utilidades;

namespace Uniandes.Controlador
{
    

    public partial class OperadorDataContext
    {
        public OperadorDataContext()
            : base("SPUniandesConnectionString".GetFromConnStrings(), mappingSource)
        {
            OnCreated();

            ///Se habilita el servicio de log de Linq, el valor del writer será escrito 
            ///posteriormente en un registro de auditoria, si la transacción iniciada con
            ///la instancia del contexto representa un cambio sobre la base de datos.
            System.IO.TextWriter Writer = new System.IO.StringWriter();
            this.Log = Writer;
        }
    }
}

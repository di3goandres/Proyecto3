using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uniandes.Utilidades
{

    /// <summary>
    /// Entidad que representa la estructura de la información de cada fila enviada a los grids.
    /// </summary>
    public class GridRow
    {
        /// <summary>
        /// Identificador de la fila
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Información de la fila (cada item en la lista representa un registro de una columna específica)
        /// </summary>
        public List<object> cell { get; set; }
    }
}

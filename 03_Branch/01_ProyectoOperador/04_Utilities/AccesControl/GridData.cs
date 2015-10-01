using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uniandes.Utilidades
{
    /// <summary>
    /// Enumeración que representa estados de la página.
    /// </summary>
    public enum Status { OK = 1, OK_WITH_MESSAGES = 2, INVALID = 3 }

    /// <summary>
    /// Entidad que representa la información enviada a los grids.
    /// </summary>
    public class GridData
    {
        /// <summary>
        /// Número de página actual.
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// Número de páginas totales disponibles.
        /// </summary>
        public int total { get; set; }

        /// <summary>
        ///  Cantidad total de registros 
        /// </summary>
        public int records { get; set; }

        /// <summary>
        /// Mensajes a mostrar al usuario.
        /// </summary>
        public string userMessage { get; set; }

        /// <summary>
        /// Mensajes técnicos enviados al log.
        /// </summary>
        public string logMessage { get; set; }

        /// <summary>
        /// El estado de la consulta.
        /// </summary>
        public Status status { get; set; }

        /// <summary>
        /// Conjunto de filas enviadas como respuesta.
        /// </summary>
        public List<GridRow> rows { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Operador.Entity
{
    public class Operacion
    {
        /// <summary>
        /// Identificador único de la operación
        /// </summary>
        public int ID_OPERACION { get; set; }

        /// <summary>
        /// Nombre de la operación
        /// </summary>
        public string NOMBRE { get; set; }

        /// <summary>
        /// Dirección de la página a la cuál una operación puede hacer referencia
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// Identificador de la operación que engloba a esta operación
        /// </summary>
        public Nullable<int> ID_OPERACION_PADRE { get; set; }

        /// <summary>
        /// Dirección de la página de ayuda de esta operación
        /// </summary>
        public string AYUDA { get; set; }

        ///// <summary>
        ///// Identificador del tipo de la operación, puede ser "consulta" o "transacción"
        ///// </summary>
        //public int IdTipoOperacion { get; set; }

        /// <summary>
        /// Conjunto de operaciones hijas de esta operación
        /// </summary>
        public List<Operacion> Hijos { get; set; }

        /// <summary>
        /// Valor que indica si la operacion debería ser visible en el menú,
        /// o si por el contrario solo es llamada desde otra página u operación
        /// </summary>
        public string VISIBLE_MENU { get; set; }
    }
}

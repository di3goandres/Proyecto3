using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Operador.Entity
{
    public class BandejaNotificaciones
    {

        /// <summary>
        /// Lista destintarios por tipo y numero de identificacion
        /// </summary>
        public List<ListaDestinatarios> DestintatariosList { get; set; }
       
        public string NombreEnvia/*         */ { get; set; }

        /// <summary>
        /// identificado por el tipo de identificacion y el numero de indentificacion
        /// CC1077845378
        /// </summary>
        public string userIdAplicacionOrigen/*        */ { get; set; }
        /// <summary>
        /// del mismo modo como el usuario que envia
        /// solo que separados por ","
        /// </summary>
        public string Destinatarios    /*        */ { get; set; }
        public DateTime fechaEnvio /*        */{ get; set; }
        public string Mensaje /*             */{ get; set; }
        public string Asunto /*             */{ get; set; }
        public bool Adjunto /*               */{ get; set; }
        public int Estado /*                 */{ get; set; }
        public string tamanio /*             */{ get; set; }
    }
}

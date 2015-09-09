using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uniandes.Entity;

namespace Uniandes.Centralizador.AccesoDatos.Menu
{
    public class MapeadorOperaciones
    {
        public static Operacion MapOperacionesToBizEntity(OPERACION operacion)
        {

            return new Operacion

            {
                ID_OPERACION = operacion.ID_OPERACION,
                NOMBRE = operacion.NOMBRE,
                URL = operacion.URL,
                ID_OPERACION_PADRE = operacion.ID_OPERACION_PADRE,
                VISIBLE_MENU = operacion.VISIBLE_MENU

            };

        }


        public static OPERACION MapOperacionesFromBizEntity(Operacion operacion)
        {
            return new OPERACION
            {
                ID_OPERACION = operacion.ID_OPERACION,
                NOMBRE = operacion.NOMBRE,
                URL = operacion.URL,
                ID_OPERACION_PADRE = operacion.ID_OPERACION_PADRE,
                VISIBLE_MENU = operacion.VISIBLE_MENU
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Operador.Entity;

namespace Uniandes.Controlador
{

    public class MapeadorCarpetaPersonal
    {

        public static CarpetaPersonal MapCarpetaToBizEntity(tblCarpetaPersonal entidad)
        {

            return new CarpetaPersonal

            {

                IdCarpetaPersonal = entidad.idCarpetaPersonal,
                idCarpetaPadre = entidad.idCarpetaPadre == null ? default(decimal) :Convert.ToDecimal( entidad.idCarpetaPadre),
                NombreCarpeta = entidad.NombreCarpeta,
                userIdAplicacion = entidad.userIdApplicacion,
            };

        }


        public static tblCarpetaPersonal MapCarpetaFromBizEntity(CarpetaPersonal entidad)
        {
            return new tblCarpetaPersonal

            {

                idCarpetaPersonal = entidad.IdCarpetaPersonal,
                idCarpetaPadre = entidad.idCarpetaPadre == null ? default(decimal) : Convert.ToDecimal(entidad.idCarpetaPadre),
                NombreCarpeta = entidad.NombreCarpeta,
                userIdApplicacion = entidad.userIdAplicacion,
            };
        }

    }
}

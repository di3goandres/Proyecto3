using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Operador.Entity;

namespace Uniandes.Controlador
{
    

      public class MapeadorMetadataArchivos
    {

        public static MetadataArchivos MapToBizEntity(tbl_metadataArchivos entidad)
        {

            return new MetadataArchivos

            {

                idDMtadataArchivo = entidad.idDMtadataArchivo,
                idCarpetaPersonal = entidad.idCarpetaPersonal == 0 ? default(decimal) :Convert.ToDecimal( entidad.idCarpetaPersonal),
                nombre = entidad.nombre,
                nombre_generado = entidad.nombre_generado,
                extension = entidad.extension,
                autor = entidad.autor,
                fecha_cargue = entidad.fecha_Cargue,
                fecha_modificacion = entidad.fecha_modificacion == null ? default(DateTime) : Convert.ToDateTime(entidad.fecha_modificacion),
                userIdApplicacion = entidad.userIdApplicacion,
                idTipoDocumento = entidad.idTipoDocumento,
                tamanio = entidad.tamanio


            };

        }


        public static tbl_metadataArchivos MapFromBizEntity(MetadataArchivos entidad)
        {
             return new tbl_metadataArchivos

            {


                idDMtadataArchivo = entidad.idDMtadataArchivo,
                idCarpetaPersonal = entidad.idCarpetaPersonal == null   ? default(decimal) :Convert.ToDecimal( entidad.idCarpetaPersonal),
                nombre = entidad.nombre,
                nombre_generado = entidad.nombre_generado,
                extension = entidad.extension,
                autor = entidad.autor,
                fecha_Cargue = entidad.fecha_cargue,
                fecha_modificacion = entidad.fecha_modificacion == null ? default(DateTime) : Convert.ToDateTime(entidad.fecha_modificacion),
                idTipoDocumento = entidad.idTipoDocumento,
                userIdApplicacion = entidad.userIdApplicacion,
                tamanio = entidad.tamanio


            };
        }

    }
}



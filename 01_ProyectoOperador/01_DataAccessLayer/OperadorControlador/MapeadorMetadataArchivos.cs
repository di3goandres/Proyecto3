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
                idCarpetaPersonal = entidad.idCarpetaPersonal == null ? default(decimal) :Convert.ToDecimal( entidad.idCarpetaPersonal),
                nombre = entidad.nombre,
                extension = entidad.extension,
                autor = entidad.autor,
                fecha_cargue = entidad.fecha_cargue,
                idTipoDocumento = entidad.idTipoDocumento,
                tamanio = entidad.tamanio


            };

        }


        public static tbl_metadataArchivos MapFromBizEntity(MetadataArchivos entidad)
        {
             return new tbl_metadataArchivos

            {

                idDMtadataArchivo = entidad.idDMtadataArchivo,
                idCarpetaPersonal = entidad.idCarpetaPersonal == null ? default(decimal) :Convert.ToDecimal( entidad.idCarpetaPersonal),
                nombre = entidad.nombre,
                extension = entidad.extension,
                autor = entidad.autor,
                fecha_cargue = entidad.fecha_cargue,
                idTipoDocumento = entidad.idTipoDocumento,
                tamanio = entidad.tamanio


            };
        }

    }
}



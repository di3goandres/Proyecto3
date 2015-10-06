using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Operador.Entity;

namespace Uniandes.Controlador
{
    public class MetadataArchivoDao
    {

        /// <summary>
        /// Obtiene los archivos que se enecuentran en una determinada ubicacion.
        /// </summary>
        /// <param name="idCarpetaPersonal"></param>
        /// <returns></returns>
        public List<MetadataArchivos> ObtenerArchivosPorCarpetasDeUsuario(long? idCarpetaPersonal)
        {

            List<MetadataArchivos> Resultados = new List<MetadataArchivos>();


            try
            {
                using (OperadorDataContext ctx = new OperadorDataContext())
                {

                    // tblCarpetaPersonal cpersonal = new tblCarpetaPersonal();
                    //si se pasa null trae los padres de lo contrario traera los Hijos
                    if (idCarpetaPersonal == null)
                    {
                        var cPersonal = (from cp in ctx.tbl_metadataArchivos
                                         where
                                          cp.idCarpetaPersonal == (decimal?)null
                                         select cp);

                        if (cPersonal.Any())
                        {
                            foreach (var operacion in cPersonal)
                            {
                                Resultados.Add(MapeadorMetadataArchivos.MapToBizEntity(operacion));
                            }
                        }
                    }
                    else
                    {

                        var cPersonal = (from cp in ctx.tbl_metadataArchivos
                                         where
                                          cp.idCarpetaPersonal == idCarpetaPersonal
                                         select cp);


                        if (cPersonal.Any())
                        {
                            foreach (var operacion in cPersonal)
                            {
                                Resultados.Add(MapeadorMetadataArchivos.MapToBizEntity(operacion));
                            }
                        }

                    }

                }



                return Resultados;


            }
            catch (Exception e)
            {
                throw e;
            }


        }

        public bool RegistrarMetadataArchivo(MetadataArchivos entidadSubir)
        {

            try
            {
                using (OperadorDataContext ctx = new OperadorDataContext())
                {

                    var entidad = MapeadorMetadataArchivos.MapFromBizEntity(entidadSubir);
                    ctx.tbl_metadataArchivos.InsertOnSubmit(entidad);
                    ctx.SubmitChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}

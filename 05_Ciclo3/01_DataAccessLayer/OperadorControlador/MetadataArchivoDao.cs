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
        public List<MetadataArchivos> ObtenerArchivosPorCarpetasDeUsuario(long? idCarpetaPersonal, decimal idcarpeta)
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
                                          && cp.idCarpetaPersonal != idcarpeta
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

        /// <summary>
        /// Metodo para registrar los metadatos de un archivo en la base de datos.
        /// </summary>
        /// <param name="entidadSubir"></param>
        /// <returns></returns>
        public bool RegistrarMetadataArchivo(MetadataArchivos entidadSubir)
        {

            try
            {
                using (OperadorDataContext ctx = new OperadorDataContext())
                {

                   
                    entidadSubir.fecha_modificacion = DateTime.Now;
                    entidadSubir.idCarpetaPersonal = entidadSubir.idCarpetaPersonal == 0 ? null : entidadSubir.idCarpetaPersonal;
                    var entidad = MapeadorMetadataArchivos.MapFromBizEntity(entidadSubir);
                    entidad.idCarpetaPersonal = entidad.idCarpetaPersonal == 0 ? null : entidadSubir.idCarpetaPersonal;
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




        /// <summary>
        /// Metodo para registrar los metadatos de un archivo en la base de datos.
        /// </summary>
        /// <param name="entidadSubir"></param>
        /// <returns></returns>
        public List<tbl_metadataArchivos> RegistrarListaMetadataArchivo(List<tbl_metadataArchivos> entidadSubir)
        {

            try
            {
                using (OperadorDataContext ctx = new OperadorDataContext())
                {
                    
                    ctx.tbl_metadataArchivos.InsertAllOnSubmit(entidadSubir);
                    ctx.SubmitChanges();

                    return entidadSubir;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


     


        /// <summary>
        /// Metodo para obtener todos los archivos de un usuario en especifico.
        /// </summary>
        /// <param name="UIDuser"></param>
        /// <returns></returns>
        public List<MetadataArchivos> ObtenerArchivosPorUsuario(string UIDuser, decimal idCarpeta)
        {

            List<MetadataArchivos> Resultados = new List<MetadataArchivos>();


            try
            {
                using (OperadorDataContext ctx = new OperadorDataContext())
                {

                    // tblCarpetaPersonal cpersonal = new tblCarpetaPersonal();
                    //si se pasa null trae los padres de lo contrario traera los Hijos

                    var cPersonal = (from cp in ctx.tbl_metadataArchivos
                                     where
                                      cp.userIdApplicacion == UIDuser
                                      && cp.idCarpetaPersonal != idCarpeta
                                      || cp.idCarpetaPersonal == (decimal?)null
                                     select cp);

                    if (cPersonal.Any())
                    {
                        foreach (var operacion in cPersonal)
                        {
                            Resultados.Add(MapeadorMetadataArchivos.MapToBizEntity(operacion));
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

        /// <summary>
        /// Metodo para actualizar el nombre de un archivo.
        /// </summary>
        /// <param name="archivo"></param>
        /// <returns></returns>
        public bool ActualizarNombreArchivo(MetadataArchivos archivo)
        {

            try
            {
                using (OperadorDataContext ctx = new OperadorDataContext())
                {


                    var datos = (from cp in ctx.tbl_metadataArchivos
                                 where cp.idDMtadataArchivo == archivo.idDMtadataArchivo
                                 select cp).FirstOrDefault();

                    if (datos != null)
                    {
                        datos.nombre = archivo.nombre;
                        datos.fecha_modificacion = DateTime.Now;
                        ctx.Refresh(System.Data.Linq.RefreshMode.KeepCurrentValues, datos);
                        ctx.SubmitChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        /// <summary>
        /// Obtiene la metadata de archivo en especifico
        /// </summary>
        /// <param name="uidMetadata"></param>
        /// <returns></returns>
        public MetadataArchivos obtenerMetadata(string uidMetadata)
        {

            MetadataArchivos Resultados = new MetadataArchivos();


            try
            {
                using (OperadorDataContext ctx = new OperadorDataContext())
                {

                    // tblCarpetaPersonal cpersonal = new tblCarpetaPersonal();
                    //si se pasa null trae los padres de lo contrario traera los Hijos

                    Guid metadata = new Guid(uidMetadata);
                    var cPersonal = (from cp in ctx.tbl_metadataArchivos
                                     where
                                      cp.idDMtadataArchivo == metadata
                                     select cp);


                    if (cPersonal.Any())
                    {
                        foreach (var operacion in cPersonal)
                        {
                            Resultados = (MapeadorMetadataArchivos.MapToBizEntity(operacion));
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

    }
}

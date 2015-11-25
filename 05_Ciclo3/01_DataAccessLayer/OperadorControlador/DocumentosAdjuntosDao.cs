using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uniandes.Controlador
{
    public class DocumentosAdjuntosDao
    {
        public List<tblDocumentosAdjuntos> RegistrarListaMetadataArchivo(List<tblDocumentosAdjuntos> entidadSubir)
        {

            try
            {
                using (OperadorDataContext ctx = new OperadorDataContext())
                {
                    ctx.tblDocumentosAdjuntos.InsertAllOnSubmit(entidadSubir);
                    ctx.SubmitChanges();

                    return entidadSubir;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<tbl_metadataArchivos> obtnerArchivos(decimal idnotificacion)
        {


            try
            {
                List<tbl_metadataArchivos> retorno = new List<tbl_metadataArchivos>();
                using (OperadorDataContext ctx = new OperadorDataContext())
                {


                    var resultado = (from cp in ctx.tblDocumentosAdjuntos
                                     where
                                      cp.idBandejaNotificaciones == idnotificacion
                                     select cp.tbl_metadataArchivos);

                    if (resultado.Any())
                    {
                        retorno = resultado.ToList();
                    }
                    return retorno;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        public List<tbl_metadataArchivos> obtnerArchivos(decimal idnotificacion, int Pagina, int NumeroDeregistros, ref int Total)
        {


            try
            {
                List<tbl_metadataArchivos> retorno = new List<tbl_metadataArchivos>();
                using (OperadorDataContext ctx = new OperadorDataContext())
                {


                    var resultado = (from cp in ctx.tblDocumentosAdjuntos
                                     where
                                      cp.idBandejaNotificaciones == idnotificacion
                                     select cp.tbl_metadataArchivos);
                    int SKIP = (Pagina - 1) * NumeroDeregistros;
                    Total = resultado.Count();
                    if (resultado.Any())
                    {
                        resultado = resultado.Skip(SKIP).Take(NumeroDeregistros);
                        retorno = resultado.ToList();
                    }
                    return retorno;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}

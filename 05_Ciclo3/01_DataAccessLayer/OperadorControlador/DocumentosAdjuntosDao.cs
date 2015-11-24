using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uniandes.Controlador
{
     public  class DocumentosAdjuntosDao
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

    }
}

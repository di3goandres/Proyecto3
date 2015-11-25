using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uniandes.Controlador;
using Uniandes.Utilidades;

namespace Uniandes.GestorLogicaOperador
{
    public class GestorDocumentosAdjuntos
    {

        public List<tbl_metadataArchivos> obtenerAdjuntos(decimal idnotificacion, int Pagina, int NumeroDeregistros, ref int Total)
        {
            try
            {
                DocumentosAdjuntosDao daoDOcumentosAdjuntos = new DocumentosAdjuntosDao();

                var resultado = daoDOcumentosAdjuntos.obtnerArchivos(idnotificacion, Pagina, NumeroDeregistros, ref Total);

                return resultado;
            }
            catch (Exception ex)
            {

                AppLog.Write(" Error Gestor documentos Adjuntos obtenerAdjuntos", AppLog.LogMessageType.Error, ex, "OperadorCarpeta");
                throw ex;
            }
        }
    }
}

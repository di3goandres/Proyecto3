using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Uniandes.GestorLogicaOperador;
using Uniandes.Utilidades;

namespace ServicioRecepcionMensajeria
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class RecepcionDocumentos : IRecepcionDocumentos
    {

        public bool RecibirMensajes(Operador.Entity.TransferenciaMensajes mensajes)
        {
            try
            {
                Boolean resultado = true;
                GestorMensajeria gestorMensajeria = new GestorMensajeria();
                 resultado = gestorMensajeria.RecibirMensaje(mensajes);
                return resultado;
            }
            catch (Exception ex) {
                AppLog.Write(" Error en el servicio De Mensajeria  - RecibirMensajes . ", AppLog.LogMessageType.Error, ex, "OperadorCarpeta");
                throw ex;
            
            }

        }


        public bool RecibirMensajesString(string mensajes)
        {
            try
            {
                Boolean resultado = true;
                GestorMensajeria gestorMensajeria = new GestorMensajeria();
                //resultado = gestorMensajeria.RecibirMensaje(mensajes);
                return resultado;
            }
            catch (Exception ex)
            {
                AppLog.Write(" Error en el servicio De Mensajeria  - RecibirMensajes . ", AppLog.LogMessageType.Error, ex, "OperadorCarpeta");
                throw ex;

            }
        }

       
    }
}

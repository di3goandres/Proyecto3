using Operador.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uniandes.Utilidades;

namespace Uniandes.Controlador
{
    public class BandejaNotificacionesDAO
    {
        public List<tblBandejaNotificaciones> ObtenerPlanes(int Pagina, int NumeroDeregistros, ref int Total, string Userid)
        {
            try
            {
                List<tblBandejaNotificaciones> listaRetorno = new List<tblBandejaNotificaciones>();
                using (OperadorDataContext ctx = new OperadorDataContext())
                {

                    var DATOS = (from c in ctx.tblBandejaNotificaciones
                                 where c.userIdAplicacionDestino == Userid
                                 select c);
                    int SKIP = (Pagina - 1) * NumeroDeregistros;
                    Total = DATOS.Count();
                    if (DATOS.Any())
                    {
                        DATOS = DATOS.OrderByDescending(x => x.fechaEnvio).Skip(SKIP).Take(NumeroDeregistros);
                        listaRetorno = DATOS.ToList();
                    }
                }

                return listaRetorno;
            }
            catch (Exception ex)
            {
                AppLog.Write(" Error obteniendo datos de planes.", AppLog.LogMessageType.Error, ex, "OperadorCarpeta");
                throw;
            }
        }


        /// <summary>
        /// Metodo para enviar un mensaje interno 
        /// </summary>
        /// <param name="mensaje"></param>
        /// <returns>Lista de Notificaciones que fueron enviadas</returns>
        public List<tblBandejaNotificaciones> EnviarMensaje(List<tblBandejaNotificaciones> listaInsertMensajes)
        {
            try
            {

               
                using (OperadorDataContext ctx = new OperadorDataContext())
                {

                    ctx.tblBandejaNotificaciones.InsertAllOnSubmit(listaInsertMensajes);
                    ctx.SubmitChanges();
                }
                return listaInsertMensajes;

               
            }
            catch (Exception ex)
            {
                AppLog.Write(" Error obteniendo datos de planes.", AppLog.LogMessageType.Error, ex, "OperadorCarpeta");
                throw;
            }

        }
    }
}

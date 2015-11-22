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
                        DATOS = DATOS.Skip(SKIP).Take(NumeroDeregistros);
                        listaRetorno = DATOS.ToList();
                    }
                }

                return listaRetorno;
            }
            catch (Exception ex)
            {
                AppLog.Write(" Error obteniendo datos de planes.", AppLog.LogMessageType.Error, ex, "BansatLog");
                throw;
            }
        }

        public bool EnviarMensaje(BandejaNotificaciones mensaje)
        {
            try
            {

                List<tblBandejaNotificaciones> listaInsertMensajes = new List<tblBandejaNotificaciones>();
                var enviarA = mensaje.DestintatariosList;
                string IdUsuarios = string.Empty;
                var fechaEnvio = DateTime.Now;
                foreach (var data in enviarA)
                {

                    IdUsuarios = new DaoUsuario().obtenerIdentficadorUnicoUsuario(data.tipoIdentificacion, data.NumeroIdentificacion);

                    listaInsertMensajes.Add(new tblBandejaNotificaciones() {
                        idBandejaNotificacionPadre = null,
                        userIdAplicacionDestino = IdUsuarios,
                        NombreEnvia = mensaje.NombreEnvia, 
                        userIdAplicacionOrigen = mensaje.userIdAplicacionOrigen,
                        Destinatarios = mensaje.Destinatarios,
                        fechaEnvio = fechaEnvio,
                        Mensaje =  mensaje.Mensaje,
                        Asunto = mensaje.Asunto,
                        Estado = 1,
                        tamanio = mensaje.tamanio

                    });


                }





                return true;
            }
            catch (Exception ex)
            {
                AppLog.Write(" Error obteniendo datos de planes.", AppLog.LogMessageType.Error, ex, "BansatLog");
                throw;
            }

        }
    }
}

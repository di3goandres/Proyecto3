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

        public bool EnviarMensaje(TransferenciaMensajes mensaje)
        {
            try
            {

                List<tblBandejaNotificaciones> listaInsertMensajes = new List<tblBandejaNotificaciones>();
                var enviarA = mensaje.destinatarios;
                string IdUsuarios = string.Empty;
                string idOrigen = string.Empty;
                bool adjunto = mensaje.archivo.Count() > 0 ? true : false;
                var fechaEnvio = DateTime.Now;
                foreach (var data in enviarA)
                {

                    IdUsuarios = new DaoUsuario().obtenerIdentficadorUnicoUsuario(data.tipoIdentificacion, data.NumeroIdentificacion);
                    idOrigen = new DaoUsuario().obtenerIdentficadorUnicoUsuario(mensaje.Origen.tipoIdentificacion, mensaje.Origen.NumeroIdentificacion);
                    listaInsertMensajes.Add(new tblBandejaNotificaciones()
                    {
                        idBandejaNotificacionPadre = null,
                        userIdAplicacionDestino = IdUsuarios,
                        NombreEnvia = mensaje.NombreEnvia,
                        userIdAplicacionOrigen = idOrigen,
                        Destinatarios = "",//mensaje.Destinatarios,
                        fechaEnvio = fechaEnvio,
                        Mensaje = mensaje.Mensaje,
                        Asunto = mensaje.Asunto,
                        Estado = 1,
                        tamanio = "",// mensaje.tamanio
                        Adjunto = adjunto
                    });


                }
                using (OperadorDataContext ctx = new OperadorDataContext())
                {



                    ctx.tblBandejaNotificaciones.InsertAllOnSubmit(listaInsertMensajes);
                    ctx.SubmitChanges();
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

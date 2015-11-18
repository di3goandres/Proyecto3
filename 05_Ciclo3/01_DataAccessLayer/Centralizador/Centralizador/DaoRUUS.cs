using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Centralizador.Entity;
using Uniandes.Utilidades;

namespace Centralizador.DAO
{
    public class DaoRUUS
    {

        /// <summary>
        /// valida la existencia del usuario por numero de identificacion y tipo de identificacion.
        /// </summary>
        /// <param name="identificacionUsuario"></param>
        /// <param name="idTipoIdentificacion"></param>
        /// <returns>RespuestasCentralizador</returns>
        public RespuestasCentralizador validarExisteUsuarios(String identificacionUsuario, int idTipoIdentificacion, string IdentificadorCarpetaColombiana)
        {
            RespuestasCentralizador retorno = new RespuestasCentralizador();
            try
            {

                AppLog.Write(" Ingrese validarExisteUsuarios ", AppLog.LogMessageType.Info, null, "CentralizadorColombiano");
                AppLog.Write(string.Format(" datos tipo identificacion: {0}, numero de identificacion {1}.", idTipoIdentificacion.ToString(), identificacionUsuario), AppLog.LogMessageType.Info, null, "CentralizadorColombiano");

                using (CentralizadorDataContext ctx = new CentralizadorDataContext())
                {
                    var usuario = (from efp in ctx.tb005_RRUS
                                   where efp.numeroIdentificacion == identificacionUsuario
                                   && efp.idTipoIdentificacion == idTipoIdentificacion
                                   select efp);
                    if (usuario.Any())
                    {

                        var UIDCarpeta = usuario.First().tb006_OPERADOR.unicoIdentificador.ToString().ToUpper();
                        if (UIDCarpeta.Equals(IdentificadorCarpetaColombiana.ToUpper()))
                            retorno.MismoOperador = true;
                        else
                            retorno.MismoOperador = false;
                        retorno.Exitoso = true;
                        retorno.Existe = true;

                        //si existen 
                        return retorno;
                    }
                    else
                    {
                        retorno.Existe = false;
                        retorno.MismoOperador = false;
                        retorno.Message = "Sin operador Asignado";
                        return retorno;
                    }
                }
            }
            catch (Exception e)
            {
                AppLog.Write(" Error validarExisteUsuarios ", AppLog.LogMessageType.Error, e, "CentralizadorColombiano");
                retorno.Exitoso = false;

                throw e;
            }


        }


        /// <summary>
        /// Metodo para registrar un usuario en el Centralizador
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public Usuario RegistrarUsuario(Usuario usuario, string IdentificadorCarpetaciudadana)
        {

            try
            {
                using (CentralizadorDataContext ctx = new CentralizadorDataContext())
                {

                    var datos = validarExisteUsuarios(usuario.numeroIdentificacion, usuario.idTipoIdentificacion, IdentificadorCarpetaciudadana);

                    if (datos.Existe)
                    {
                        return null;

                    }
                    else
                    {
                        var entidad = MapeadorUsuario.MapUsuarioFromBizEntity(usuario);
                        Guid ownerIdGuid = Guid.Empty;
                        ownerIdGuid = new Guid(IdentificadorCarpetaciudadana);
                        var IdcarpetaCiudadana = (from cc in ctx.tb006_OPERADOR
                                                  where cc.unicoIdentificador == ownerIdGuid
                                                  select cc.idOperador).First();
                        entidad.idOperador = IdcarpetaCiudadana;
                        ctx.tb005_RRUS.InsertOnSubmit(entidad);
                        ctx.SubmitChanges();

                        var retorno = MapeadorUsuario.MapUsuarioToBizEntity(entidad);
                        return retorno;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}

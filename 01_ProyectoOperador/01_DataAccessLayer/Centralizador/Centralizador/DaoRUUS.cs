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
        /// <returns></returns>
        public bool validarExisteUsuarios(String identificacionUsuario, int idTipoIdentificacion)
        {

            try
            {
                AppLog.Write(" Ingrese validarExisteUsuarios ", AppLog.LogMessageType.Info, null, "CentralizadorColombiano");
                AppLog.Write(string.Format(" datos tipo identificacion: {0}, numero de identificacion {1}.",  idTipoIdentificacion.ToString(),identificacionUsuario) , AppLog.LogMessageType.Info, null, "CentralizadorColombiano");

                using (CentralizadorDataContext ctx = new CentralizadorDataContext())
                {
                    var usuario = (from efp in ctx.tb005_RRUS
                                   where efp.numeroIdentificacion == identificacionUsuario
                                   && efp.idTipoIdentificacion == idTipoIdentificacion
                                   select efp);
                    if (usuario.Any())
                    {
                        //si existen 
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                AppLog.Write(" Error validarExisteUsuarios ", AppLog.LogMessageType.Error, e, "CentralizadorColombiano");
                return false;
            }


        }


        /// <summary>
        /// Metodo para registrar un usuario en el Centralizador
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public Usuario RegistrarUsuario(Usuario usuario)
        {

            try
            {
                using (CentralizadorDataContext ctx = new CentralizadorDataContext())
                {

                    bool existe = validarExisteUsuarios(usuario.numeroIdentificacion, usuario.idTipoIdentificacion);

                    if (existe)
                    {
                        return null;

                    }
                    else
                    {
                        var entidad = MapeadorUsuario.MapUsuarioFromBizEntity(usuario);

                      
                        ctx.tb005_RRUS.InsertOnSubmit(entidad);
                        ctx.SubmitChanges();
                       
                        var retorno = MapeadorUsuario.MapUsuarioToBizEntity(entidad);
                        return retorno;
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
    }
}

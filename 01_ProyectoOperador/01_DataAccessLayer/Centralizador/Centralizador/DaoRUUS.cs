using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Centralizador.Entity;

namespace Centralizador.DAO
{
    public class DaoRUUS
    {

        public bool validarExisteUsuarios(String identificacionUsuario, int idTipoIdentificacion)
        {

            try
            {
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
                return false;
            }


        }


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

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

                registrarLog("VAUS", IdentificadorCarpetaColombiana, "VALIDACION REGISTRO USUARIOS");
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
                        registrarLog("REUS", IdentificadorCarpetaciudadana, "Registro Usuarios en el sistema");
                        return retorno;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        /// <summary>
        /// Metodo para registrar un usuario en el Centralizador
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public Usuario ConsultarUsuario(String identificadoUsuario, string IdentificadorCarpetaciudadana)
        {

            try
            {
                Guid ownerIdGuid = Guid.Empty;
                ownerIdGuid = new Guid(identificadoUsuario);
                Usuario retorno = new Usuario();
                using (CentralizadorDataContext ctx = new CentralizadorDataContext())
                {


                    var entidad = (from cc in ctx.tb005_RRUS
                                   where cc.UID == ownerIdGuid
                                   select cc);


                    if (entidad.Any())
                    {
                        foreach (var data in entidad)
                        {
                            retorno = MapeadorUsuario.MapUsuarioToBizEntity(data);
                        }

                        registrarLog("COUS", IdentificadorCarpetaciudadana, "CONSULTA DE USUARIO EN EL SISTEMA");
                    }
                    return retorno;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }




        /// <summary>
        /// Metodo para actualizar los datos del usuario
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="IdentificadorCarpetaciudadana"></param>
        /// <returns></returns>
        public bool ActualizarDatosUsuario(Usuario entity, string IdentificadorCarpetaciudadana)
        {

            try
            {
                StringBuilder Comentarios = new StringBuilder();
                using (CentralizadorDataContext ctx = new CentralizadorDataContext())
                {
                    var oldEntity = (from c in ctx.tb005_RRUS
                                     where c.UID.ToString() == entity.UUID
                                     select c).FirstOrDefault();
                    if (oldEntity != null && oldEntity.idRRUS != 0)
                    {
                        if (oldEntity.idMunicipioResidencia != entity.idMunicipioResidencia)
                        {
                            oldEntity.idMunicipioResidencia = entity.idMunicipioResidencia;
                            Comentarios = Comentarios.AppendLine("Se Actualiza Municipio de Residencia");
                        }


                        if (oldEntity.direccionResidencia != entity.direccionResidencia)
                        {
                            oldEntity.direccionResidencia = entity.direccionResidencia;
                            Comentarios = Comentarios.AppendLine("Se Actualiza dirección de Residencia");
                        }
                        if (oldEntity.idMunicipioNotificacionCorrespondencia != entity.idMunicipioNotificacionCorrespondencia)
                        {
                            oldEntity.idMunicipioNotificacionCorrespondencia = entity.idMunicipioNotificacionCorrespondencia;
                            Comentarios = Comentarios.AppendLine("Se Actualiza Municipio de Correspondencia");
                        }
                        if (oldEntity.direccionNotificacionCorrespondencia != entity.direccionNotificacionCorrespondencia)
                        {
                            oldEntity.direccionNotificacionCorrespondencia = entity.direccionNotificacionCorrespondencia;
                            Comentarios = Comentarios.AppendLine("Se Actualiza dirección  de Correspondencia");
                        }
                        if (oldEntity.telefono != entity.telefono)
                        {
                            oldEntity.telefono = entity.telefono;
                            Comentarios = Comentarios.AppendLine("Se Actualiza Telefono");
                        }
                        oldEntity.telefono = entity.telefono;

                        ctx.Refresh(System.Data.Linq.RefreshMode.KeepCurrentValues, oldEntity);
                        ctx.SubmitChanges();
                    }

                    registrarLog("ACDA", IdentificadorCarpetaciudadana, Comentarios.ToString());
                    return true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }




        /// <summary>
        /// Metodo para registrar el log en el centralizador.
        /// </summary>
        /// <param name="Codigo"></param>
        /// <param name="idOperadors"></param>
        /// <param name="Comentarios"></param>
        private void registrarLog(String Codigo, String idOperadors, String Comentarios)
        {
            try
            {
                using (CentralizadorDataContext ctx = new CentralizadorDataContext())
                {
                    Guid ownerIdGuid = Guid.Empty;
                    ownerIdGuid = new Guid(idOperadors);

                    var idOperador = (from op in ctx.tb006_OPERADOR
                                      where op.unicoIdentificador == ownerIdGuid
                                      select op.idOperador).First();

                    var codigoLog = (from coLog in ctx.tb007_TIPOS_AUDITORIA
                                     where coLog.codAuditori == Codigo
                                     select coLog.idAuditoria).First();

                    tb008_LOG_AUDITORIA auditoria = new tb008_LOG_AUDITORIA();
                    auditoria.comentarios = Comentarios;
                    auditoria.fechaLog = DateTime.Now;
                    auditoria.idOperador = idOperador;
                    auditoria.idAuditoria = codigoLog;
                    ctx.tb008_LOG_AUDITORIA.InsertOnSubmit(auditoria);
                    ctx.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}

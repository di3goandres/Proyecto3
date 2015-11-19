using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Centralizador.DAO;
using Centralizador.Entity;
using Uniandes.Utilidades;

namespace ServicioRUUS
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {

        public Usuario RegistrarUsuario(Usuario usuario, String IndentificadorCarpeta)
        {

            try
            {
                DaoRUUS dao = new DaoRUUS();
                return dao.RegistrarUsuario(usuario, IndentificadorCarpeta);
            }
            catch (Exception ee)
            {
                AppLog.Write(" Error RegistrarUsuario ", AppLog.LogMessageType.Error, ee, "CentralizadorColombiano");
                throw ee;

            }

        }


        public RespuestasCentralizador ValidarExistenciaUsuario(Usuario usuario, String IndentificadorCarpeta)
        {
            RespuestasCentralizador retorno = new RespuestasCentralizador();
            try
            {
                DaoRUUS dao = new DaoRUUS();
                return dao.validarExisteUsuarios(usuario.numeroIdentificacion, usuario.idTipoIdentificacion, IndentificadorCarpeta);
            }
            catch (Exception ex)
            {
                retorno.Exitoso = false;
                retorno.Message = ex.Message;
                return retorno;

            }
        }

        /// <summary>
        /// Metodo para consultar
        /// </summary>
        /// <param name="numeroIdentificacion"></param>
        /// <param name="idTipoIdentificacion"></param>
        /// <returns>RespuestasCentralizador</returns>
        public RespuestasCentralizador ValidarPorIdentificacionYTipo(string numeroIdentificacion, int idTipoIdentificacion, String IndentificadorCarpeta)
        {
            RespuestasCentralizador retorno = new RespuestasCentralizador();
            try
            {
                DaoRUUS dao = new DaoRUUS();
                return dao.validarExisteUsuarios(numeroIdentificacion, idTipoIdentificacion, IndentificadorCarpeta);
            }
            catch(Exception ex)
            {
                retorno.Exitoso = false;
                retorno.Message = ex.Message;
                return retorno;

            }
        }


        public Usuario ConsultarUsuario(string identificadoUsuario, string IdentificadorCarpetaciudadana)
        {
            try
            {
                DaoRUUS dao = new DaoRUUS();
                return dao.ConsultarUsuario(identificadoUsuario, IdentificadorCarpetaciudadana);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}

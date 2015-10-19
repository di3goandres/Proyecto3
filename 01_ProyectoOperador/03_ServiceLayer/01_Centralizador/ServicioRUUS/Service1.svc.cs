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

        public Usuario RegistrarUsuario(Usuario usuario)
        {

            try
            {
                DaoRUUS dao = new DaoRUUS();
                return dao.RegistrarUsuario(usuario);
            }
            catch (Exception ee)
            {
                AppLog.Write(" Error RegistrarUsuario ", AppLog.LogMessageType.Error, ee, "CentralizadorColombiano");
                throw ee;

            }

        }


        public bool ValidarExistenciaUsuario(Usuario usuario)
        {

            try
            {
                DaoRUUS dao = new DaoRUUS();
                return dao.validarExisteUsuarios(usuario.numeroIdentificacion, usuario.idTipoIdentificacion);
            }
            catch
            {

                return false;

            }
        }

        /// <summary>
        /// Metodo para consultar
        /// </summary>
        /// <param name="numeroIdentificacion"></param>
        /// <param name="idTipoIdentificacion"></param>
        /// <returns></returns>
        public bool ValidarPorIdentificacionYTipo(string numeroIdentificacion, int idTipoIdentificacion)
        {

            try
            {
                DaoRUUS dao = new DaoRUUS();
                return dao.validarExisteUsuarios(numeroIdentificacion, idTipoIdentificacion);
            }
            catch
            {

                return false;

            }
        }
    }
}

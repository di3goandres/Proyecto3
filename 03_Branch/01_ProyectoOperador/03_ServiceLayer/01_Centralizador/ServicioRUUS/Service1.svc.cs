﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Centralizador.DAO;
using Centralizador.Entity;

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
    }
}

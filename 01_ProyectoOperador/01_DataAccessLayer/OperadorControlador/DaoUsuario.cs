﻿using Operador.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Uniandes.Utilidades;

namespace Uniandes.Controlador
{
    public class DaoUsuario
    {
        /// <summary>
        /// Metodo para registrar el usuario ante el centralizador
        /// </summary>
        /// <param name="app"></param>
        /// <param name="centralizador"></param>
        /// <param name="repositorioKey"></param>
        /// <param name="carpetaUsuarioInicial"></param>
        /// <returns></returns>
        public int RegistrarUsuario(String app, String centralizador, String repositorioKey, String carpetaUsuarioInicial)
        {

            try
            {
                Conexion conn = new Conexion();
                SqlConnection cnn = conn.getSqlConnection();
                SqlCommand cmd = new SqlCommand("registrar_usuario_app", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                #region(Crear parametros del procedimiento almacenado)
                cmd.Parameters.Add("@userIdApplicacion", SqlDbType.VarChar);
                cmd.Parameters.Add("@userIdCentralizador", SqlDbType.VarChar);
                cmd.Parameters.Add("@repositorioKey", SqlDbType.VarChar);
                cmd.Parameters.Add("@carpetaUsuarioInicial", SqlDbType.VarChar);




                #endregion

                #region(Pasar parametros del procedimiento almacenado)

                cmd.Parameters["@userIdApplicacion"].Value /*   */= app;
                cmd.Parameters["@userIdCentralizador"].Value/* */ = centralizador;
                cmd.Parameters["@repositorioKey"].Value /*   */= repositorioKey;
                cmd.Parameters["@carpetaUsuarioInicial"].Value/* */ = carpetaUsuarioInicial;




                #endregion

                cmd.Connection.Open();
                int i = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return i;

            }
            catch (Exception ex) {
                AppLog.Write(" Error Creacion Usuario", AppLog.LogMessageType.Error, ex, "OperadorCarpeta");
                throw ex;
            
            }


        }



        public UsuarioOperador ObtnerUsuario(string UUID)
        {

            UsuarioOperador retorno = new UsuarioOperador();

            string fullName = string.Empty;
            using (OperadorDataContext ctx = new OperadorDataContext())
            {


                var cUsuarios = (from cp in ctx.tbl_usuarios
                                 where cp.userIdApplicacion == UUID
                                 select cp);


                if (cUsuarios.Any())
                {
                    retorno.respositorioKey = cUsuarios.First().repositorioKey;
                    retorno.userIdApplicacion = cUsuarios.First().userIdApplicacion;
                    retorno.userIdCentralizador = cUsuarios.First().userIdCentralizador;
                    retorno.CarpetaInicial = cUsuarios.First().carpetaUsuarioInicial;

                }
            }

            return retorno;
        
        }




    }
}

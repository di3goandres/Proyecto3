using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Uniandes.Controlador
{
    public class DaoUsuario
    {
        public int RegistrarUsuario(String app, String centralizador, String repositorioKey, String carpetaUsuarioInicial)
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
    }
}

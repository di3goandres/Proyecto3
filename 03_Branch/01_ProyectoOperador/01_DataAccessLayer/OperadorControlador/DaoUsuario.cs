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
        public int RegistrarUsuario(String app, String centralizador)
        {

            Conexion conn = new Conexion();
            SqlConnection cnn = conn.getSqlConnection();
            SqlCommand cmd = new SqlCommand("sp_registrar_paciente", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            #region(Crear parametros del procedimiento almacenado)
            cmd.Parameters.Add("@userIdApplicacion", SqlDbType.VarChar);
            cmd.Parameters.Add("@userIdCentralizador", SqlDbType.VarChar);
           


            #endregion

            #region(Pasar parametros del procedimiento almacenado)

            cmd.Parameters["@userIdApplicacion"].Value /*   */= app;
            cmd.Parameters["@userIdCentralizador"].Value/* */ = centralizador;
          



            #endregion

            cmd.Connection.Open();
            int i = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return i;



        }
    }
}

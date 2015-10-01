using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Operador.Entity;

namespace Uniandes.Controlador
{
    public class TipoidentificacionDao
    {
        public List<TipoIdentificacion> obtenerTipos()
        {
            List<TipoIdentificacion> retorno = new List<TipoIdentificacion>();
            Conexion conn = new Conexion();
            SqlConnection cnn = conn.getSqlConnection();
            SqlCommand cmd = new SqlCommand("sp_obtener_tipos_identificacion", cnn);
            cmd.CommandType = CommandType.StoredProcedure;




            cnn.Open();


            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {

                TipoIdentificacion entidad = new TipoIdentificacion();
                entidad.id_tipoId = Convert.ToInt32(dr["id_tipoId"].ToString());
                entidad.nombre_tipoId = dr["nombre_tipoId"].ToString();
                retorno.Add(entidad);
            }
            dr.Close();
            return retorno;
        }
    }
}

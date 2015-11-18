using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Operador.Entity;

namespace Uniandes.Controlador
{
    public class DepartamentosDao
    {
        public List<Departamento> ObtenerDepartamentos() {



            List<Departamento> retorno = new List<Departamento>();
            Conexion conn = new Conexion();
            SqlConnection cnn = conn.getSqlConnection();
            SqlCommand cmd = new SqlCommand("sp_consulta_departamentos", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
         
            cnn.Open();


            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
         
            while (dr.Read())
            {

                Departamento entidad = new Departamento();
                entidad.IdDepartamento = Convert.ToInt32(dr["idDepartamento"].ToString());
                entidad.IdPais = Convert.ToInt32(dr["IdPais"].ToString());
                entidad.NombreDepartamento = dr["NombreDepartamento"].ToString();
                entidad.CodigoDANE = dr["CodigoDANE"].ToString();
                retorno.Add(entidad);
            }
            dr.Close();
            return retorno;
        
        }


        public List<Municipio> ObtenerMunicipiosDepartamentos(int IdDepartamento)
        {



            List<Municipio> retorno = new List<Municipio>();
            Conexion conn = new Conexion();
            SqlConnection cnn = conn.getSqlConnection();
            SqlCommand cmd = new SqlCommand("sp_obtener_municipios_departamento", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@idDepartamento", SqlDbType.Int);
          

            cmd.Parameters["@idDepartamento"].Value = IdDepartamento;
            cnn.Open();


            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {

                Municipio entidad = new Municipio();
                entidad.IdDepartamento = Convert.ToInt32(dr["idDepartamento"].ToString());
                entidad.IdMunicipio = Convert.ToInt32(dr["IdMunicipio"].ToString());
                entidad.NombreMunicipio = dr["NombreMunicipio"].ToString();
                entidad.CodigoDANE = dr["CodigoDANE"].ToString();
                retorno.Add(entidad);
            }
            dr.Close();
            return retorno;

        }


        public List<Pais> obtenerPaises()
        {



            List<Pais> retorno = new List<Pais>();
            Conexion conn = new Conexion();
            SqlConnection cnn = conn.getSqlConnection();
            SqlCommand cmd = new SqlCommand("sp_consulta_pais", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cnn.Open();


            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {

                Pais entidad = new Pais();
                entidad.IdPais = Convert.ToInt32(dr["idPais"].ToString());
                entidad.NombrePais = dr["nombrePais"].ToString();
                entidad.CodigoDane = dr["codigoDANE"].ToString();
                retorno.Add(entidad);
            }
            dr.Close();
            return retorno;

        }


    }
}

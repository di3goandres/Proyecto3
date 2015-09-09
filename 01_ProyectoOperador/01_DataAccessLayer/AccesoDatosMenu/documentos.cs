using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Centralizador.Entity;

namespace Uniandes.Centralizador.AccesoDatos.Menu
{
    public class documentos
    {
    //    public bool insertar(string contenido, string extension, string nombre)
    //    {
    //        try
    //        {





    //            SqlConnection connection = new SqlConnection();
    //            connection.ConnectionString = ConfigurationManager.ConnectionStrings["GestorDocumental"].ConnectionString;
    //            SqlCommand cmd = new SqlCommand();


    //            SqlDataReader reader;




    //            string query = "insert into documentos (contenido, nombre, extension) values ('{0}', '{1}', '{2}')";
    //            string pasar = string.Format(query, contenido, nombre, extension);

    //            cmd.CommandText = pasar;

    //            cmd.CommandType = CommandType.Text;
    //            cmd.Connection = connection;


    //            connection.Open();

    //            reader = cmd.ExecuteReader();
    //            connection.Close();

    //            return true;
    //        }
    //        catch (Exception ex)
    //        {

    //            throw new Exception("Error tratando de Obtener listado de OperacionesPorPerfil.", ex);
    //        }
    //    }

    //    public Documento consultar(string idDocumento)
    //    {
    //        try
    //        {

    //            Documento retorno = new Documento();




    //            SqlConnection connection = new SqlConnection();
    //            connection.ConnectionString = ConfigurationManager.ConnectionStrings["GestorDocumental"].ConnectionString;
    //            SqlCommand cmd = new SqlCommand();


    //            SqlDataReader reader;




    //            string query = "Select  contenido, nombre, extension from Documentos where idDocumento ='{0}'";

    //            string pasar = string.Format(query, idDocumento);


    //            cmd.CommandText = pasar;

    //            cmd.CommandType = CommandType.Text;
    //            cmd.Connection = connection;
    //            connection.Open();
    //            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);


    //            while (dr.Read())
    //            {


    //                retorno.contenido = (dr["contenido"].ToString());
    //                retorno.nombre = dr["nombre"].ToString();
    //                retorno.extension = dr["extension"].ToString();

    //            }


    //            return retorno;
    //        }
    //        catch (Exception ex)
    //        {

    //            throw new Exception("Error tratando de Obtener listado de OperacionesPorPerfil.", ex);
    //        }
    //    }
    }
}

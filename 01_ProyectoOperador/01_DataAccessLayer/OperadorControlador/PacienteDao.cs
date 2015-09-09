
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Uniandes.Entity;

namespace Uniandes.Controlador
{
    public class PacienteDao
    {

        /// <summary>
        /// metodo para consultar todos los pacientes que existen en el momento en la base de datos
        /// </summary>
        /// <param name="paginaActual">Pagina actual, inicia en cero</param>
        /// <param name="TamanioPagina"></param>
        /// <param name="TotalRegistros"></param>
        /// <returns></returns>

        public List<Paciente> obtenerPacientes(int paginaActual, int TamanioPagina, ref int TotalRegistros)
        {

            List<Paciente> retorno = new List<Paciente>();
            Conexion conn = new Conexion();
            SqlConnection cnn = conn.getSqlConnection();
            SqlCommand cmd = new SqlCommand("sp_consultar_pacientes", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PageSize", SqlDbType.Int);
            cmd.Parameters.Add("@PageNumber", SqlDbType.Int);

            cmd.Parameters["@PageSize"].Value = TamanioPagina;
            cmd.Parameters["@PageNumber"].Value = paginaActual;

            cnn.Open();


            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            bool primera = true;
            while (dr.Read())
            {
                if (primera)
                {
                    TotalRegistros = Convert.ToInt32(dr["TotalRows"].ToString());
                    primera = false;
                }
                Paciente entidad = new Paciente();
                entidad.id_paciente = Convert.ToInt32(dr["id_paciente"].ToString());
                entidad.nombres_paciente = dr["nombres_paciente"].ToString();

                entidad.apellidos_paciente = dr["apellidos_paciente"].ToString();
                entidad.ident_paciente = dr["ident_paciente"].ToString();
                entidad.tipo_id = Convert.ToInt32(dr["tipo_id"].ToString());
                entidad.genero_paciente = Convert.ToInt32(dr["genero_paciente"].ToString());
                entidad.fecha_nacimiento = Convert.ToDateTime(dr["fecha_nacimiento"].ToString());
                entidad.fecha_registro = Convert.ToDateTime(dr["fecha_registro"].ToString());
                entidad.direccion_paciente = dr["direccion_paciente"].ToString();
                entidad.telefono_paciente = dr["telefono_paciente"].ToString();
                entidad.movil_paciente = dr["movil_paciente"].ToString();
                entidad.mail_paciente = dr["mail_paciente"].ToString();

                retorno.Add(entidad);
            }
            dr.Close();
            return retorno;

        }
        public List<Paciente> obtenerPacientes(int paginaActual, int TamanioPagina, ref int TotalRegistros, string NumeroIdentificacion)
        {

            List<Paciente> retorno = new List<Paciente>();
            Conexion conn = new Conexion();
            SqlConnection cnn = conn.getSqlConnection();
            SqlCommand cmd = new SqlCommand("sp_consultar_pacientes_identificacion", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PageSize", SqlDbType.Int);
            cmd.Parameters.Add("@PageNumber", SqlDbType.Int);
            cmd.Parameters.Add("@Identificacion", SqlDbType.VarChar);


            cmd.Parameters["@PageSize"].Value = TamanioPagina;
            cmd.Parameters["@PageNumber"].Value = paginaActual;
            cmd.Parameters["@Identificacion"].Value = NumeroIdentificacion;


            cnn.Open();


            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            bool primera = true;
            while (dr.Read())
            {
                if (primera)
                {
                    TotalRegistros = Convert.ToInt32(dr["TotalRows"].ToString());
                    primera = false;
                }
                Paciente entidad = new Paciente();
                entidad.id_paciente = Convert.ToInt32(dr["id_paciente"].ToString());
                entidad.nombres_paciente = dr["nombres_paciente"].ToString();

                entidad.apellidos_paciente = dr["apellidos_paciente"].ToString();
                entidad.ident_paciente = dr["ident_paciente"].ToString();
                entidad.tipo_id = Convert.ToInt32(dr["tipo_id"].ToString());
                entidad.genero_paciente = Convert.ToInt32(dr["genero_paciente"].ToString());
                entidad.fecha_nacimiento = Convert.ToDateTime(dr["fecha_nacimiento"].ToString());
                entidad.fecha_registro = Convert.ToDateTime(dr["fecha_registro"].ToString());
                entidad.direccion_paciente = dr["direccion_paciente"].ToString();
                entidad.telefono_paciente = dr["telefono_paciente"].ToString();
                entidad.movil_paciente = dr["movil_paciente"].ToString();
                entidad.mail_paciente = dr["mail_paciente"].ToString();

                retorno.Add(entidad);
            }
            dr.Close();
            return retorno;

        }

        public int registrarPacienteNuevo(Paciente pacienteNuevo)
        {

            Conexion conn = new Conexion();
            SqlConnection cnn = conn.getSqlConnection();
            SqlCommand cmd = new SqlCommand("sp_registrar_paciente", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            #region(Crear parametros del procedimiento almacenado)
            cmd.Parameters.Add("@nombres_paciente", SqlDbType.VarChar);
            cmd.Parameters.Add("@apellidos_paciente", SqlDbType.VarChar);
            cmd.Parameters.Add("@ident_paciente", SqlDbType.VarChar);
            cmd.Parameters.Add("@tipo_id", SqlDbType.Int);
            cmd.Parameters.Add("@genero_paciente", SqlDbType.Int);
            //cmd.Parameters.Add("@fecha_nacimiento", SqlDbType.Date);
            cmd.Parameters.Add("@direccion_paciente", SqlDbType.VarChar);
            cmd.Parameters.Add("@telefono_paciente", SqlDbType.VarChar);
            cmd.Parameters.Add("@movil_paciente", SqlDbType.VarChar);
            cmd.Parameters.Add("@mail_paciente", SqlDbType.VarChar);
            cmd.Parameters.Add("@userId", SqlDbType.VarChar);


            #endregion

            #region(Pasar parametros del procedimiento almacenado)

            cmd.Parameters["@nombres_paciente"].Value /*   */= pacienteNuevo.nombres_paciente;
            cmd.Parameters["@apellidos_paciente"].Value/* */ = pacienteNuevo.apellidos_paciente;
            cmd.Parameters["@ident_paciente"].Value/*     */ = pacienteNuevo.ident_paciente;
            cmd.Parameters["@tipo_id"].Value/*            */ = pacienteNuevo.tipo_id;
            cmd.Parameters["@genero_paciente"].Value/*   */  = pacienteNuevo.genero_paciente;
            //cmd.Parameters["@fecha_nacimiento"].Value/*   */ = pacienteNuevo.fecha_nacimiento;
            cmd.Parameters["@direccion_paciente"].Value/* */ = pacienteNuevo.direccion_paciente;
            cmd.Parameters["@telefono_paciente"].Value/*  */ = pacienteNuevo.telefono_paciente;
            cmd.Parameters["@movil_paciente"].Value/*     */ = pacienteNuevo.movil_paciente;
            cmd.Parameters["@mail_paciente"].Value/*      */ = pacienteNuevo.mail_paciente;
            cmd.Parameters["@userId"].Value/*             */ = pacienteNuevo.userId;



            #endregion

            cmd.Connection.Open();
            int i = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return i;



        }

        public int sp_registrar_episodio_paciente(string idpaciente, int idintensidad, int duracion)
        {

            Conexion conn = new Conexion();
            SqlConnection cnn = conn.getSqlConnection();
            SqlCommand cmd = new SqlCommand("sp_registrar_episodio_paciente", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            #region(Crear parametros del procedimiento almacenado)
            cmd.Parameters.Add("@idpaciente", SqlDbType.VarChar);
            cmd.Parameters.Add("@idintensidad", SqlDbType.Int);
            cmd.Parameters.Add("@duracion", SqlDbType.Int);
            SqlParameter parm2 = new SqlParameter("@new_identity", SqlDbType.Int);
            parm2.Size = 50;
            parm2.Direction = ParameterDirection.Output; // This is important!
            cmd.Parameters.Add(parm2);
         

            #endregion

            #region(Pasar parametros del procedimiento almacenado)

            cmd.Parameters["@idpaciente"].Value /*   */= idpaciente;
            cmd.Parameters["@idintensidad"].Value/* */ = idintensidad;
            cmd.Parameters["@duracion"].Value/*     */ = duracion;
            



            #endregion

            cmd.Connection.Open();
            cmd.ExecuteNonQuery();

            int ans = (int)cmd.Parameters["@new_identity"].Value;
            cmd.Connection.Close();
            return ans;
        
        }
    }

}
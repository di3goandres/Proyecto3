using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Uniandes.Utilidades;

namespace Uniandes.Controlador
{

    public class Conexion
    {
        SqlConnection connection = new SqlConnection();
        String connectionString = string.Empty;

    

        public Conexion()
        {
            connectionString = "SPUniandesConnectionString".GetFromConnStrings();
            connection.ConnectionString = connectionString;
        }

        public string getConnectionString()
        {
            return connectionString;
        }
        public SqlConnection getSqlConnection()
        {
            return connection;
        }
    }
}

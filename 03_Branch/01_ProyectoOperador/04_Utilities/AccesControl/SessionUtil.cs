using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Web.Administration;


namespace Uniandes.Utilidades
{
    public class SessionUtil
    {
        

        
        /// <summary>
        /// Valida si la sesion del usuario aún existe
        /// </summary>
        /// <param name="throwEx">Si es True lanza una excepción EndSessionException</param>
        /// <returns></returns>
        public static bool IsSessionStillActive(bool throwEx)
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["PERFIL_ACTUAL"] == null)
            {
                if (throwEx)
                    throw new EndSessionException();
                else
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Valida si la sesion del usuario aún existe
        /// </summary>
        /// <returns></returns>
        public static bool IsSessionStillActive()
        {
            return IsSessionStillActive(false);
        }

  
        /// <summary>
        /// Obtiene el valor asociado a un campo del DataSet que contiene la información del usuario
        /// </summary>
        /// <typeparam name="T">Tipo de dato de retorno</typeparam>
        /// <param name="columnName">Nombre de la columna del DataTable parameters</param>
        /// <returns></returns>
        public static T GetUserSessionData<T>(string columnName)
        {
            DataSet dtsUser = (DataSet)HttpContext.Current.Session["InfoUser"];
            return dtsUser.Tables["parameters"].Rows[0].Field<T>(columnName);
        }

        /// <summary>
        /// Guarda información en sesion
        /// </summary>
        /// <param name="key">Llave de la sesion</param>
        /// <param name="data">Información a guardar</param>
        public static void SetSessionData(string key, object data)
        {
            HttpContext.Current.Session.Add(key, data);
          
        }

        /// <summary>
        /// Obtiene información de la sesion
        /// </summary>
        /// <param name="key">Llave de la sesion</param>
        public static object GetSessionData(string key)
        {
            if (HttpContext.Current.Session[key] == null) return null;

            return HttpContext.Current.Session[key];
        }

        /// <summary>
        /// Obtiene información de la sesion
        /// </summary>
        /// <param name="key">Llave de la sesion</param>
        public static T GetSessionData<T>(string key)
        {
            if (HttpContext.Current.Session[key] == null) return default(T);

       

            return (T)Convert.ChangeType(HttpContext.Current.Session[key], typeof(T));
        }



    }
    public class EndSessionException : Exception
    {
        public EndSessionException()
            : base("Su Sesión ha finalizado")
        {
        }
    }
}

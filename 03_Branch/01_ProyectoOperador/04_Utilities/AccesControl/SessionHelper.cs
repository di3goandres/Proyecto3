using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Web.Administration;

namespace Uniandes.Utilidades
{
    public class SessionHelper
    {
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



        public static void SetSessionData(string key, object data)
        {
            HttpContext.Current.Session.Add(key, data);
          
        }
    }

}

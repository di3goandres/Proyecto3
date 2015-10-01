using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Xml.Serialization;

namespace Uniandes.Utilidades
{
    public static class ExtensionMethods
    {

        public static byte[] ReadFully(this Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
        /// <summary>
        /// Obtiene el valor de la cadena de coneccion del archivo de configuración 
        /// en la sección de connectionStrings.
        /// </summary>
        /// <returns>Cadena de conección.</returns>
        public static string GetFromConnStrings(this string connectionStringName)
        {
            return ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
        }


        /// <summary>
        /// Obteiene el valor del parámetro del archivo de configuración 
        /// en la sección de appsetings.
        /// </summary>
        /// <returns>String del valor</returns>
        public static string GetFromAppCfg(this string appKey)
        {
            return ConfigurationManager.AppSettings[appKey];
        }

        public static string ToXML<T>( T obj)
        {
            using (StringWriter stringWriter = new StringWriter(new StringBuilder()))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(stringWriter, obj);
                return stringWriter.ToString();
            }
        }

    }

}

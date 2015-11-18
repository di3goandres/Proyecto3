using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Uniandes.Utilidades
{
    public class AppLog
    {
        public enum LogMessageType
        {
            Debug = 0,
            Info = 1,
            Warn = 2,
            Error = 3,
            Fatal = 4,
        }

        public static void Init()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public static void Write(string message, AppLog.LogMessageType messageType, Exception ex, string logger)
        {

            ILog enviar = LogManager.GetLogger(logger);
            StringBuilder datos = new StringBuilder();
            datos.Append(message);
            datos.AppendLine();
            datos.Append(ex != null ? ex.ToString() : "");

            switch (messageType)
            {

                case LogMessageType.Debug:
                    enviar.Debug(datos);
                    break;
                case LogMessageType.Info:
                    enviar.Info(datos);
                    break;
                case LogMessageType.Warn:
                    enviar.Warn(datos);
                    break;
                case LogMessageType.Error:
                    enviar.Error(datos);
                    break;
                case LogMessageType.Fatal:
                    enviar.Fatal(datos);
                    break;
            }
        }


    }
}

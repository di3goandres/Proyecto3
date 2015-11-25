using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uniandes.Controlador;
using Uniandes.Utilidades;

using Uniandes.FileControl;
using Operador.Entity;

namespace Uniandes.GestorRegistro
{
    public class GestorRegistro
    {
        public bool RegistrarUsuario(String app, String centralizador, String repositorioKey,
          String carpetaUsuarioInicial, String nombres,
          String Apellidos, int tipoIdentificacion, String numeroIdentificacion)
        {
            DaoUsuario daoUsuario = new DaoUsuario();
            CarpetaPersonalDao daoCarpeta = new CarpetaPersonalDao();

            try
            {

                daoUsuario.RegistrarUsuario(app, centralizador, repositorioKey,
               carpetaUsuarioInicial, nombres,
               Apellidos, tipoIdentificacion, numeroIdentificacion);

            }
            catch (Exception ex)
            {

                AppLog.Write(" Error Creacion Usuario", AppLog.LogMessageType.Error, ex, "OperadorCarpeta");
                throw ex;
            }

            try
            {

                #region crear carpeta en el servidor
                var fileControl = new Uniandes.FileControl.FileControl(Int32.Parse("MaxFileSize".GetFromAppCfg()));

                fileControl._CreateFolderInFTP(carpetaUsuarioInicial, "OPERADOR_REPOSITORY_USER");
                fileControl._CreateFolderInFTP(carpetaUsuarioInicial + @"/ADJUNTOS", "OPERADOR_REPOSITORY_USER");


                CarpetaPersonal nueva = new CarpetaPersonal();
                nueva.idCarpetaPadre = null;
                nueva.NombreCarpeta = "ADJUNTOS";
                nueva.userIdAplicacion = app;
                daoCarpeta.RegistrarCarpetaPersonal(nueva);

            }
            catch (Exception ex)
            {

                AppLog.Write(" Error Creacion Usuario", AppLog.LogMessageType.Error, ex, "OperadorCarpeta");
                throw ex;

            }
                #endregion

            return true;
        }
    }
}

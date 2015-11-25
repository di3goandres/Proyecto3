using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uniandes.Controlador;
using Uniandes.FileControl;
using Uniandes.Utilidades;

namespace Uniandes.GestorLogicaOperador
{
    public class GestorDescargaArchivo
    {
        public DescargaArchivo obtenerArchivoUsuario(string Userid, string UIDNombreArchivo, int idCarpeta)
        {
            try
            {
                DescargaArchivo retorno = new DescargaArchivo();
                Uniandes.FileControl.FileControl fileControl = new Uniandes.FileControl.FileControl(0);

                DaoUsuario daoUsuario = new DaoUsuario();
                var usuario = daoUsuario.ObtnerUsuario(Userid);
                MetadataArchivoDao daoMetadata = new MetadataArchivoDao();
                var metadata = daoMetadata.obtenerMetadata(UIDNombreArchivo);

                CarpetaPersonalDao cpDao = new CarpetaPersonalDao();
                string path = cpDao.fullPathPorCarpeta(idCarpeta);
                string fullpath = @"" + usuario.CarpetaInicial + @"" + path + @"\\" + metadata.nombre_generado;
                var file = fileControl.GetFileFromFtpRepository(usuario.respositorioKey, fullpath);

                retorno.file = file;
                retorno.nombre = metadata.nombre;

                return retorno;
            }
            catch (Exception ex)
            {
                AppLog.Write(" Error obteniendo un archivo del usuario . ", AppLog.LogMessageType.Error, ex, "OperadorCarpeta");
                throw ex;
            }

        }


        public List<DescargaArchivo> obtenerArchivoAdjuntosBandejaNotificacionUsuario(decimal idNotificacion)
        {
            try
            {
                List<DescargaArchivo> retorno = new List<DescargaArchivo>();


                DocumentosAdjuntosDao daoDOcumentosAdjuntos = new DocumentosAdjuntosDao();

                var resultado = daoDOcumentosAdjuntos.obtnerArchivos(idNotificacion);
                Uniandes.FileControl.FileControl fileControl = new Uniandes.FileControl.FileControl(0);


                foreach (var data in resultado)
                {

                    retorno.Add(obtenerArchivoUsuario(data.userIdApplicacion, data.idDMtadataArchivo.ToString(), (int)data.idCarpetaPersonal));

                }



                return retorno;
            }
            catch (Exception ex)
            {
                AppLog.Write(" Error obteniendo los Adjuntos de un mensaje. ", AppLog.LogMessageType.Error, ex, "OperadorCarpeta");
                throw ex;
            }

        }


        public DescargaArchivo ObtenerAdjunto(String UID)
        {
            try
            {
                DescargaArchivo retorno = new DescargaArchivo();


                DocumentosAdjuntosDao daoDOcumentosAdjuntos = new DocumentosAdjuntosDao();
                MetadataArchivoDao daoMetadata = new MetadataArchivoDao();
                var data = daoMetadata.obtenerMetadata(UID);


                Uniandes.FileControl.FileControl fileControl = new Uniandes.FileControl.FileControl(0);



                if (data != null)
                    retorno = (obtenerArchivoUsuario(data.userIdApplicacion, data.idDMtadataArchivo.ToString(), (int)data.idCarpetaPersonal));





                return retorno;
            }
            catch (Exception ex)
            {
                AppLog.Write(" Error obteniendo los Adjuntos de un mensaje. ", AppLog.LogMessageType.Error, ex, "OperadorCarpeta");
                throw ex;
            }

        }
    }

    public class DescargaArchivo
    {

        public string nombre { get; set; }
        public byte[] file { get; set; }


    }
}

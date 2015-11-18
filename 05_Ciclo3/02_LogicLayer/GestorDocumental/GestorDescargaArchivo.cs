using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uniandes.Controlador;
using Uniandes.FileControl;
using Uniandes.Utilidades;

namespace Uniandes.GestorDocumental
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
                AppLog.Write(" Error obteniendo la informacion Inicial. ", AppLog.LogMessageType.Error, ex, "OperadorCarpeta");
                throw ex;
            }

        }
    }

    public class DescargaArchivo {

        public string nombre { get; set; }
        public byte[] file { get; set; }

    
    }
}

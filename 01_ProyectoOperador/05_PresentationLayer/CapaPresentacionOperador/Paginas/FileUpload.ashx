﻿<%@ WebHandler Language="C#" Class="FileUpload" %>

using System;
using System.Web;
using System.IO;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using AccesControl.Utilidades;

using Uniandes.FileControl;


public class FileUpload : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        JavaScriptSerializer ser = new JavaScriptSerializer();
        HttpFileCollection fileCollection;
        Operador.Entity.MetadataArchivos nuevaMetadata = new Operador.Entity.MetadataArchivos();
        
        try
        {


            context.Response.ContentType = "text/plain";

            ///Se obtienen los archivos.
            ///
            fileCollection = context.Request.Files;
            if (fileCollection.AllKeys.Length <= 0)
            {
                context.Response.Write(ser.Serialize(new { Estado = "ERROR", Mensaje = "No se recibió ningun archivo para cargar." }));
            }
        }
        catch (Exception ex)
        {
            var respuestaError = new { Estado = "ERROR", Mensaje = ex.Message };
            context.Response.Write(ser.Serialize(respuestaError));
            return;
        }
        ///Se obtienen los tipos MIME de sesión, si existen.
        ///
        var mimeTypesObject = SessionHelper.GetSessionData("MIME_TYPES");
        List<string> TiposPermitidos = (mimeTypesObject == null) ? new List<string>() : (List<string>)mimeTypesObject;
        var listaNombresArchivos = new List<FileNameControl>();
        var fileControl = new FileControl(Int32.Parse("MaxFileSize".GetFromAppCfg()));

        try
        {
            ///Se obtiene el nombre de los archivos para utilizarlos en caso de error
            ///
            for (int i = 0; i <= fileCollection.Count - 1; i++)
            {
                HttpPostedFile postedFile = fileCollection[i];
                if (postedFile.ContentLength > 0)
                {
                    var filename = Path.GetFileName(postedFile.FileName);
                    string ext = System.IO.Path.GetExtension(filename).ToLower();
                    nuevaMetadata.extension = ext;
                    nuevaMetadata.fecha_cargue = DateTime.Now;
                    nuevaMetadata.idCarpetaPersonal = 3;

                    nuevaMetadata.idTipoDocumento = 1;
                    nuevaMetadata.nombre = filename;
                    
                    
                    listaNombresArchivos.Add(new FileNameControl() { Original = filename, });
                }
            }
            ///Se crea un objeto de tipo FileControl utilizado para el control de arhivos
            ///
            ///Se suben los archivos y se espera que realice la verificación
            ///
            fileControl.UploadFileTsoScan(fileCollection);
            listaNombresArchivos = fileControl.AntivirusFileNames;
            //var fileLoaded = SessionHelper.GetSessionData("MIME_TYPES");


            fileControl.CopyAntivirusToUserRepositorio("OPERADOR_REPOSITORY_USER", listaNombresArchivos.First().Generated, @"\CC1077845378\Carpeta001\Sub02CarpetaDe001");
            Uniandes.Controlador.MetadataArchivoDao mDataArchibo = new Uniandes.Controlador.MetadataArchivoDao();

            nuevaMetadata.tamanio = listaNombresArchivos.First().tamanioArchivo.ToString();
            nuevaMetadata.autor = "usuario";
            nuevaMetadata.idDMtadataArchivo = Guid.NewGuid();
             mDataArchibo.RegistrarMetadataArchivo(nuevaMetadata);

            var respuesta = new { Estado = "OK", Mensaje = ".", Archivos = listaNombresArchivos };
            context.Response.Write(ser.Serialize(respuesta));
        }
        catch (MaximumSizeExeption ex)
        {
            var respuestaVirus = new { Estado = "SIZE", Mensaje = ex.Message, Archivos = listaNombresArchivos, ArchivosMaximum = ex.FileNames };
            context.Response.Write(ser.Serialize(respuestaVirus));
        }
        catch (InvalidMimeTypesException ex)
        {
            var respuestaVirus = new { Estado = "MIME", Mensaje = ex.Message, Archivos = listaNombresArchivos, ArchivosMime = ex.FileNames };
            context.Response.Write(ser.Serialize(respuestaVirus));
        }
        catch (VirusFileExeption ex)
        {
            var respuestaVirus = new { Estado = "VIRUS", Mensaje = ex.Message, Archivos = listaNombresArchivos, ArchivosVirus = ex.FileNames };
            context.Response.Write(ser.Serialize(respuestaVirus));
        }
        catch (Exception ex)
        {
            var respuestaError = new { Estado = "ERROR", Mensaje = "Ha ocurrido un error cargando.", ErrorMessage = ex.ToString(), Archivos = listaNombresArchivos };
            context.Response.Write(ser.Serialize(respuestaError));
        }

    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
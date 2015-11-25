using Operador.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Uniandes.Controlador;
using Uniandes.Utilidades;

namespace Uniandes.GestorLogicaOperador
{
    public class GestorMensajeria
    {

        public bool EnviarMensaje(TransferenciaMensajes mensajes)
        {
            BandejaNotificacionesDAO dao = new BandejaNotificacionesDAO();
            DocumentosAdjuntosDao daoDocumentosAdjuntos = new DocumentosAdjuntosDao();


            var tipoDocumental = new tipoDocumentalDao().obtenerTipoLogiaDocumental("500001");
            List<tblBandejaNotificaciones> listaInsertMensajes = new List<tblBandejaNotificaciones>();
            var enviarA = mensajes.destinatarios;
            List<tbl_usuarios> ListaUsuarios = new List<tbl_usuarios>();
            tbl_usuarios idOrigen = new tbl_usuarios();
            DaoUsuario daoUsuarios = new DaoUsuario();
            bool adjunto = mensajes.archivo.Count() > 0 ? true : false;
            var fechaEnvio = DateTime.Now;
            idOrigen = daoUsuarios.obtenerIdentficadorUnicoUsuario(mensajes.Origen.tipoIdentificacion, mensajes.Origen.NumeroIdentificacion);
            string Destinos = string.Empty;
            string usuarioOrigen = mensajes.NombreEnvia;
            foreach (var data in enviarA)
            {
                var usuario = daoUsuarios.obtenerIdentficadorUnicoUsuario(data.tipoIdentificacion, data.NumeroIdentificacion);
                Destinos = PrefijoEnumTIPO_IDENTIFICACION.EnumToTIPO_IDENTIFICACION(Convert.ToInt32( usuario.tipoIdentificacion)) + usuario.numeroIdentificacion + ",";

                ListaUsuarios.Add(usuario);

            }
            if (Destinos.EndsWith(","))
            {
                Destinos = Destinos.Substring(0, Destinos.Length - 1);
            }

            foreach (var data in ListaUsuarios)
            {


                listaInsertMensajes.Add(new tblBandejaNotificaciones()
                {
                    idBandejaNotificacionPadre = null,
                    userIdAplicacionDestino = data.userIdApplicacion,
                    NombreEnvia = mensajes.NombreEnvia,
                    userIdAplicacionOrigen = idOrigen.userIdApplicacion,
                    Destinatarios =Destinos,
                    fechaEnvio = fechaEnvio,
                    Mensaje = mensajes.Mensaje,
                    Asunto = mensajes.Asunto,
                    Estado = 1,
                    tamanio = "",// mensaje.tamanio
                    Adjunto = adjunto,
            

                });


            }

            var resultadoInsertarMensaje = dao.EnviarMensaje(listaInsertMensajes);
            #region Guardar Documentos En las carpetas de los usuarios
            var fileControl = new Uniandes.FileControl.FileControl(Int32.Parse("MaxFileSize".GetFromAppCfg()));

            List<tbl_metadataArchivos> listaMetadatos = new List<tbl_metadataArchivos>();

            MetadataArchivoDao daoMetadata = new MetadataArchivoDao();
            CarpetaPersonalDao daoCarpetaPersonal = new CarpetaPersonalDao();
            foreach (var usuarios in ListaUsuarios)
            {
                var idCarpeta = daoCarpetaPersonal.obtenerIdCarpeta(usuarios.userIdApplicacion, "ADJUNTOS");
                listaMetadatos = new List<tbl_metadataArchivos>();
                foreach (var archivos in mensajes.archivo)
                {
                    
                    fileControl.CopyStringByteFileToRepositorio(usuarios.repositorioKey, @"" + usuarios.carpetaUsuarioInicial + "/Adjuntos", archivos.Contenido, archivos.NombreArchivo);
                    string ext = System.IO.Path.GetExtension(archivos.NombreArchivo).ToLower();
                    listaMetadatos.Add(new tbl_metadataArchivos()
                    {
                        idDMtadataArchivo =  Guid.NewGuid(),
                        autor = usuarioOrigen,
                        userIdApplicacion = usuarios.userIdApplicacion,
                        extension = ext,
                        nombre_generado = archivos.NombreArchivo,
                        fecha_Cargue = Convert.ToDateTime(archivos.FechaCargueArchivo),
                        fecha_modificacion = Convert.ToDateTime(archivos.FechaCargueArchivo),
                        idTipoDocumento = tipoDocumental,
                        tamanio = "",
                        nombre = archivos.NombreArchivo,
                        idCarpetaPersonal = idCarpeta
                    });

                }

               var resultadoGuardardatos = daoMetadata.RegistrarListaMetadataArchivo(listaMetadatos);

              

               List<tblDocumentosAdjuntos> almacenarAdjuntos = new List<tblDocumentosAdjuntos>();

               var idMensaje = listaInsertMensajes.Where(x => x.userIdAplicacionDestino == usuarios.userIdApplicacion).First().idBandejaNotificaciones;

               foreach (var lista in listaMetadatos) {

                   almacenarAdjuntos.Add(new tblDocumentosAdjuntos() { 
                    idDMetadaArchivo = lista.idDMtadataArchivo,
                    idBandejaNotificaciones = idMensaje
                   });
               
               }
               daoDocumentosAdjuntos.RegistrarListaMetadataArchivo(almacenarAdjuntos);
            }

            

            #endregion
            return true;
            
        }


        private Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}

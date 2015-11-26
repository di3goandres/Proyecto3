using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Uniandes.Controlador;
using Operador.Entity;
using Uniandes.Utilidades;
using Uniandes.FileControl;
using Uniandes.GestorLogicaOperador;

public partial class Paginas_EnviarMensaje : System.Web.UI.Page
{
    private static string REPOSITORY = "OPERADOR_REPOSITORY_USER";
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    /// <summary>
    /// Metodo para validar la informacion ingresada por el usuario y sera enviada para validar en el centralizador
    /// </summary>
    /// <param name="TIPO_IDENTIFICACION"></param>
    /// <param name="NUMERO_IDENTIFICACION"></param>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object validar(int TIPO_IDENTIFICACION, string NUMERO_IDENTIFICACION)
    {

        Centralizador.Service1Client serviciocentralizador = new Centralizador.Service1Client();
        var IdentificadorOperador = "identificadorOperador".GetFromAppCfg();

        try
        {
            AppLog.Write("Inicio consulta en el centralizador", AppLog.LogMessageType.Info, null, "OperadorCarpeta");
            var completo = PrefijoEnumTIPO_IDENTIFICACION.EnumToTIPO_IDENTIFICACIONCOMPLETO(TIPO_IDENTIFICACION);
            var Existe = serviciocentralizador.ValidarPorIdentificacionYTipo(NUMERO_IDENTIFICACION, TIPO_IDENTIFICACION, IdentificadorOperador);

            if (Existe.Existe)
            {

                SessionHelper.SetSessionData("TIPO_IDENTIFICACION_ENVIO", TIPO_IDENTIFICACION);
                SessionHelper.SetSessionData("NUMERO_IDENTIFICACION_ENVIO", NUMERO_IDENTIFICACION);



                if (Existe.MismoOperador)
                {

                    SessionHelper.SetSessionData("OPERADOR_ENVIO_LOCAL_U_OTRO", true);

                    return new
                    {
                        OK = "OK",
                        mensaje = "Se ha agregado un usuario",
                        NUMERO_IDENTIFICACION = NUMERO_IDENTIFICACION,
                        TIPO_IDENTIFICACION = TIPO_IDENTIFICACION,
                        COMPLETO = completo,
                        MISMO = true

                    };
                }
                else
                {

                    SessionHelper.SetSessionData("OPERADOR_ENVIO_LOCAL_U_OTRO", false);

                    return new
                    {
                        OK = "OK",
                        mensaje = "Se ha agregado un usuario",
                        NUMERO_IDENTIFICACION = NUMERO_IDENTIFICACION,
                        TIPO_IDENTIFICACION = TIPO_IDENTIFICACION,
                        COMPLETO = completo,
                        MISMO = false

                    };
                }

            }
            else
            {

                return new
                {
                    OK = "NO",
                    mensaje = "El usuario al que estas tratando de enviarle un mensaje, no existe en ningun operador registrado",
                    NUMERO_IDENTIFICACION = NUMERO_IDENTIFICACION,
                    TIPO_IDENTIFICACION = TIPO_IDENTIFICACION,
                    COMPLETO = completo,


                };

            }
        }
        catch (Exception ex)
        {
            AppLog.Write(" Error Validando informacion  ", AppLog.LogMessageType.Error, ex, "OperadorCarpeta");

            return new
            {
                OK = "error",
                mensaje = "Ha ocurrido un error inesperador por favor intentelo en un unos instantes",


            };
        }
    }


    /// <summary>
    /// Metodo para enviar mensajes
    /// </summary>
    /// <param name="Asunto"></param>
    /// <param name="cuerpoMensaje"></param>
    /// <param name="FILENAMES"></param>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object EnviarMensaje(List<usuariosMensajes> usuarios, string Asunto, string cuerpoMensaje, string FILENAMES)
    {
        try
        {
            #region Envio Mensajes


           
            ServicioIntermediario.ServicioIntermediarioClient servicioMensajeriaExterno = new ServicioIntermediario.ServicioIntermediarioClient();

            ServicioIntermediario.TransferenciaMensajes MensajeOtroOperadore = new ServicioIntermediario.TransferenciaMensajes();



            List<ServicioIntermediario.ListaDestinatarios> destinosExternos = new List<ServicioIntermediario.ListaDestinatarios>();

            ServicioIntermediario.ListaDestinatarios OrigenExtenos = new ServicioIntermediario.ListaDestinatarios();


            TransferenciaMensajes MENSAJE = new TransferenciaMensajes();
            List<ListaDestinatarios> destino = new List<ListaDestinatarios>();
            List<ServicioIntermediario.Archivo> archivosEnviarExterno = new List<ServicioIntermediario.Archivo>();

            var idTipo = (int)SessionHelper.GetSessionData("TIPO_IDENTIFICACION_ENVIO");
            var numero = (string)SessionHelper.GetSessionData("NUMERO_IDENTIFICACION_ENVIO");


            #region Validacion Usuarios Externos Internos
            /**
             *recorremos toda lista validando los usuarios que son del sistema y los que no 
             */
            foreach (var data in usuarios)
            {

                if (data.mismo)
                {
                    destino.Add(new ListaDestinatarios()
                    {

                        NumeroIdentificacion = data.numeroIdentificacion,
                        tipoIdentificacion = data.idTIpoIdentificacion
                    });
                }
                {

                    destinosExternos.Add(new ServicioIntermediario.ListaDestinatarios()
                    {

                        NumeroIdentificacion = data.numeroIdentificacion,
                        tipoIdentificacion = data.idTIpoIdentificacion
                    });

                }
            }
            #endregion


            List<Archivo> archivosEnviar = new List<Archivo>();
            List<string> archivos = FILENAMES.Split(',').Where(x => string.IsNullOrWhiteSpace(x) == false).ToList();
            var fileControl = new FileControl(Int32.Parse("MaxFileSize".GetFromAppCfg()));
            DateTime FechaEnvio = DateTime.Now;
            byte[] datosArchivo = null;
            foreach (string nombreArchivo in archivos)
            {
                datosArchivo = fileControl.GetFileFromAntivirus(REPOSITORY, nombreArchivo);
                string DatosArchivoString = Convert.ToBase64String(datosArchivo);
                archivosEnviar.Add(new Archivo()
                {
                    Contenido = DatosArchivoString,
                    FechaCargueArchivo = FechaEnvio,
                    FechaExpedicionArchivo = FechaEnvio,
                    FechaVigencia = FechaEnvio,
                    NombreArchivo = nombreArchivo,
                    ArchivoExpedidoPor = "",

                });
            }



            var usuario = (UsuarioOperador)SessionHelper.GetSessionData("USUARIO");

            ListaDestinatarios origien = new ListaDestinatarios();
            origien.tipoIdentificacion = usuario.tipoIdentificacion.Value;
            origien.NumeroIdentificacion = usuario.numeroIdentificacion;


            if (destinosExternos.Count() > 0)
            {


                foreach (var data in archivosEnviar)
                {
                    archivosEnviarExterno.Add(new ServicioIntermediario.Archivo()
                    {
                        Contenido = data.Contenido,
                        FechaCargueArchivo = data.FechaCargueArchivo,
                        FechaExpedicionArchivo = data.FechaExpedicionArchivo,
                        FechaVigencia = data.FechaVigencia,
                        NombreArchivo = data.NombreArchivo,
                        ArchivoExpedidoPor = "",

                    });

                }
                OrigenExtenos.tipoIdentificacion = usuario.tipoIdentificacion.Value;
                OrigenExtenos.NumeroIdentificacion = usuario.numeroIdentificacion;

                MensajeOtroOperadore.archivos = archivosEnviarExterno.ToArray();
               
                MensajeOtroOperadore.Asunto = Asunto;
                MensajeOtroOperadore.Mensaje = cuerpoMensaje;
                MensajeOtroOperadore.destinatarios = destinosExternos.ToArray();
                MensajeOtroOperadore.Origen = OrigenExtenos;
                MensajeOtroOperadore.NombreEnvia = usuario.Nombres + " " + usuario.Apellidos;

                try {

                    var resultado = servicioMensajeriaExterno.RecibirMensajes(MensajeOtroOperadore);
                }
                catch (Exception ex) { 
                
                
                
                }

            }
            MENSAJE.archivo = archivosEnviar;
            MENSAJE.Asunto = Asunto;
            MENSAJE.Mensaje = cuerpoMensaje;
            MENSAJE.destinatarios = destino;
            MENSAJE.Origen = origien;
            MENSAJE.NombreEnvia = usuario.Nombres + " " + usuario.Apellidos;


            GestorMensajeria gestor = new GestorMensajeria();

            var resultadoEnvio = gestor.EnviarMensaje(MENSAJE);





            if (resultadoEnvio)
            {
                return new
                {
                    OK = "OK",
                    mensaje = "Se ha enviado el mensaje a los destinatarios Correctamente."
                };
            }
            else
            {
                return new
                {
                    OK = "fallo",
                    mensaje = "Ha ocurrido un error inesperado por favor intentelo mas tarde."
                };

            }
            #endregion

        }
        catch (Exception ex)
        {
            AppLog.Write(" Error obteniendo la informacion Inicial. ", AppLog.LogMessageType.Error, ex, "OperadorCarpeta");
            return new
            {
                OK = "error",
                mensaje = "Ha ocurrido un error inesperado por favor intentelo mas tarde."
            };

        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object TraerinformacionInicial()
    {

        try
        {



            return new
            {
                Ok = "OK",

                TIPOIDENTIFICACION = _GetParametrosIdentificacion(),

                //aniofechaIngresoMaxima = (DateTime.Now.Year), // se le restan los dias del mes para que de el ultimo del mes anterior
                //mesfechaIngresoMaxima = DateTime.Now.Month - 1,  // por que el datepicker de jquery empieza en cero
                //diafechaIngresoMaxima = DateTime.Now.Day,
                //aniofechaIngresoMinima = (DateTime.Now.AddYears(-100)).Year, // se le restan los dias del mes para que de el ultimo del mes anterior
                //mesfechaIngresoMinima = (DateTime.Now.AddYears(-100)).Month - 1,  // por que el datepicker de jquery empieza en cero
                //diafechaIngresoMinima = (DateTime.Now.AddDays(-1)).Day,

                //yearRange = (DateTime.Now.AddYears(-150).Year).ToString() + ":" + (DateTime.Now.Year).ToString(),

            };



        }

        catch (Exception ex)
        {
            AppLog.Write(" Error obteniendo la informacion Inicial. ", AppLog.LogMessageType.Error, ex, "OperadorCarpeta");

            return new
            {
                OK = "Error Consultando información inicial.",
                mensaje = ex.Message + ex.StackTrace
            };
        }
    }


    public static object _GetParametrosIdentificacion()
    {


        var resultado = new TipoidentificacionDao().obtenerTipos();
        var retorno = resultado.Select(x => new
        {
            Id = x.id_tipoId,
            Nombre = x.nombre_tipoId.ToUpper()
        });

        return retorno;
    }



}

[Serializable]
public class usuariosMensajes
{

    public string ID { get; set; }
    public int idTIpoIdentificacion { get; set; }
    public string tipoIdentificacion { get; set; }
    public string numeroIdentificacion { get; set; }
    public bool mismo { get; set; }



}
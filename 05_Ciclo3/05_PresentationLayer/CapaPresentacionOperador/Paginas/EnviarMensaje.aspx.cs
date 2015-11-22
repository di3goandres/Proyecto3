﻿using System;
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

public partial class Paginas_EnviarMensaje : System.Web.UI.Page
{
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
                        mensaje = "OK"
                    };
                }
                else
                {

                    SessionHelper.SetSessionData("OPERADOR_ENVIO_LOCAL_U_OTRO", false);

                    return new
                    {
                        OK = "OK",
                        mensaje = "El usuario se encuentra registrado actualmente en otro operador,"
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
                    TIPO_IDENTIFICACION = TIPO_IDENTIFICACION

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
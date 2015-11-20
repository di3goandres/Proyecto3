using AccesControl.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Uniandes.Centralizador.AccesoDatos.Menu;
using Uniandes.Controlador;

public partial class Registro_DatosPersonales : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }





    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object TraerinformacionInicial()
    {

        string UID = string.Empty;
        string IDENTIFICADOR_OPERADOR = string.Empty;
        try
        {

            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {

                Centralizador.Service1Client serviciocentralizador = new Centralizador.Service1Client();

                if (SessionHelper.GetSessionData("ID_USUARIO_CENTRALIZADOR") == null)
                {
                    AppLog.Write("La session ha terminado ", AppLog.LogMessageType.Info, null, "OperadorCarpeta");


                    return new
                    {
                        OK = "SESSIONEND",
                        mensaje = "Su sesión ha finalizado"
                    };

                }
                UID = (string)SessionHelper.GetSessionData("ID_USUARIO_CENTRALIZADOR");
                IDENTIFICADOR_OPERADOR = (string)SessionHelper.GetSessionData("IDENTIFICADOR_OPERADOR");

                var USUARIO = serviciocentralizador.ConsultarUsuario(UID, IDENTIFICADOR_OPERADOR);
                var fechaExpedicion = USUARIO.fechaExpedicion.ToShortDateString();
                var fechaNacimiento = USUARIO.fechaNacimiento.ToShortDateString();

                return new
                {
                    Ok = "OK",

                    TIPOIDENTIFICACION = _GetParametrosIdentificacion(),
                    DEPARTAMENTOS = _GetDepartamentos(),
                    MUNICIPIOSDEPARTAMENTO = _GetMunicipiuosDepartamentos(USUARIO.idDepartamentoResidencia),
                    Paises = _getPaises(),
                    aniofechaIngresoMaxima = (DateTime.Now.Year), // se le restan los dias del mes para que de el ultimo del mes anterior
                    mesfechaIngresoMaxima = DateTime.Now.Month - 1,  // por que el datepicker de jquery empieza en cero
                    diafechaIngresoMaxima = DateTime.Now.Day,
                    aniofechaIngresoMinima = (DateTime.Now.AddYears(-100)).Year, // se le restan los dias del mes para que de el ultimo del mes anterior
                    mesfechaIngresoMinima = (DateTime.Now.AddYears(-100)).Month - 1,  // por que el datepicker de jquery empieza en cero
                    diafechaIngresoMinima = (DateTime.Now.AddDays(-1)).Day,

                    yearRange = (DateTime.Now.AddYears(-150).Year).ToString() + ":" + (DateTime.Now.Year).ToString(),
                    USUARIO = USUARIO,
                    fechaNacimiento,
                    fechaExpedicion
                };



            }
            else
            {
                AppLog.Write("La session ha terminado ", AppLog.LogMessageType.Info, null, "OperadorCarpeta");


                return new
                {
                    OK = "SESSIONEND",
                    mensaje = "Su sesión ha finalizado"

                };
            }






        }
        catch (EndSessionException end)
        {
            AppLog.Write("Su session ha finalizado", AppLog.LogMessageType.Info, end, "OperadorCarpeta");
            return new { OK = "Su session ha finalizado" };
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
    public static object __getPreguntas()
    {


        var resultado = new DaoPreguntas().ObtenerPreguntas();
        var retorno = resultado.Select(x => new
        {
            Id = x.id,
            Nombre = x.pregunta
        });

        return retorno;
    }



    public static object _GetDepartamentos()
    {


        var resultado = new DepartamentosDao().ObtenerDepartamentos();
        var retorno = resultado.Select(x => new
        {
            Id = x.IdDepartamento,
            Nombre = x.NombreDepartamento.ToUpper()
        });

        return retorno;
    }


    public static object _GetMunicipiuosDepartamentos(int idDepartametnto)
    {


        var resultado = new DepartamentosDao().ObtenerMunicipiosDepartamentos(idDepartametnto);
        var retorno = resultado.Select(x => new
        {
            Id = x.IdMunicipio,
            Nombre = x.NombreMunicipio.ToUpper()
        });

        return retorno;
    }

    public static object _getPaises()
    {


        var resultado = new DepartamentosDao().obtenerPaises();
        var retorno = resultado.Select(x => new
        {
            Id = x.IdPais,
            Nombre = x.NombrePais.ToUpper()
        });

        return retorno;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object CargarDatosDropDownMunicipio(int identificador, string select)
    {
        return new
        {
            status = "ok",
            items = _GetMunicipiuosDepartamentos(identificador),
            select = select


        };
    }



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object ActualizarUsuario(int munResidencia, string DireccionResidencia,

        string telefono
      )
    {
        string UID = string.Empty;
        string IDENTIFICADOR_OPERADOR = string.Empty;
        try
        {

            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {

                Centralizador.Service1Client serviciocentralizador = new Centralizador.Service1Client();
                Centralizador.Usuario actualizarusuario = new Centralizador.Usuario();
                if (SessionHelper.GetSessionData("ID_USUARIO_CENTRALIZADOR") == null)
                {
                    AppLog.Write("La session ha terminado ", AppLog.LogMessageType.Info, null, "OperadorCarpeta");


                    return new
                    {
                        OK = "SESSIONEND",
                        mensaje = "Su sesión ha finalizado"
                    };

                }
                UID = (string)SessionHelper.GetSessionData("ID_USUARIO_CENTRALIZADOR");
                IDENTIFICADOR_OPERADOR = (string)SessionHelper.GetSessionData("IDENTIFICADOR_OPERADOR");
                actualizarusuario.UUID = UID;
                actualizarusuario.idMunicipioResidencia = munResidencia;
                actualizarusuario.direccionResidencia = DireccionResidencia;
                actualizarusuario.telefono = telefono;

                var resultado = serviciocentralizador.ActualizarDatosUsuario(actualizarusuario, IDENTIFICADOR_OPERADOR);

                if (resultado)
                {
                    return new
                    {
                        Ok = "OK",
                        mensaje ="Se han actualizado los datos correctamente"

                    };
                }
                else {
                    return new
                    {
                        Ok = "error",
                        mensaje ="Ha ocurrido un error inesperado por favor intentelo nuevamente."

                    };
                
                }



            }
            else
            {
                AppLog.Write("La session ha terminado ", AppLog.LogMessageType.Info, null, "OperadorCarpeta");


                return new
                {
                    OK = "SESSIONEND",
                    mensaje = "Su sesión ha finalizado"

                };
            }






        }
        catch (EndSessionException end)
        {
            AppLog.Write("Su session ha finalizado", AppLog.LogMessageType.Info, end, "OperadorCarpeta");
            return new { OK = "Su session ha finalizado" };
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


}
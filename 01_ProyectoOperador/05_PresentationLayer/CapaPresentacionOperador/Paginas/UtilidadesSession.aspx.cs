using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Script.Services;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Uniandes.Utilidades;
using Uniandes.Centralizador.AccesoDatos.Menu;
using Uniandes.Entity;


public partial class shared_UtilidadesSession : System.Web.UI.Page
{

    public static string PERFIL_ACTUAL = "";
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// metodo que consulta las operaciones asignadas al usuario actual en session
    /// </summary>
    /// <returns>objeto con el listado en arbol de las operacines asignadas</returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object ConsultarMenuUsuarioArbol()
    {
        try
        {
            List<Operacion> operacionesMenuUsua = new List<Operacion>();
            List<Operacion> operacionesMenuUsuaPerfiles = new List<Operacion>();
            List<Operacion> operacionesMenu = new List<Operacion>();

            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                string perfil_actual = "";
                List<string> listaPerfilesDelUsuario = new List<string>();

                bool IsDevelopment = false;

                if (SessionHelper.GetSessionData("MenuUsuario") == null)
                {
                    string usuarioActual = Thread.CurrentPrincipal.Identity.Name;
                    //  string nombreUsuario = SessionHelper.GetSessionData("NombreUsuario").ToString();
                    
                   
                    string[] rolUsuarioPropietario = Roles.GetRolesForUser(usuarioActual);
                   
                    perfil_actual = PERFIL_ACTUAL.ToString();
                    operacionesMenuUsuaPerfiles = new GestorOperaciones().ConsultarOperacionesMenuPorPrefijoPerfil(rolUsuarioPropietario.ToList()).ToList();
                    operacionesMenu.AddRange(operacionesMenuUsuaPerfiles);
                    List<Operacion> listaMenu = new List<Operacion>();

                    listaMenu.Add(new Operacion()
                    {
                        ID_OPERACION = 94,
                        Hijos = null,
                        ID_OPERACION_PADRE = 92,
                        NOMBRE = "Ir al Inicio",
                        URL = "../paginas/Default.aspx",
                        AYUDA = ""
                    }); listaMenu.Add(new Operacion()
                    {
                        ID_OPERACION = 94,
                        Hijos = null,
                        ID_OPERACION_PADRE = 92,
                        NOMBRE = "Cambiar Contraseña",
                        URL = "../RestablecerContrasena/CambiarContrasenia.aspx",
                        AYUDA = ""
                    });
                    listaMenu.Add(new Operacion()
                    {
                        ID_OPERACION = 93,
                        Hijos = null,
                        ID_OPERACION_PADRE = 92,
                        NOMBRE = "Cerrar",
                        URL = "../Logoff.aspx",
                        AYUDA = ""
                    });

                    operacionesMenu.Add(new Operacion
                    {
                        ID_OPERACION = 92,
                        Hijos = listaMenu,
                        ID_OPERACION_PADRE = 1,
                        NOMBRE = "Sesión",
                        URL = "",
                        AYUDA = ""
                    });
                    SessionHelper.SetSessionData("MenuUsuario", operacionesMenu);
                }
                else
                {
                    operacionesMenu = (List<Operacion>)SessionHelper.GetSessionData("MenuUsuario");
                }
                SessionHelper.SetSessionData("MenuUsuario", operacionesMenu);
                return new
                {
                    OK = "OK",
                    Perfil = PERFIL_ACTUAL,
                    EsDesarrollo = IsDevelopment,
                    Menu = operacionesMenu.ToArray(),
                    idusu = ""
                };
            }
            else
            {
                AppLog.Write("La session ha terminado ", AppLog.LogMessageType.Info, null, "HotelLog");

                return new
                {
                    OK = "SESSIONEND"
                };
            }
        }
        catch (EndSessionException end)
        {
            AppLog.Write("Su session ha finalizado", AppLog.LogMessageType.Info, end, "HotelLog");
            return new { OK = "Su session ha finalizado" };
        }
        catch (Exception ex)
        {
            AppLog.Write(" Error obteniendo el menu del usuario. ", AppLog.LogMessageType.Error, ex, "HotelLog");
            return new { OK = "se presento un error consultando el menu de usuario." };
        }
    }
}
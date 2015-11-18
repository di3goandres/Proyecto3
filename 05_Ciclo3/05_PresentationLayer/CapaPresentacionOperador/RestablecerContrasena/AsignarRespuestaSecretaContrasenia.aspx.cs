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
using Uniandes.Centralizador.AccesoDatos.Menu;

public partial class RestablecerContrasena_AsignarRespuestaSecretaContrasenia : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
                {
                    string usuarioActual = Thread.CurrentPrincipal.Identity.Name;

                    MembershipUser u = Membership.GetUser(usuarioActual);
                    if (!u.LastPasswordChangedDate.Equals(u.CreationDate))
                    {
                        Response.Redirect("../Paginas/Default.aspx", true);

                    }
                }
                else
                {
                    Response.Redirect("../Logoff.aspx");
                }
            }
        }
        catch (Exception ex)
        {

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
                PREGUNTAS = _GetPreguntas(),


            };



        }

        catch (Exception ex)
        {

            return new
            {
                OK = "Error Consultando información inicial.",
                mensaje = ex.Message + ex.StackTrace
            };
        }
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object CambioContrasenaRespuestaSecreta(string PREGUNTA, string SecurityAnswer, string Password, string contraseñaAnterior)
    {

        try
        {
            string usuarioActual = Thread.CurrentPrincipal.Identity.Name;
            MembershipUser u = Membership.GetUser(usuarioActual);

            bool CambioContrasena = false;
            bool CambioRespuestaSecreta = false;

            var validadoContrasena = Membership.ValidateUser(usuarioActual, contraseñaAnterior);
            if (validadoContrasena)
            {
                CambioRespuestaSecreta = Membership.Provider.ChangePasswordQuestionAndAnswer(usuarioActual, contraseñaAnterior, PREGUNTA, SecurityAnswer);
                CambioContrasena = u.ChangePassword(contraseñaAnterior, Password);

            }
            else
            {
                return new
                {
                    Ok = "Contra",
                    mensaje = "La contraseña ingresada es incorrecta."
                };

            }
            if (CambioRespuestaSecreta && CambioContrasena)
            {
                return new
                {
                    Ok = "OK",
                    mensaje = "Se ha actualizado correctemente"
                };
            }
            else
            {
                return new
                {
                    Ok = "NO",
                    mensaje = "No se ha actualizado correctamente"
                };
            }


        }

        catch (Exception ex)
        {

            return new
            {
                OK = "Error.",
                mensaje = ex.Message + ex.StackTrace
            };
        }
    }

    public static object _GetPreguntas()
    {


        var resultado = new DaoPreguntas().ObtenerPreguntas();
        var retorno = resultado.Select(x => new
        {
            Id = x.pregunta,
            Nombre = x.pregunta
        });

        return retorno;
    }
}
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
using Uniandes.AccesoDatos.Menu;
using Uniandes.Utilidades;

public partial class RestablecerContrasena_CambiarContrasenia : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                string usuarioActual = Thread.CurrentPrincipal.Identity.Name;

                MembershipUser u = Membership.GetUser(usuarioActual);
                if (u.LastPasswordChangedDate.Equals(u.CreationDate))
                {
                    Response.Redirect("../RestablecerContrasena/AsignarRespuestaSecretaContrasenia.aspx", true);

                }
            }
            else
            {
                Response.Redirect("../Logoff.aspx");
            }
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
            AppLog.Write(" Error obteniendo la informacion Inicial. ", AppLog.LogMessageType.Error, ex, "BansatLog");

            return new
            {
                OK = "Error Consultando información inicial.",
                mensaje = ex.Message + ex.StackTrace
            };
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object CambiarContrasena(string PasswordOld, string PasswordNew, string PreguntaSelect, string respuestaSecreta)
    {

        try
        {
            string PreguntaSecreta = "";

            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                string usuarioActual = Thread.CurrentPrincipal.Identity.Name;

                MembershipUser usuario = Membership.GetUser(usuarioActual);
                if (Membership.ValidateUser(usuarioActual, PasswordOld))
                {

                    PreguntaSecreta = usuario.PasswordQuestion;
                    if (PreguntaSecreta.Equals(PreguntaSelect))
                    {
                        try
                        {

                            usuario.ChangePassword(usuario.ResetPassword(respuestaSecreta), PasswordNew);
                            usuario.UnlockUser();

                            return new
                            {
                                Ok = "OK",
                                mensaje = "Se ha actualizado correctamente la contraseña."
                            };

                        }
                        catch (MembershipPasswordException se)
                        {
                            string ses = se.ToString();
                            return new
                            {
                                Ok = "DATOS",
                                mensaje = "Algunos de los datos ingresados no son correctos."
                            };
                        }
                    }
                    else
                    {
                        return new
                        {
                            Ok = "DATOS",
                            mensaje = "Algunos de los datos ingresados no son correctos."
                        };
                    }




                }
                else
                {
                    return new
                    {
                        Ok = "no",
                        mensaje = "La contraseña ingresada no es la correcta."
                    };


                }




            }
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
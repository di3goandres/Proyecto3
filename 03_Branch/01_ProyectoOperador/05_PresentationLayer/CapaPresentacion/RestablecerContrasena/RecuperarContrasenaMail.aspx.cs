using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mail;
using System.Web.Script.Services;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Uniandes.Utilidades;
using Uniandes.AccesoDatos.Menu;


public partial class RecuperarContrasenaMail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public static void EmailPassword(string email, string password, string usuario)
    {
        try
        {
            var gestor = new Correos().EmailPassword(email, password, usuario);
           
        }
        catch (Exception ex)
        {
            AppLog.Write("Error Enviando Email", AppLog.LogMessageType.Error, ex, "BansatLog");
            
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object TraerInformacionInicial()
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
    public static object RecuperarContrasenaEmail(string userName, string Pregunta, string respuestaSecreta)
    {
        try
        {
            string PreguntaSecreta = string.Empty;
            string Password = string.Empty;
            MembershipUser usuario = Membership.GetUser(userName);
            if (usuario == null)
            {
                return new
                {
                    Ok = "NO_EXISTE",
                    mensaje = "El usuario: " + userName + " , no existe,"
                };
            }
            else
            {
                PreguntaSecreta = usuario.PasswordQuestion;
                if (PreguntaSecreta.Equals(Pregunta))
                {
                    try
                    {
                        Password = usuario.ResetPassword(respuestaSecreta);
                        usuario.UnlockUser();
                        EmailPassword(usuario.Email, Password, usuario.UserName);
                        return new
                        {
                            Ok = "OK",
                            mensaje = "Se ha enviado una nueva contraseña a su correo electronico asociado."
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
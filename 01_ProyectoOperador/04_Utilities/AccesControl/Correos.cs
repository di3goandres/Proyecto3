using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace Uniandes.Utilidades
{
    public class Correos
    {
        public bool EmailPassword(string email, string password, string usuario)
        {
            try
            {
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage("FROMMAIL".GetFromAppCfg(), email);

                mail.Subject = "Diario de una migraña - Nueva Contraseña";

                string body = "<html lang='" + "en'" + "xmlns='" + "http://www.w3.org/1999/xhtml'>" +
                    "<head>" +
                        "<meta charset='" + "utf-8' />" +
                        "<title>DIARIO DE UNA MIGRAÑA</title>" +
                        "<style type='" + "text/css'>" +
                            ".auto-style1 {" +
                                "font-size: small; font-family: Tahoma;" +
                            "}" +
                        "</style></head><body><p>" +

                        "<p>" +
                    /*
                     * aca el mensaje
                     */
                    "<table><tr>"
                    + "<td>  <img src='https://s.yimg.com/wv/images/45113a5e6a4b9c1e03793d36e373a38b_96.jpeg' class='img-responsive' alt='logos'/></td><td></td></tr>"
                      + "</table>" +
                        "<table><tr>"
                    + "<tr><td><td>Su nueva Contraseña es: </td><td>" + password + "</td></tr>"
                    + "<tr><td><td>Su Usuario de ingreso es:  </td><td>" + usuario + "</td></tr>"

                    +"</table>" +
                        //"Su nueva Contraseña es: " + password + "<br />" +
                        //"Su Usuario de ingreso es: " + usuario + "<br />" +

                        "No olvide cambiar la contraseña nuevamente.<br /><br />" +
                        "ADMINISTRADOR - DIARIO DE UNA MIGRAÑA" +
                "&nbsp;</p></body></html>";


                mail.Body = body;//"Your password is: " + Server.HtmlEncode(password);
                mail.IsBodyHtml = true;


                SmtpClient smtpClient = new SmtpClient();
                try
                {
                    smtpClient.Send(mail);
                    return true;

                }
                catch (System.Net.Mail.SmtpException ex)
                {
                    //acciones
                    AppLog.Write(" Error Enviand ocorreo de recuperacion de contraseña.", AppLog.LogMessageType.Error, ex, "HotelLog");

                    return false;


                }

            }
            catch (Exception ex)
            {
                AppLog.Write("Error Enviando Email", AppLog.LogMessageType.Error, ex, "UniandesLog");
                throw;
              
            }
        }

        public bool EnviarEmailCreacionDeUsuario(string email)
        {

            try
            {
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage("FROMMAIL".GetFromAppCfg(), email);

                mail.Subject = "Diario de una Migraña - Nueva Usuario";

                string body = "<html lang='" + "en'" + "xmlns='" + "http://www.w3.org/1999/xhtml'>" +
                    "<head>" +
                        "<meta charset='" + "utf-8' />" +
                        "<title>DIARIO DE UNA MIGRAÑA</title>" +
                        "<style type='" + "text/css'>" +
                            ".auto-style1 {" +
                                "font-size: small; font-family: Tahoma;" +
                            "}" +
                        "</style></head><body><p>" +

                        "<p>" +
                    /*
                     * aca el mensaje
                     */
                      "<table><tr>"
                    + "<td>  <img src='https://s.yimg.com/wv/images/45113a5e6a4b9c1e03793d36e373a38b_96.jpeg' class='img-responsive' alt='logos'/></td><td></td></tr></table>"
                    +
                        "Cordial Saludo<br /> Se ha creado una cuenta de usuario en nuestra plataforma<br /> para ingresar por favor utilizar como contraseña el numero de identificacion ingresado.<br />" +
                        "tanto para usuario y contraseña. <br /><br />" +

                        "No olvide cambiar la contraseña.<br /><br />" +
                        "ADMINISTRADOR - DIARIO DE UNA MIGRAÑA" +
                "&nbsp;</p></body></html>";

                mail.Body = body;
                mail.IsBodyHtml = true;


                SmtpClient smtpClient = new SmtpClient();
                try
                {
                    smtpClient.Send(mail);
                    return true;

                }
                catch (System.Net.Mail.SmtpException ex)
                {
                    //acciones
                    AppLog.Write(" Error Enviandocorreo de creacion de usuarios.", AppLog.LogMessageType.Error, ex, "UniandesLog");
                    return false;


                }

            }
            catch (Exception ex)
            {
                AppLog.Write("Error Enviando Email", AppLog.LogMessageType.Error, ex, "UniandesLog");
                throw;

            }

        }




    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Script.Services;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using AccesControl.Utilidades;
using Uniandes.Centralizador.AccesoDatos.Menu;
public partial class Paginas_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
               // documentos doc = new documentos();

               // Byte[] bytes = File.ReadAllBytes(@"D:\datos.png");
               // String file = Convert.ToBase64String(bytes);

               //// doc.insertar(file, "png", "datos.png");

               // string path = @"D:\100_Cargues\";

               // var documento = doc.consultar("");

               // Byte[] bytess = Convert.FromBase64String(documento.contenido);




               // File.WriteAllBytes(path + documento.nombre, bytes);


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
        catch (Exception ex)
        {

        }
    }



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object TraerinformacionInicial()
    {
        var SIN = SessionHelper.GetSessionData("SINPERMISOS");
        if (SIN == null)
        {
            SessionHelper.SetSessionData("SINPERMISOS", null);
            return new
            {
                OK = false

            };

        }
        else
        {
            SessionHelper.SetSessionData("SINPERMISOS", null);

            return new
            {
                OK = true

            };
        }
        
    }


}
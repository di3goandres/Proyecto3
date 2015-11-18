using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Operador.Entity;
using Uniandes.Controlador;
using System.Threading;
using System.Web.Security;
using Uniandes.Utilidades;
using Uniandes.Encriptador;
using System.Text.RegularExpressions;
using System.Text;

public partial class Paginas_jqueryfiletreedemo_prueba : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        EncriptadorTripleDES des = new EncriptadorTripleDES();

        if (!IsPostBack)
        {
            string uid = string.Empty;
            string NombreFullCarpeta = string.Empty;
            try
            {
                if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
                {

                    string usuarioActual = Thread.CurrentPrincipal.Identity.Name;

                    MembershipUser u = Membership.GetUser(usuarioActual);
                    uid = u.ProviderUserKey.ToString();
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

            CarpetaPersonalDao cPdao = new CarpetaPersonalDao();
            MetadataArchivoDao mDatadao = new MetadataArchivoDao();

            List<string> carpetasTodasUsuario = new List<string>();
            List<CarpetaPersonal> resultadoCarpetas = new List<CarpetaPersonal>();
            List<MetadataArchivos> resultadoMetadata = new List<MetadataArchivos>();

            string dir;
            string returnUrl = Server.UrlEncode(Request.Form["dir"]);
            // returnUrl = Server.UrlEncode(Request.Form["dir"]);


            // returnUrl = System.Web.HttpUtility.UrlEncode(Request.Form["dir"]);
            // returnUrl = System.Web.HttpUtility.HtmlDecode(Request.Form["dir"]);

            //returnUrl = Regex.Replace(Request.Form["dir"].ToString(), @"[^\u0000-\u007F]", string.Empty);
            //returnUrl = Regex.Replace(returnUrl, @"[^\u0000-\u007F]", string.Empty);

            if (Request.Form["dir"] == null || Request.Form["dir"].Length <= 0)
                dir = "/";
            else
            {

                dir = Server.UrlDecode(Request.Form["dir"]);
        

                dir = des.Decrypt(returnUrl, true);

                dir = dir.Replace("-/", "");
            }

            var dato = dir.Split('@');
            if (dato[0] == "/")
            {
                resultadoCarpetas = cPdao.ObtenerCarpetasPorUsuarioCarpeta(uid, null);
                resultadoMetadata = mDatadao.ObtenerArchivosPorCarpetasDeUsuario(null);

                // NombreFullCarpeta = cPdao.fullPathPorCarpeta(null);
            }
            if (dir != "/")
            {
                resultadoCarpetas = cPdao.ObtenerCarpetasPorUsuarioCarpeta(uid, Convert.ToInt64(dato[0]));
                resultadoMetadata = mDatadao.ObtenerArchivosPorCarpetasDeUsuario(Convert.ToInt64(dato[0]));
                //dataNombreFullCarpeta = cPdao.fullPathPorCarpeta(Convert.ToInt64(dir));
            }
            if (NombreFullCarpeta.StartsWith("'\\'"))
            {

                NombreFullCarpeta = NombreFullCarpeta.Remove(0, 2);
            }
            NombreFullCarpeta.Replace("'\\'", @"\");
            if (dato.Count() > 1)
            {
                SessionHelper.SetSessionData("NOMBRE_CARPETA", @dato[1].Replace("%5C", "/").Replace("%20", " "));
                SessionHelper.SetSessionData("ID_CARPETA", Convert.ToInt64(dato[0]));

            }
            else
            {
                SessionHelper.SetSessionData("NOMBRE_CARPETA", "");
                SessionHelper.SetSessionData("ID_CARPETA", 0);
            }
          

            Response.Write("<ul class=\"jqueryFileTree\" style=\"display: none;\">\n");

            string idEncriptado = string.Empty;
            foreach (var data in resultadoCarpetas)
            {
                //idEncriptado = des.Encrypt(data.IdCarpetaPersonal + "@" + @data.PathTotal, true);
                Response.Write("\t<li class=\"directory collapsed\"><a href=\"#\" rel=\"" + data.IdCarpetaPersonal + "@" + @data.PathTotal + "/\">" + data.NombreCarpeta + "</a></li>\n");

            }


            foreach (var data in resultadoMetadata)
            {


                Response.Write("\t<li class=\"file ext_" + data.extension.Replace(".", "") + "\"><a href=\"#\" rel=\"" + data.idDMtadataArchivo + "\">" + data.nombre + "</a></li>\n");
            }

            Response.Write("</ul>");


        }



    }
}
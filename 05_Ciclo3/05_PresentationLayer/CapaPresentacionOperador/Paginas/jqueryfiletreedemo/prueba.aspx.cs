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

public partial class Paginas_prueba : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            //string uid = string.Empty;
            //try
            //{
            //    if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            //    {

            //        string usuarioActual = Thread.CurrentPrincipal.Identity.Name;
                   
            //        MembershipUser u = Membership.GetUser(usuarioActual);
            //        uid = u.ProviderUserKey.ToString();
            //        if (u.LastPasswordChangedDate.Equals(u.CreationDate))
            //        {
            //            Response.Redirect("../RestablecerContrasena/AsignarRespuestaSecretaContrasenia.aspx", true);
            //        }

            //    }
            //    else
            //    {
            //        Response.Redirect("../Logoff.aspx");
            //    }
            //}
            //catch (Exception ex)
            //{

            //}

            //CarpetaPersonalDao cPdao = new CarpetaPersonalDao();
            //MetadataArchivoDao mDatadao = new MetadataArchivoDao();

            //List<string> carpetasTodasUsuario = new List<string>();
            //List<CarpetaPersonal> resultadoCarpetas = new List<CarpetaPersonal>();
            //List<MetadataArchivos> resultadoMetadata = new List<MetadataArchivos>();

            //string dir;
            //if (Request.Form["dir"] == null || Request.Form["dir"].Length <= 0)
            //    dir = "/";
            //else
            //{
            //    dir = Request.Form["dir"];
            //    dir = dir.Replace("/", "");
            //}

            //if (dir == "/")
            //{
            //    resultadoCarpetas = cPdao.ObtenerCarpetasPorUsuarioCarpeta(uid, null);
            //    resultadoMetadata = mDatadao.ObtenerArchivosPorCarpetasDeUsuario(null);

            //}
            //if (dir != "/")
            //{
            //    resultadoCarpetas = cPdao.ObtenerCarpetasPorUsuarioCarpeta(uid, Convert.ToInt64(dir));
            //    resultadoMetadata = mDatadao.ObtenerArchivosPorCarpetasDeUsuario(Convert.ToInt64(dir));
            //}



            //System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(dir);
            //Response.Write("<ul class=\"jqueryFileTree\" style=\"display: none;\">\n");

            //foreach (var data in resultadoCarpetas)
            //{
            //    Response.Write("\t<li class=\"directory collapsed\"><a href=\"#\" rel=\"" + data.IdCarpetaPersonal + "/\">" + data.NombreCarpeta + "</a></li>\n");

            //}


            //foreach (var data in resultadoMetadata)
            //{


            //    Response.Write("\t<li class=\"file ext_" + data.extension.Replace(".", "") + "\"><a href=\"#\" rel=\"" + data.idDMtadataArchivo + "\">" + data.nombre + "</a></li>\n");
            //}

            //Response.Write("</ul>");


        }



    }
}
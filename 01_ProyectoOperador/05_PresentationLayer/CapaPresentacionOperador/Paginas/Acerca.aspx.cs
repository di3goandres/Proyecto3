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
using Uniandes.AccesoDatos;
using Uniandes.AccesoDatos.Menu;

public partial class Paginas_Acerca : System.Web.UI.Page
{
    String PaginaActual = "Acerca.aspx";

    protected void Page_Load(object sender, EventArgs e)
    {
        {
            if (!IsPostBack)
            {
                DaoActivity actividad = new DaoActivity();

                if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
                {
                    string usuarioActual = Thread.CurrentPrincipal.Identity.Name;
                    //  string nombreUsuario = SessionHelper.GetSessionData("NombreUsuario").ToString();
                    MembershipUser u = Membership.GetUser(usuarioActual);
                    Guid a = new Guid(u.ProviderUserKey.ToString());
                    actividad.registrarLog(a, PaginaActual);
                }
            }

        }
    }
}
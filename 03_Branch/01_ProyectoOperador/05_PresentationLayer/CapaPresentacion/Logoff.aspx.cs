using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.ClientServices.Providers;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Uniandes.Utilidades;

public partial class Logoff : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.RemoveAll();
        Session.Abandon();
        SessionHelper.SetSessionData("MenuUsuario", null);
        Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(""), new string[0]);
        ClientFormsAuthenticationMembershipProvider authProvider = new ClientFormsAuthenticationMembershipProvider();// (ClientFormsAuthenticationMembershipProvider)

        FormsAuthentication.SignOut();

        Response.Redirect("Login.aspx");
    }
}
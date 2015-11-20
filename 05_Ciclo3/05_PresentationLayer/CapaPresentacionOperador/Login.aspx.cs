using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Uniandes.Centralizador.AccesoDatos.Menu;
using Uniandes.Controlador;
using Uniandes.GestionUsuarios;




public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        //Centralizador.Service1Client serviciocentralizador = new Centralizador.Service1Client();
        //var re = serviciocentralizador.ConsultarUsuario("bbd1b83f-47eb-4a47-86d7-93e1f23e6eb3", "");
        //if (!IsPostBack)
        //{
            string ola ="";
         
            documentos doc = new documentos();

            //Byte[] bytes = File.ReadAllBytes(@"D:\10_pagos\Respuesta Transferencia.pdf");
            //String file = Convert.ToBase64String(bytes);
            //double len = new FileInfo(@"D:\10_pagos\PSPsm.pdf").Length;

            //doc.insertar(file, "pdf", "Respuesta Transferencia1.pdf");



            //var documento = doc.consultar("4D34CF82-F61A-47A6-B140-61F2F1988FB2");


            //string path = @"D:\100_Cargues\";



            //Byte[] bytess = Convert.FromBase64String(documento.contenido);




            //File.WriteAllBytes(path + documento.nombre, bytess);

            //ServicioGestionUsuarios.GestionUsuarioClient sClient = new ServicioGestionUsuarios.GestionUsuarioClient();
            //sClient.validarUsuarios("diego", "mateo1987.");
           // DaoOperaciones a = new DaoOperaciones();
            //a.DeleteOperacionesPerfil("ADMIN", 4);
            //a.InsertarOperacionesPerfil("ADMIN", 4);

            DateTime result;
            CultureInfo provider = CultureInfo.InvariantCulture;
            string dateString = "Sun 15 Jun 2008 8:30 AM -06:00";
            string format = "ddd dd MMM yyyy h:mm tt zzz";
            try
            {
                result = DateTime.ParseExact(dateString, format, provider);
                Console.WriteLine("{0} converts to {1}.", dateString, result.ToString());
            }
            catch (FormatException)
            {
                Console.WriteLine("{0} is not in the correct format.", dateString);
            }
        //}
    }
    protected void Login1_LoggedIn(object sender, EventArgs e)
    {
        Response.Redirect("Paginas/Default.aspx");
    }

    protected void LoginButton_Click1(object sender, AuthenticateEventArgs e)
    {
        string password = this.Password.Text;
        string usuario = this.UserName.Text;

        GestionUsuario gestor = new GestionUsuario();

        if (gestor.ValidateUser(usuario, password))
        {

            MembershipUser usrInfo = Membership.GetUser(usuario);
            if (usrInfo != null)
            {
                // Email matches, the credentials are valid
                e.Authenticated = true;
            }
            else
            {
                // Email address is invalid...
                e.Authenticated = false;
            }
        }
        else
        {
            // Username/password are not valid...
            e.Authenticated = false;
        }

    }
    protected void LoginButton_Click1(object sender, EventArgs e)
    {
        string password = this.Password.Text;
        string usuario = this.UserName.Text;
        GestionUsuario gestor = new GestionUsuario();

          if (gestor.ValidateUser(usuario, password))
          {
              FormsAuthentication.SetAuthCookie(usuario, true);
              Response.Redirect("Paginas/Default.aspx");

          }
    }
}

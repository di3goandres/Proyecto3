using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Uniandes.Utilidades;


public partial class Registrarse : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object CrearUsuario(string Nombres, string Apellidos, int TIPO_IDENTIFICACION, string NUMERO_IDENTIFICACION,
        int Municipio, string Direccion, string telefono,
       string userName, string Email, string passwordQuestion, string SecurityAnswer)
    {
        string PERFILP = "CLIENTES";
        string Retorno = "";
        string status = "";
        MembershipUser a = Membership.GetUser(userName);

        string porEmail = string.Empty;
        porEmail = Membership.GetUserNameByEmail(Email);
        if (a == null && string.IsNullOrEmpty(porEmail))
        {
            #region ("Creacion")
            MembershipCreateStatus createStatus;
            MembershipUser newUser =
                       Membership.CreateUser(userName, NUMERO_IDENTIFICACION,
                                             Email, passwordQuestion,
                                             SecurityAnswer, true,
                                             out createStatus);

            switch (createStatus)
            {
                case MembershipCreateStatus.Success:
                    Roles.AddUserToRole(userName, PERFILP);

                
                    status = "OK";
                    Retorno = "La cuenta del usuario, ha sido creada con exito";

                    break;

                case MembershipCreateStatus.DuplicateUserName:
                    status = "Existe";
                    Retorno = "Ya existe un usuario con ese nombre de usuario";
                    //CreateAccountResults.Text = "Ya existe un usuario con ese nombre de usuario";//"There already exists a user with this username.";
                    break;

                case MembershipCreateStatus.DuplicateEmail:
                    status = "Duplicado";
                    Retorno = "Ya existe un usuario con este email.";// "There already exists a user with this email address.";
                    break;

                case MembershipCreateStatus.InvalidEmail:
                    status = "email";
                    Retorno = "La dirección de correo electrónico que nos ha facilitado en inválida.";//"There email address you provided in invalid.";
                    break;

                case MembershipCreateStatus.InvalidPassword:
                    status = "password";
                    Retorno = "La contraseña que ha proporcionado no es válido. Debe ser de siete caracteres y tener al menos un carácter no alfanumérico.";//"The password you provided is invalid. It must be seven characters long and have at least one non-alphanumeric character.";
                    break;

                default:
                    status = "Error";
                    Retorno = "Hubo un error desconocido, la cuenta de usuario no fue creado.";//"There was an unknown error; the user account was NOT created.";
                    break;
            }
            #endregion
        }
        else
        {
            if (a != null)
            {
                status = "Existe";
                Retorno = "El nombre de usuario ya existe.";
            }
            //        CreateAccountResults.Text = "El usuario ya existe";

            if (!string.IsNullOrEmpty(porEmail))
            {
                status = "EmailCambiar";
                Retorno = "Ingrese por favor una dirección de correo electrónico diferente.";
            }
        }
        return new
        {
            status = status,
            mensaje = Retorno
        };
    }



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object TraerinformacionInicial()
    {

        try
        {

            // var perfies = (int)ModelLayer.Bansat.Prefijo_TIPO_PARAMETROS.TIPO_PARAMETROS.PERFILES;

            return new
            {
                Ok = "OK",
                //PREGUNTAS = __getPreguntas(),
                //TIPOIDENTIFICACION = _GetParametrosIdentificacion(),
                //DEPARTAMENTOS = _GetDepartamento(),



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

    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public static object CargarDatosDropDownMunicipio(int identificador)
    //{
    //    try
    //    {
    //        return new
    //        {
    //            Ok = "OK",
    //            items = _GetMunicipioDelDepartamanto(identificador),
    //        };
    //    }

    //    catch (Exception ex)
    //    {
    //        AppLog.Write(" Error obteniendo la informacion Inicial. pagina crear usuarios distribuidores ", AppLog.LogMessageType.Error, ex, "BansatLog");
    //        return new
    //        {
    //            OK = "Error Consultando información inicial.",
    //            mensaje = ex.Message + ex.StackTrace
    //        };
    //    }
    //}

    //public static object _GetParametrosIdentificacion()
    //{


    //    var resultado = new GestorTipoIdentificacion().obtenerTipos();
    //    var retorno = resultado.Select(x => new
    //    {
    //        Id = x.ID_TIPO_IDENTIFICACION,
    //        Nombre = x.Descripcion
    //    });

    //    return retorno;
    //}
    //public static object __getPreguntas()
    //{


    //    var resultado = new GestorPreguntas().ObtenerPreguntas();
    //    var retorno = resultado.Select(x => new
    //    {
    //        Id = x.id,
    //        Nombre = x.pregunta
    //    });

    //    return retorno;
    //}

    //public static object _GetDepartamento()
    //{


    //    var resultado = new GestorUbicacion().Obtener().OrderBy(X => X.NombreDepartamento);
    //    var retorno = resultado.Select(x => new
    //    {
    //        Id = x.IdDepartamento,
    //        Nombre = x.NombreDepartamento.ToUpper()
    //    });

    //    return retorno;
    //}

    //public static object _GetMunicipioDelDepartamanto(int IdDepartamento)
    //{


    //    var resultado = new GestorUbicacion().Obtener(IdDepartamento).OrderBy(x => x.NombreMunicipio);
    //    var retorno = resultado.Select(x => new
    //    {
    //        Id = x.IdMunicipio,
    //        Nombre = x.NombreMunicipio.ToUpper()
    //    });

    //    return retorno;
    //}

}
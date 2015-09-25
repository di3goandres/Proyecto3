using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using AccesControl.Utilidades;
using Uniandes.Centralizador.AccesoDatos.Menu;
using Uniandes.GestionUsuarios;

public partial class Administracion_AdministrarUsuarios : System.Web.UI.Page
{
    //INGRESA LOG EN DATBLA DE AUDITORIA 

    String PaginaActual = "AdministrarUsuarios.aspx";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DaoActivity actividad = new DaoActivity();

            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                string usuarioActual = Thread.CurrentPrincipal.Identity.Name;
                //  string nombreUsuario = SessionHelper.GetSessionData("NombreUsuario").ToString();
                var up = new GestionRoles().GetRolesForUser(usuarioActual);
                var tienePermisos = new DaoPerfil().PerfilTieneAcceso(up.First(), PaginaActual);
                if (!tienePermisos)
                {

                    SessionHelper.SetSessionData("SINPERMISOS", "No tiene Permisos para estar en esta pagina");
                    Response.Redirect("../Paginas/Default.aspx");
                }
                MembershipUser u = Membership.GetUser(usuarioActual);
                Guid a = new Guid(u.ProviderUserKey.ToString());
                actividad.registrarLog(a, PaginaActual);
            }
        }
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static GridData GetGridDataWithPaging(
        string colName, string sortOrder, int numPage, int numRows, string searchField, string searchString, string searchOper, bool isSearch)
    {
        GridData gridData = new GridData();
        gridData = _getListListConPaginacion(numPage, numRows, numPage, isSearch, searchField, searchString, searchOper);
        return gridData;
    }



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object EditarAgregar(
       string NombrePerfil, string Descripcion, string Prefijo,
        int ID,
        bool EsEditar)
    {

        try
        {

            DaoPerfil perfilDao = new DaoPerfil();

            GestionRoles gestRoles = new GestionRoles();

            //
            if (!EsEditar)
            {
                #region ("AGREGAR")


                bool resultado = false;
                #region (verificar si existe el rol por el prefijo)
                var existe = gestRoles.RoleExists(Prefijo);
                if (existe)
                {

                    return new
                    {
                        Ok = "Error",
                        mensaje = "El perfil ya existe en nuestra base de datos, se identifica por el PREFIJO"

                    };
                }

                #endregion
                resultado = perfilDao.InsertPerfil(NombrePerfil, Descripcion, Prefijo);
                #region ("Resultado agregar")
                if (resultado)
                {
                    gestRoles.CreateRole(Prefijo);

                    return new
                    {
                        Ok = "OK",
                        mensaje = "Se ha agregado el registro Correctamente"

                    };
                }
                else
                {
                    return new
                    {
                        Ok = "error",
                        mensaje = "No se ha podido registrar el usuario."

                    };
                }
                #endregion
            }
            else
            {

                var resultado = perfilDao.ActualizarPerfil(NombrePerfil, Descripcion, ID);
                if (resultado)
                {

                    return new
                    {
                        Ok = "OK",
                        mensaje = "Se ha Actualizado el registro Correctamente"

                    };
                }
                else
                {
                    return new
                    {
                        Ok = "error",
                        mensaje = "No se ha podido Actualizar el registro ."

                    };
                }
            }


                #endregion




        }
        catch (Exception ex)
        {

            return new
            {
                Ok = "Error",
                mensaje = "ha Ocurrido un error inesperado: " + ex.ToString()

            };

        }
    }



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object Eliminar(
        String ID
        )
    {

        try
        {

            DaoPerfil perfilDao = new DaoPerfil();
            GestionUsuario gestor = new GestionUsuario();


            bool resultado = false;

            resultado = gestor.DeleteUser(ID);
            GestionRoles gestRoles = new GestionRoles();
            #region ("Resultado agregar")
            if (resultado)
            {
                

                return new
                {
                    OK = "OK",
                    mensaje = "Se ha Eliminado el registro Correctamente"

                };
            }
            else
            {
                return new
                {
                    OK = "error",
                    mensaje = "No se ha podido eliminar el usuario."

                };
            }
            #endregion



        }
        catch (Exception ex)
        {

            return new
            {
                OK = "Error",
                mensaje = "ha Ocurrido un error inesperado: " + ex.ToString()

            };

        }
    }

    private static GridData _getListListConPaginacion(int pageIndex, int pageSize, int pageCount, bool isSearch, string searchField, string searchString, string searchOper)
    {
        try
        {
            int totalRecords = 0;

            DaoPerfil perfilDao = new DaoPerfil();
            GestionUsuario gestorusuario = new GestionUsuario();


            var resultado = gestorusuario.GetAllUsers(pageIndex - 1, pageSize, out totalRecords);// perfilDao.GetPerfiles(pageIndex, pageSize, ref totalRecords);
            //  totalRecords = resultado.Count();
            List<GridRow> listProcesos = new List<GridRow>();

            #region ("TOTAL==0")
            if (totalRecords == 0)
            {
                return new GridData
                {
                    page = pageIndex,
                    total = (int)Math.Ceiling((double)totalRecords / (double)pageSize),
                    records = totalRecords,
                    rows = new List<GridRow>(),
                    userMessage = "Se han cargado los datos con éxito.",
                    logMessage = "Carga satisfactoria...",
                    status = Status.OK
                };
            }
            #endregion
            else
            {
                //pageIndex,
                //pageSize, ref totalRecords, estado, banda, UID, plan, cliente);
                int id = 0;
                var userList = new List<MembershipUser>();
                foreach (MembershipUser user in resultado)
                {
                    userList.Add(user);
                }
                foreach (var proceso in userList)
                {
                    id++;

                    var rolesForUser = Roles.GetRolesForUser(proceso.UserName);


                    string rolP = "";
                    foreach (var roles in rolesForUser)
                    {

                        rolP = rolP + roles;
                        rolP = rolP + "; ";

                    }
                    listProcesos.Add(


                    new GridRow()
                    {
                        id = proceso.UserName.ToString(),
                        cell = new List<object>(){
                                proceso.UserName,
                                proceso.IsLockedOut,
                                proceso.LastActivityDate.ToString("yyyy/MM/dd HH:mm"),
                                proceso.Email,
                                rolP
                                
                                
                                
                        }
                    });
                }

            }

            /// Con la información de los procesos y de la consulta se ensambla el objeto GridData de respuesta.
            /// 
            return new GridData
            {
                page = pageIndex,
                total = (int)Math.Ceiling((double)totalRecords / (double)pageSize),
                records = totalRecords,
                rows = listProcesos,
                userMessage = "Se han cargado los datos con éxito.",
                logMessage = "Carga satisfactoria...",
                status = Status.OK
            };

        }
        catch (Exception ex)
        {
            AppLog.Write(" Error consultando la informacion de Cities ", AppLog.LogMessageType.Error, ex, "IbMallsLog");

            return new GridData
            {
                page = pageIndex,
                total = default(int),
                records = default(int),
                rows = new List<GridRow>(),
                userMessage = "Se han cargado los datos con éxito.",
                logMessage = "Carga satisfactoria...",
                status = Status.OK_WITH_MESSAGES
            };
        }
    }


    

    #region (Crear Usuario)
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object CrearUsuario(string userName, string Email, string passwordQuestion, string SecurityAnswer, string Perfil)
    {
        
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
                       Membership.CreateUser(userName, Email,
                                             Email, passwordQuestion,
                                             SecurityAnswer, true,
                                             out createStatus);

            switch (createStatus)
            {
                case MembershipCreateStatus.Success:
                    Roles.AddUserToRole(userName, Perfil);

                   

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
            OK = status,
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
                PREGUNTAS = __getPreguntas(),
                PERFILES = _GetPerfiles(),
    



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


    public static object _GetPerfiles()
    {

        var resultado = new GestionRoles().GetAllRoles();
        var retorno = resultado.Select(x => new
        {
            Id = x,
            Nombre = x.ToUpper()
        });

        return retorno;
    }
    public static object __getPreguntas()
    {


        var resultado = new DaoPreguntas().ObtenerPreguntas();
        var retorno = resultado.Select(x => new
        {
            Id = x.id,
            Nombre = x.pregunta
        });

        return retorno;
    }

    #endregion
}
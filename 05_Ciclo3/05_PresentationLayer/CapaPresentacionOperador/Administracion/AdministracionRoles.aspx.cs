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

public partial class Administracion_AdministracionRoles : System.Web.UI.Page
{
    //INGRESA LOG EN DATBLA DE AUDITORIA 

    String PaginaActual = "AdministracionRoles.aspx";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DaoActivity actividad = new DaoActivity();

            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                string usuarioActual = Thread.CurrentPrincipal.Identity.Name;
                //  string nombreUsuario = SessionHelper.GetSessionData("NombreUsuario").ToString();
                MembershipUser u = Membership.GetUser(usuarioActual);
                var up = new GestionRoles().GetRolesForUser(usuarioActual);
                //validamos que tenga permisos para esta pagina 

                var tienePermisos = new DaoPerfil().PerfilTieneAcceso(up.First(), PaginaActual);
                if (!tienePermisos) {

                    SessionHelper.SetSessionData("SINPERMISOS", "No tiene Permisos para estar en esta pagina");
                    Response.Redirect("../Paginas/Default.aspx");
                }
                Guid a = new Guid(u.ProviderUserKey.ToString());
                actividad.registrarLog(a, PaginaActual);
            }
        }
    }
    //FIN INGRESAR LOG DE AUDITORIA
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
                if (existe) {

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
        int ID, string Prefijo
        )
    {

        try
        {

            DaoPerfil perfilDao = new DaoPerfil();



                bool resultado = false;

                resultado = perfilDao.DeletePerfil(ID);
                GestionRoles gestRoles = new GestionRoles();
                #region ("Resultado agregar")
                if (resultado)
                {
                    gestRoles.DeleteRole(Prefijo);

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
        catch (Exception ex)
        {

            return new
            {
                Ok = "Error",
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

            var resultado = perfilDao.GetPerfiles(pageIndex, pageSize, ref totalRecords);
            totalRecords = resultado.Count();
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
                foreach (var proceso in resultado)
                {
                    id++;
                    listProcesos.Add(

                    new GridRow()
                    {
                        id = proceso.ID_PERFIL.ToString(),
                        cell = new List<object>(){
                                proceso.ID_PERFIL,
                                proceso.NOMBRE_PERFIL,
                                proceso.DESCRIPCION,
                                proceso.PREFIJO
                                
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


}
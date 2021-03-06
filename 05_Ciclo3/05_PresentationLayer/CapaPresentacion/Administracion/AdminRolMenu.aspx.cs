﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Script.Services;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Uniandes.AccesoDatos.Menu;
using Uniandes.GestionUsuarios;
using Uniandes.Utilidades;
using Uniandes.AccesoDatos;

public partial class Administracion_AdminRolMenu : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            String PaginaActual = Request.Url.Segments[Request.Url.Segments.Length - 1];

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
    public static GridData GetGridDataWithPagingHijos(
        string colName, string sortOrder, int numPage, int numRows, string searchField, string searchString, string searchOper, bool isSearch, int IDPADRE)
    {
        GridData gridData = new GridData();
        gridData = _getListListConPaginacionHijos(IDPADRE, numPage, numRows, numPage, isSearch, searchField, searchString, searchOper);
        return gridData;
    }

    private static GridData _getListListConPaginacionHijos(int padre, int pageIndex, int pageSize, int pageCount, bool isSearch, string searchField, string searchString, string searchOper)
    {
        try
        {
            int totalRecords = 0;

            DaoOperaciones operacinoes = new DaoOperaciones();
            DaoPerfil aa = new DaoPerfil();


            var resultado = aa.consultaPerfilesAsociadosAMenu(padre);
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
                int id = 0;
                foreach (var proceso in resultado)
                {
                    id++;
                    listProcesos.Add(

                    new GridRow()
                    {
                        id = proceso.ID_PERFIL.ToString(),
                        cell = new List<object>(){
                                proceso.ID_PERFIL.ToString(),
                                proceso.NOMBRE_PERFIL,
                                
                                
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



    private static GridData _getListListConPaginacion(int pageIndex, int pageSize, int pageCount, bool isSearch, string searchField, string searchString, string searchOper)
    {
        try
        {
            int totalRecords = 0;

            DaoOperaciones operacinoes = new DaoOperaciones();

            var resultado = operacinoes.ConsultarMenuCabecera(pageIndex, pageSize, ref totalRecords);
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
                        id = proceso.ID_OPERACION.ToString(),
                        cell = new List<object>(){
                                proceso.ID_OPERACION,
                                proceso.NOMBRE,
                                proceso.URL,
                                proceso.VISIBLE_MENU =="S" ? true:false
                                
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object DesactivarActivar(

        String ID, bool Activar, int Padre, int Perfil
      )
    {

        try
        {
            DaoOperaciones operacionesDao = new DaoOperaciones();

            if (!Activar)
            {
                #region desactivar perfil de la operacion
                var resultado = operacionesDao.DeleteOperacionesPerfil(ID, Padre);


                if (resultado)
                {

                    return new
                    {
                        Ok = "OK",
                        mensaje = "Se han desasociado los perifiles para este menu, Correctamente"

                    };
                }
                else
                {
                    return new
                    {
                        Ok = "error",
                        mensaje = "No se ha podido actualizar el regitro"

                    };
                }
                #endregion
            }
            else
            {
                DaoPerfil  p = new DaoPerfil() ;
                int i =0;
                var perfil = p.GetPerfiles(1, 999999, ref i);

                var idPerfil = perfil.Where(x => x.PREFIJO == ID).First().ID_PERFIL;

                //validar que el perfil no exista en la base de datos ya
                var existe = operacionesDao.consultaExisteEnbaseDatos(idPerfil, Padre);
                if (existe)
                {
                    return new
                    {
                        Ok = "ER",
                        mensaje = "El perfil ya existe asociado a este menu."

                    };
                }
                else
                {
                    var resultadoinsertar = operacionesDao.InsertarOperacionesPerfil(ID, Padre);
                    if (resultadoinsertar)
                    {

                        return new
                        {
                            Ok = "OK",
                            mensaje = "Se han Asociados los perifiles para este menu, Correctamente"

                        };
                    }
                    else
                    {
                        return new
                        {
                            Ok = "error",
                            mensaje = "No se ha podido actualizar el regitro"

                        };
                    }


                }
            }

        }
        catch (Exception ex)
        {

            return new
            {
                Ok = "Error",
                mensaje = "ha Ocurrido un error inesperado" + ex.ToString()

            };
        }
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object EditarAgregar(
       string MenuName, string URL,
        int ID, bool EsPadre,
        bool EsEditar)
    {

        try
        {

            DaoOperaciones operacionesDao = new DaoOperaciones();


            //
            if (!EsEditar)
            {
                #region ("AGREGAR")


                bool resultado = false;

                if (EsPadre)
                {
                    resultado = operacionesDao.AgregarMenuCabecera(MenuName, URL, true, 0);
                }
                else
                {
                    {
                        resultado = operacionesDao.AgregarMenuCabecera(MenuName, URL, false, ID);
                    }
                }
                #region ("Resultado agregar")
                if (resultado)
                {

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

                var resultado = true;// operacionesDao.DesactivarMenuCabeceraOHijo(Activar, ID);
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
    public static object Actualizar(
       string MenuName, string URL,
        int ID)
    {

        try
        {
            DaoOperaciones operacionesDao = new DaoOperaciones();
            var resultado = operacionesDao.ActualizarDescripcionesMenu(ID, MenuName, URL);
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
        catch (Exception ex)
        {
            return new
            {
                Ok = "Error",
                mensaje = "ha Ocurrido un error inesperado: " + ex.ToString()

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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object TraerinformacionInicial()
    {

        try
        {

           

            return new
            {
                Ok = "OK",

                PERFILES = _GetPerfiles(),




            };



        }

        catch (Exception ex)
        {
         

            return new
            {
                OK = "Error Consultando información inicial.",
                mensaje = ex.Message + ex.StackTrace
            };
        }
    }


}
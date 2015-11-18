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
using Uniandes.AccesoDatos.Menu;
using Uniandes.Controlador;
using Uniandes.Entity;
using Uniandes.Utilidades;

public partial class Pacientes_HistorialEpisodios : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {



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
    public static GridData GetGridDataWithPagingHistorial(
        string colName, string sortOrder, int numPage, int numRows, string searchField, string searchString, string searchOper, bool isSearch, int Paciented)
    {
        GridData gridData = new GridData();
        gridData = _getListListConPaginacionHistorial(numPage, numRows, numPage, isSearch, searchField, searchString, searchOper);
        return gridData;
    }


    private static GridData _getListListConPaginacionHistorial(int pageIndex, int pageSize, int pageCount, bool isSearch, string searchField, string searchString, string searchOper)
    {
        try
        {
            int totalRecords = 0;

            DaoPerfil perfilDao = new DaoPerfil();

            List<Episodios> resultado = new List<Episodios>();
            EpisodiosDao pd = new EpisodiosDao();
            string usuarioActual = "";
            string userid = "";

            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                usuarioActual = Thread.CurrentPrincipal.Identity.Name;
                MembershipUser u = Membership.GetUser(usuarioActual);
                userid = u.ProviderUserKey.ToString();

            }


            resultado = pd.obtenerEpisodiosPacientes(pageIndex - 1, pageSize, ref totalRecords, userid); ;


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

                //foreach (var user in resultado)
                //{
                //    userList.Add(user);
                //}
                foreach (var proceso in resultado)
                {
                    id++;
                    listProcesos.Add(


      new GridRow()
      {
          id = proceso.id_episodio.ToString(),
          cell = new List<object>(){
                        proceso.id_episodio.ToString(),
                        proceso.nombre_intensidad.ToString().ToUpper(),
                        proceso.fecha_episodio.ToString("yyyy/MM/dd HH:mm"),
                        proceso.duracion,
                       
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
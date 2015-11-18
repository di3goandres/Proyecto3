using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Uniandes.AccesoDatos.Menu;
using Uniandes.Controlador;
using Uniandes.Entity;
using Uniandes.GestionUsuarios;
using Uniandes.Utilidades;

public partial class Doctores_ConsultaPacientes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static GridData GetGridDataWithPaging(
        string colName, string sortOrder, int numPage, int numRows, string searchField, string searchString, string searchOper, bool isSearch, string NumeroIdentificacion)
    {
        GridData gridData = new GridData();
        gridData = _getListListConPaginacion(numPage, numRows, numPage, isSearch, searchField, searchString, searchOper, NumeroIdentificacion);
        return gridData;
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static GridData GetGridDataWithPagingHistorial(
        string colName, string sortOrder, int numPage, int numRows, string searchField, string searchString, string searchOper, bool isSearch, int Paciented, string fechaini, string fechafin)
    {
        GridData gridData = new GridData();
        gridData = _getListListConPaginacionHistorial(numPage, numRows, numPage, isSearch, searchField, searchString, searchOper, Paciented, fechaini, fechafin);
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


    private static GridData _getListListConPaginacion(int pageIndex, int pageSize, int pageCount, bool isSearch, string searchField, string searchString, string searchOper, string NumeroIdentificacion)
    {
        try
        {
            int totalRecords = 0;

            DaoPerfil perfilDao = new DaoPerfil();

            List<Paciente> resultado = new List<Paciente>();
            PacienteDao pd = new PacienteDao();


            WebRequest req = WebRequest.Create(@"https://lab-desplieguescc1.herokuapp.com/competitors");

            req.Method = "GET";

            string a = string.Empty;
            string[] arra;
            string[] arradatos;
            List<GridRow> listProcesos = new List<GridRow>();




            try
            {
                HttpWebRequest request = WebRequest.Create(@"https://lab-desplieguescc1.herokuapp.com/competitors") as HttpWebRequest;
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new Exception(String.Format(
                        "Server error (HTTP {0}: {1}).",
                        response.StatusCode,
                        response.StatusDescription));
                    DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(List<Response>));
                    object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
                    List<Response> jsonResponse = objResponse as List<Response>;




                    foreach (var proceso in jsonResponse)
                    {

                        listProcesos.Add(


                        new GridRow()
                        {
                        id = proceso.id.ToString(),
                        cell = new List<object>(){
                        proceso.id.ToString(),
                        proceso.surname.ToString().ToUpper(),
                        proceso.telephone,
                        proceso.winner,
                        proceso.country,
                        }
                        });
                    }

                    totalRecords =jsonResponse.Count();

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
            }

            catch (Exception e)
            {


            }




                //DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Response));
                //var objResponse = jsonSerializer.ReadObject(resp.GetResponseStream());
                //Response jsonResponse                 = objResponse as Response;


                //using (var respStream = resp.GetResponseStream())
                //{
                //    StreamReader reader = new StreamReader(respStream, Encoding.UTF8);
                  

                //    a = reader.ReadToEnd();

                  
                //    arra = a.Split('}');


                //}
                //foreach (var data in arra)
                //{


           


            if (string.IsNullOrWhiteSpace(NumeroIdentificacion))
            {
                resultado = pd.obtenerPacientes(pageIndex - 1, pageSize, ref totalRecords); ;
            }
            else
            {
                resultado = pd.obtenerPacientes(pageIndex - 1, pageSize, ref totalRecords, NumeroIdentificacion);


            }

        

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
          id = proceso.id_paciente.ToString(),
          cell = new List<object>(){
                        proceso.id_paciente.ToString(),
                        proceso.nombres_paciente.ToString().ToUpper()+ " " + proceso.apellidos_paciente.ToString().ToUpper(),
                        proceso.ident_paciente,
                        proceso.mail_paciente,
                        proceso.movil_paciente,
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

    private static GridData _getListListConPaginacionHistorial(int pageIndex, int pageSize,
        int pageCount, bool isSearch, string searchField, string searchString, string searchOper, int Paciented, string fechaini, string fechafin)
    {
        try
        {
            int totalRecords = 0;

            DaoPerfil perfilDao = new DaoPerfil();

            List<Episodios> resultado = new List<Episodios>();
            EpisodiosDao pd = new EpisodiosDao();


            DateTime fechauno = new DateTime();
            DateTime fechados = new DateTime();

            // valido que no venga vacio si viene vacio
            if (!String.IsNullOrEmpty(fechafin))
            {
                if (String.IsNullOrEmpty(fechaini))
                {
                    return new GridData
                    {
                        page = pageIndex,
                        total = (int)Math.Ceiling((double)totalRecords / (double)pageSize),
                        records = totalRecords,
                        rows = new List<GridRow>(),
                        userMessage = "por favor ingrese la fecha inicial",
                        logMessage = "Carga satisfactoria...",
                        status = Status.OK_WITH_MESSAGES
                    };
                }
            }

            if (!String.IsNullOrEmpty(fechaini))
            {
                if (String.IsNullOrEmpty(fechafin))
                {
                    return new GridData
                    {
                        page = pageIndex,
                        total = (int)Math.Ceiling((double)totalRecords / (double)pageSize),
                        records = totalRecords,
                        rows = new List<GridRow>(),
                        userMessage = "por favor ingrese la fecha final",
                        logMessage = "Carga satisfactoria...",
                        status = Status.OK_WITH_MESSAGES
                    };
                }

                try
                {
                    fechauno = Convert.ToDateTime(fechafin);

                }
                catch
                {
                    return new GridData
                    {
                        page = pageIndex,
                        total = (int)Math.Ceiling((double)totalRecords / (double)pageSize),
                        records = totalRecords,
                        rows = new List<GridRow>(),
                        userMessage = "La fecha inicial es incorrecta por favor verificar nuevamente",
                        logMessage = "Carga satisfactoria...",
                        status = Status.OK_WITH_MESSAGES
                    };


                }
                try
                {
                    fechados = Convert.ToDateTime(fechaini);

                }
                catch
                {
                    return new GridData
                    {
                        page = pageIndex,
                        total = (int)Math.Ceiling((double)totalRecords / (double)pageSize),
                        records = totalRecords,
                        rows = new List<GridRow>(),
                        userMessage = "La fecha inicial es incorrecta por favor verificar nuevamente",
                        logMessage = "Carga satisfactoria...",
                        status = Status.OK_WITH_MESSAGES
                    };


                }
            }

            if (String.IsNullOrEmpty(fechaini) && String.IsNullOrEmpty(fechafin))
            {
                fechauno = new DateTime(1900, 01, 01);
                fechados = new DateTime(2400, 01, 01);


            }

            resultado = pd.obtenerEpisodiosPacientes(pageIndex - 1, pageSize, ref totalRecords, Paciented, fechauno, fechados); ;
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

    public static object GetPiechartData(int idPaciente, String fechaini, String fechafin)
    {

        List<ChartDetails> dataList = new List<ChartDetails>();
        List<ReporteSintomas> sintomas = new List<ReporteSintomas>();

        ReportesDao rd = new ReportesDao();


        DateTime fechauno = new DateTime();
        DateTime fechados = new DateTime();

        // valido que no venga vacio si viene vacio
        if (!String.IsNullOrEmpty(fechafin))
        {
            if (String.IsNullOrEmpty(fechaini))
            {
                return new
                {

                    userMessage = "por favor ingrese la fecha inicial",
                    logMessage = "Carga satisfactoria...",
                    status = Status.OK_WITH_MESSAGES
                };
            }
        }

        if (!String.IsNullOrEmpty(fechaini))
        {
            if (String.IsNullOrEmpty(fechafin))
            {
                return new GridData
                {

                    userMessage = "por favor ingrese la fecha final",
                    logMessage = "Carga satisfactoria...",
                    status = Status.OK_WITH_MESSAGES
                };
            }

            try
            {
                fechauno = Convert.ToDateTime(fechafin);

            }
            catch
            {
                return new GridData
                {

                    userMessage = "La fecha inicial es incorrecta por favor verificar nuevamente",
                    logMessage = "Carga satisfactoria...",
                    status = Status.OK_WITH_MESSAGES
                };


            }
            try
            {
                fechados = Convert.ToDateTime(fechaini);

            }
            catch
            {
                return new GridData
                {

                    userMessage = "La fecha inicial es incorrecta por favor verificar nuevamente",
                    logMessage = "Carga satisfactoria...",
                    status = Status.OK_WITH_MESSAGES
                };


            }
        }

        if (String.IsNullOrEmpty(fechaini) && String.IsNullOrEmpty(fechafin))
        {
            fechauno = new DateTime(1900, 01, 01);
            fechados = new DateTime(2400, 01, 01);


        }

        sintomas = rd.sp_reporte_sintamas_paciente(fechauno, fechados, idPaciente);
        dataList = rd.obtenerReporteEpisodios(idPaciente, fechauno, fechados);


        return new
        {
            status = Status.OK,
            pie = dataList,
            lineas = sintomas

        };
    }




}



public class Response
{
  
    public string surname { get; set; }

    public string age { get; set; }
 
    public int telephone { get; set; }
 
    public string cellphone { get; set; }
 
    public string city { get; set; }
  
    public string winner { get; set; }
  
    public string address { get; set; }
 
    public string name { get; set; }
  
    public string id { get; set; }

    public string country { get; set; }

}
//[DataContract]
//public class Response
//{
//    [DataMember(Name = "surname")]
//    public string surname { get; set; }
//    [DataMember(Name = "age")]
//    public string age { get; set; }
//    [DataMember(Name = "telephone")]
//    public int telephone { get; set; }
//    [DataMember(Name = "cellphone")]
//    public string cellphone { get; set; }
//    [DataMember(Name = "city")]
//    public string city { get; set; }
//    [DataMember(Name = "winner")]
//    public string winner { get; set; }
//    [DataMember(Name = "address")]
//    public string address { get; set; }
//    [DataMember(Name = "name")]
//    public string name { get; set; }
//    [DataMember(Name = "id")]
//    public string id { get; set; }
//    [DataMember(Name = "country")]
//    public string country { get; set; }

//}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Uniandes.Utilidades;
using Uniandes.Controlador;
using System.Threading;
using System.Web.Security;

public partial class Pacientes_RegistrarEpisodio : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object TraerInformacionInicial()
    {
        try
        {
            //var datos = new GestorPreguntaEncuesta();
            var resultado = new ArmarRespuestas().RetornaPreguntas();
            //var resultadopregunta_26 = datos.ConsultarPreguntasConrespuestasParaArmar(24);
            return new
            {
                Ok = "OK",
                aniofechaIngresoMaxima = (DateTime.Now.AddYears(15).Year), // se le restan los dias del mes para que de el ultimo del mes anterior
                mesfechaIngresoMaxima = DateTime.Now.AddYears(15).Month - 1,  // por que el datepicker de jquery empieza en cero
                diafechaIngresoMaxima = DateTime.Now.Day,
                aniofechaIngresoMinima = (DateTime.Now.AddYears(-1)).Year, // se le restan los dias del mes para que de el ultimo del mes anterior
                mesfechaIngresoMinima = (DateTime.Now.AddYears(-1)).Month - 1,  // por que el datepicker de jquery empieza en cero
                diafechaIngresoMinima = (DateTime.Now.AddDays(-1)).Day,
                preguntas = resultado,


            };



        }
        catch (EndSessionException end)
        {
            AppLog.Write("Su session ha finalizado", AppLog.LogMessageType.Info, end, "AcdivocaLog");
            return new { OK = "Su session ha finalizado" };
        }
        catch (Exception ex)
        {
            AppLog.Write(" Error obteniendo la informacion Inicial. ", AppLog.LogMessageType.Error, ex, "AcdivocaLog");

            return new
            {
                Ok = "Error Consultando información inicial.",
                MsgError = ex.Message + ex.StackTrace
            };
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object registraSintoma(List<registros> sintomas, List<registros> catalizadore, String fechaRegistro, int minutos, int intensidad)
    {
        PacienteDao pd = new PacienteDao();
        SintomasDao sd = new SintomasDao();
        string usuarioActual = "";
         string userid ="";
        if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
        {
            usuarioActual = Thread.CurrentPrincipal.Identity.Name;
            MembershipUser u = Membership.GetUser(usuarioActual);
            userid = u.ProviderUserKey.ToString();

        }
        //retorna el iddel episodio
        List<int> nuevo = new List<int>();
        foreach(var data in sintomas){
        nuevo.Add(data.ID);
        
        }
        var x = pd.sp_registrar_episodio_paciente(userid, intensidad, minutos);
        sd.registrarSintomasEpisodioPaciente(x, nuevo);
               return new
               {
                   status ="OK",
                 

               };

    }

}

public class registros
{

    public int ID { get; set; }
}




using AccesControl.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Uniandes.GestorLogicaOperador;
using System.IO.Compression;
using System.Threading;

public partial class Paginas_DownloadAdjuntos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (Request.QueryString["ID_ARCHIVO"] == null)
            {
                this.ErrorLabel.Text = "No se encontraron archivos." + Environment.NewLine + "Si el problema persiste contacte el administrador del sistema.";

            }
            else
            {
                string fecha = DateTime.Now.ToString("yyyyMMddHHmmss");
                var uid = Request.QueryString["ID_ARCHIVO"];// (decimal)SessionHelper.GetSessionData("ID_NOTIFICACION");
                GestorDescargaArchivo descargaArchivo = new GestorDescargaArchivo();

                var file = descargaArchivo.ObtenerAdjunto(uid);
                Response.Clear();
                AppLog.Write(" Nombre de archivo", AppLog.LogMessageType.Info, null, "OperadorCarpeta");
                Response.AddHeader("content-disposition", "attachment;filename=" + file.nombre);
                Response.ContentType = System.Net.Mime.MediaTypeNames.Application.Octet;
                Response.BinaryWrite(file.file);
                Response.End();
            }
        }
        catch (ThreadAbortException ab) { 
        
        
        }
        catch (Exception ex)
        {
            AppLog.Write("Ha ocurrido un error inesperado descargando archivos", AppLog.LogMessageType.Error, ex, "OperadorCarpeta");
            this.ErrorLabel.Text = "No se encontraron archivos." + Environment.NewLine + "Si el problema persiste contacte el administrador del sistema.";
        }
    }
}
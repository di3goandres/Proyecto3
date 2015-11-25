using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Uniandes.Encriptador;
using Uniandes.GestorLogicaOperador;
using Uniandes.Utilidades;

public partial class Paginas_Download : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
         EncriptadorTripleDES des = new EncriptadorTripleDES();
        string uid = string.Empty;
        if (Request.QueryString["ID"] == null || Request.QueryString["IDC"]==null)
        {
        
            this.ErrorLabel.Text = "No se encontraron archivos." + Environment.NewLine + "Si el problema persiste contacte el administrador del sistema.";
        }
        else
        {
            var IdCarpeta = Convert.ToInt32( des.Decrypt(Request.QueryString["IDC"], true)); //(int)SessionHelper.GetSessionData("ID_CARPETA");
            var uidArchivo = des.Decrypt(Request.QueryString["ID"], true); 

            try
            {
                uid = (string)SessionHelper.GetSessionData("USUARIO_AUTENTICADO");
                GestorDescargaArchivo descargaArchivo = new GestorDescargaArchivo();

                var file = descargaArchivo.obtenerArchivoUsuario(uid, uidArchivo, IdCarpeta);
                Response.Clear();
                AppLog.Write(" Nombre de archivo", AppLog.LogMessageType.Info, null, "OperadorCarpeta");
                Response.AddHeader("content-disposition", "attachment;filename=" + file.nombre.Replace(" ", "_"));
                Response.ContentType = System.Net.Mime.MediaTypeNames.Application.Octet;
                Response.BinaryWrite(file.file);
                Response.End();
            }
            catch (ThreadAbortException ab)
            {


            }
            catch (Exception ex)
            {
                AppLog.Write("Ha ocurrido un error inesperado", AppLog.LogMessageType.Error, ex, "OperadorCarpeta");
                this.ErrorLabel.Text = "No se encontraron archivos." + Environment.NewLine + "Si el problema persiste contacte el administrador del sistema.";
            }


            
        }
    }



}
using Operador.Entity;
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
using Uniandes.Controlador;
using Uniandes.Encriptador;
using Uniandes.FileControl;
using Uniandes.GestorLogicaOperador;
using Uniandes.Utilidades;

public partial class Paginas_MisDocumentos_Documentos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object TraerDatosNombreCarpeta()
    {

        string nombreCarpeta = SessionHelper.GetSessionData("NOMBRE_CARPETA") == null ? "" : (String)SessionHelper.GetSessionData("NOMBRE_CARPETA");

        return new
        {
            status = "OK",
            NombreCarpeta = nombreCarpeta

        };
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object GuardarCarpetaActual(string id)
    {

        try
        {
            SessionHelper.SetSessionData("ID_CARPETA_ACTUAL", id);

            return new
            {
                status = "OK",
                mensaje = ""

            };
        }
        catch (Exception ex) {
            return new
            {
                status = "a",
                mensaje = "ha ocurrido un error inesperado"

            };
        
        }



    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object TraerInformacionInicial()
    {


        EncriptadorTripleDES des = new EncriptadorTripleDES();
        string uid = string.Empty;
        string NombreFullCarpeta = string.Empty;
        try
        {
            uid = (string)SessionHelper.GetSessionData("USUARIO_AUTENTICADO");
        }
        catch (Exception ex)
        {
            return new
            {
                mensaje = "Session Finalizada",
                status = "error",
            };
        }
        CarpetaPersonalDao cPdao = new CarpetaPersonalDao();
        List<CarpetaPersonal> resultadoCarpetas = new List<CarpetaPersonal>();
        List<TreeField> ListaTodos = new List<TreeField>();
        List<TreeField> Listaretorno = new List<TreeField>();
        GestorArbol gArbolDao = new GestorArbol();
        ListaTodos = gArbolDao.ObtenerArbolPorUsuario(uid, true);
      
        foreach (var datos in ListaTodos)
        {
            datos.id = des.Encrypt(datos.id, true);
            datos.parent = datos.parent == "#" ? "#" : des.Encrypt(datos.parent, true);
            Listaretorno.Add(datos);
        }
        return new
        {
            status = "OK",
            arbol = Listaretorno.ToArray()
        };
    }



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object CrearEditarDocumentos(string anterior, string NuevoNombre, bool Escrear, string identificador)
    {
        string uid = string.Empty;
        string mensaje = string.Empty;
        string OK = "OK";

        EncriptadorTripleDES des = new EncriptadorTripleDES();
        var identificadorArchivo = des.Decrypt(identificador, true);
        var fileControl = new FileControl(Int32.Parse("MaxFileSize".GetFromAppCfg()));
        CarpetaPersonalDao daoCarpeta = new CarpetaPersonalDao();
        DaoUsuario daoUsuaroo = new DaoUsuario();
        try
        {
            uid = (string)SessionHelper.GetSessionData("USUARIO_AUTENTICADO");
        }
        catch (Exception ex)
        {
            return new
            {
                status = "error",
            };
        }
        Decimal? idCarpeta;
        if (identificadorArchivo == "0")
        {
            idCarpeta = null;

        }
        else
        {
            idCarpeta = Convert.ToDecimal(identificadorArchivo);
        }
        var usuario = daoUsuaroo.ObtnerUsuario(uid);
        var full =@""+usuario.CarpetaInicial+ @"\" +@""+daoCarpeta.fullPathPorCarpeta(Convert.ToDecimal(identificadorArchivo));
        if (Escrear)
        {
           
           var existe=  daoCarpeta.validarExisteCarpeta(NuevoNombre, idCarpeta);
           if (!existe)
           {
               var secreo = fileControl._CreateFolderInFTP(full + @"\" + NuevoNombre, usuario.respositorioKey);

               if (secreo)
               {
                   CarpetaPersonal nueva = new CarpetaPersonal();
                   nueva.idCarpetaPadre = Convert.ToDecimal(identificadorArchivo);
                   nueva.NombreCarpeta = NuevoNombre;
                   nueva.userIdAplicacion = uid;
                   daoCarpeta.RegistrarCarpetaPersonal(nueva);
                   mensaje = "Se ha creado satisfactoriamente la carpeta";
               }
               else
               {
                   OK = "iguales";
                   mensaje = "No Se ha creado la carpeta, existe una con el mismo nombre";
               }
           }
           else
           {
               OK = "iguales";
               mensaje = "No Se ha creado la carpeta, existe una con el mismo nombre";
           }
        }
        else {

            var existe = daoCarpeta.validarExisteCarpeta(NuevoNombre, idCarpeta);


            if (!existe)
            {
                fileControl._RenameFolderInFTP(full, NuevoNombre, usuario.respositorioKey);

                CarpetaPersonal actualizar = new CarpetaPersonal();
                actualizar.IdCarpetaPersonal = Convert.ToDecimal(identificadorArchivo);
                actualizar.NombreCarpeta = NuevoNombre;
                actualizar.userIdAplicacion = uid;
                daoCarpeta.ActualizarCarpetaPersonal(actualizar);
                mensaje = "Se ha actualizado satisfactoriamente la carpeta";
            }
            else
            {
                OK = "iguales";
                mensaje = "No Se ha modificado el nombre de la carpeta, existe una con el mismo nombre";
            }
        }
        return new
        {
            status = OK,
            mensaje =mensaje

        };
    }



    public static List<TreeField> armarHijosTreeField(List<CarpetaPersonal> hijos)
    {
        List<TreeField> Listaretorno = new List<TreeField>();

        foreach (var data in hijos)
        {

            TreeField agregar = new TreeField()
            {
                id = data.IdCarpetaPersonal.ToString(),
                //parent = data.idCarpetaPadre == 0 ? "#" : data.idCarpetaPadre.ToString(),
                text = data.NombreCarpeta,
                state = new Estados() { disabled = false, opened = false, selected = false },
                //  children = data.Hijos !=null ? armarHijosTreeField(data.Hijos) : new List<TreeField>()



            };
            Listaretorno.Add(agregar);


        }

        return Listaretorno;

    }





}
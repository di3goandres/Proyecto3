using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Script.Services;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Uniandes.Utilidades;
using Uniandes.Centralizador.AccesoDatos.Menu;
using Uniandes.FileControl;
public partial class Paginas_Default : System.Web.UI.Page
{

    public static string ByteArrayToString(byte[] ba)
    {
        StringBuilder hex = new StringBuilder(ba.Length * 2);
        foreach (byte b in ba)
            hex.AppendFormat("{0:x2}", b);
        return hex.ToString();
    }

    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {

        Response.Write(TreeView1.SelectedNode.Value);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {


                //var fileControl = new FileControl(Int32.Parse("MaxFileSize".GetFromAppCfg()));

                //fileControl._CreateFolderInFTP("CC1077845378/Carpeta1", "OPERADOR_REPOSITORY_USER");
                //fileControl._CreateFolderInFTP("CC1077845378/Carpeta2", "OPERADOR_REPOSITORY_USER");
                //fileControl._CreateFolderInFTP("CC1077845378/Carpeta2/subcarpeta1", "OPERADOR_REPOSITORY_USER");



                //if (!Page.IsPostBack)
                //{
                    TreeView1.Nodes.Add(new TreeNode("Node1","0"));
                    TreeView1.Nodes[0].ChildNodes.Add(new TreeNode("ChildNode","2"));
                    TreeView1.Nodes.Add(new TreeNode("Node2"));
                    TreeView1.Nodes[1].ChildNodes.Add(new TreeNode("ChildNode2"));
                    TreeView1.Nodes.Add(new TreeNode("Node3"));
                    TreeView1.Nodes[2].ChildNodes.Add(new TreeNode("ChildNode2"));

                //}
                
                // documentos doc = new documentos();
                string datosMime = getMimeFromFile(@"D:\vcredist.tmp.jpeg.bmp");

                Byte[] bytes = File.ReadAllBytes(@"D:\datos.png");
                String file = Convert.ToBase64String(bytes);

                // Example #2: Write one string to a text file.
               
                // WriteAllText creates a file, writes the specified string to the file,
                // and then closes the file.    You do NOT need to call Flush() or Close().
                System.IO.File.WriteAllText(@"C:\Users\USUARIO\Desktop\prueba archivos\64.txt", file);
                string fileHexa = ByteArrayToString(bytes);


                
                // WriteAllText creates a file, writes the specified string to the file,
                // and then closes the file.    You do NOT need to call Flush() or Close().
                System.IO.File.WriteAllText(@"C:\Users\USUARIO\Desktop\prueba archivos\hexa.txt", fileHexa);


                string hex = BitConverter.ToString(bytes);
                hex.Replace("-", "");

                System.IO.File.WriteAllText(@"C:\Users\USUARIO\Desktop\prueba archivos\bitConverter.txt", hex);


               //// doc.insertar(file, "png", "datos.png");

               // string path = @"D:\100_Cargues\";

               // var documento = doc.consultar("");

               // Byte[] bytess = Convert.FromBase64String(documento.contenido);




               // File.WriteAllBytes(path + documento.nombre, bytes);


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
    public static object TraerinformacionInicial()
    {
        var SIN = SessionHelper.GetSessionData("SINPERMISOS");
        if (SIN == null)
        {
            SessionHelper.SetSessionData("SINPERMISOS", null);
            return new
            {
                OK = false

            };

        }
        else
        {
            SessionHelper.SetSessionData("SINPERMISOS", null);

            return new
            {
                OK = true

            };
        }
        
    }

    public static string getMimeFromFile(string filename)
    {
        if (!File.Exists(filename))
            throw new FileNotFoundException(filename + " not found");

        byte[] buffer = new byte[256];
        using (FileStream fs = new FileStream(filename, FileMode.Open))
        {
            if (fs.Length >= 256)
                fs.Read(buffer, 0, 256);
            else
                fs.Read(buffer, 0, (int)fs.Length);
        }
        try
        {
            //System.UInt32 mimetype;
            //FindMimeFromData(0, null, buffer, 256, null, 0, out mimetype, 0);
            //System.IntPtr mimeTypePtr = new IntPtr(mimetype);
            //string mime = Marshal.PtrToStringUni(mimeTypePtr);
            //Marshal.FreeCoTaskMem(mimeTypePtr);
            //return mime;

            string mimeType = "application/unknown";
            string ext = System.IO.Path.GetExtension(filename).ToLower();
            string ext2 = System.IO.Path.GetExtension(filename).ToLower();

            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }
        catch (Exception e)
        {
            return "unknown/unknown";
        }
    }


}
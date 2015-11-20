using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;
using Uniandes.FileControl;
using Uniandes.Utilidades;

public partial class eliminaralfinalizar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        var fileControl = new FileControl(Int32.Parse("MaxFileSize".GetFromAppCfg()));
       // fileControl._CreateFolderInFTP(@"CC1077845378\Diego",  "OPERADOR_REPOSITORY_USER");

       
        ISoapMessage a = new ISoapMessage();

        a.Uri = "http://localhost/servicios/servicioweb/Service1.asmx";
        a.ContentXml = @"<?xml version=""1.0"" encoding=""utf-8""?>
             <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns=""http://tempuri.org/"">
               <soapenv:Header/>
               <soapenv:Body>
                  <ValidarExistenciaUsuario>
                     <!--Optional:-->
                     <usuario>
                        <!--Optional:-->
                        <UUID>mmmm</UUID>
                        <idTipoIdentificacion>1</idTipoIdentificacion>
                        <!--Optional:-->
                        <numeroIdentificacion>1077845378</numeroIdentificacion>
                        <idMunicipioExpedicionDocumento>1</idMunicipioExpedicionDocumento>
                        <fechaExpedicion>2015-06-30</fechaExpedicion>
                        <!--Optional:-->
                        <primerApellido>?</primerApellido>
                        <!--Optional:-->
                        <segundoApellido>?</segundoApellido>
                        <!--Optional:-->
                        <primerNombre>?</primerNombre>
                        <!--Optional:-->
                        <segundoNombre>?</segundoNombre>
                        <!--Optional:-->
                        <genero>m</genero>
                        <fechaNacimiento>2015-06-30</fechaNacimiento>
                        <idMunicipioNacimiento>1</idMunicipioNacimiento>
                        <idPaisNacionalidad>233</idPaisNacionalidad>
                        <idMunicipioResidencia>1</idMunicipioResidencia>
                        <!--Optional:-->
                        <direccionResidencia>8</direccionResidencia>
                        <idMunicipioNotificacionCorrespondencia>1</idMunicipioNotificacionCorrespondencia>
                        <!--Optional:-->
                        <direccionNotificacionCorrespondencia>8</direccionNotificacionCorrespondencia>
                        <!--Optional:-->
                        <telefono>8</telefono>
                        <!--Optional:-->
                        <correoElectronico>8</correoElectronico>
                        <idMunicipioLaboral>1</idMunicipioLaboral>
                        <!--Optional:-->
                        <estadoCivil>a</estadoCivil>
                        <idOperador>1</idOperador>
                     </usuario>
                  </ValidarExistenciaUsuario>
               </soapenv:Body>
            </soapenv:Envelope>";
        a.SoapAction = "http://tempuri.org/ValidarExistenciaUsuario";
        Operador.Entity.UsuarioOperador nuevo = new Operador.Entity.UsuarioOperador();
        Centralizador.Usuario nuevos = new Centralizador.Usuario();

     


        var x = Uniandes.Utilidades.ExtensionMethods.ToXML(nuevo);
        var y = Uniandes.Utilidades.ExtensionMethods.ToXML(nuevos);


        var ALGO = CallWebService(a.ContentXml);
       

    }



    public void crearSOAP(Centralizador.Usuario MyObject)
    {


        //XmlSerializer xsSubmit = new XmlSerializer(typeof(MyObject));
        //var subReq = new MyObject();
        //using (StringWriter sww = new StringWriter())
        //using (XmlWriter writer = XmlWriter.Create(sww))
        //{
        //    xsSubmit.Serialize(writer, subReq);
        //    var xml = sww.ToString(); // Your XML
        //}
    }


    
    public WebRequest CreateRequest(ISoapMessage soapMessage)
    {
        var wr = WebRequest.Create(soapMessage.Uri);
        wr.ContentType = "text/xml;charset=utf-8";
        wr.ContentLength = soapMessage.ContentXml.Length;

        wr.Headers.Add("SOAPAction", soapMessage.SoapAction);
        // wr.Credentials = soapMessage.Credentials;
        wr.Method = "POST";
        wr.GetRequestStream().Write(Encoding.UTF8.GetBytes(soapMessage.ContentXml), 0, soapMessage.ContentXml.Length);

        return wr;
    }



    public string CallWebService(string datos)
    {
        var _url = "http://localhost/servicios/servicioweb/Service1.asmx";
        var _action = "http://tempuri.org/ValidarExistenciaUsuario";

        XmlDocument soapEnvelopeXml = CreateSoapEnvelope(datos);
        HttpWebRequest webRequest = CreateWebRequest(_url, _action);
        InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

        // begin async call to web request.
        IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

        // suspend this thread until call is complete. You might want to
        // do something usefull here like update your UI.
        asyncResult.AsyncWaitHandle.WaitOne();

        // get the response from the completed web request.
        string soapResult;
        using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
        {
            using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
            {
                soapResult = rd.ReadToEnd();
            }
            Console.Write(soapResult);
        }
        return soapResult;
    }

    private static HttpWebRequest CreateWebRequest(string url, string action)
    {
        HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
        webRequest.Headers.Add("SOAPAction", action);
        webRequest.ContentType = "text/xml;charset=\"utf-8\"";
        webRequest.Accept = "text/xml";
        webRequest.Method = "POST";
        return webRequest;
    }

    private static XmlDocument CreateSoapEnvelope(string datos)
    {
        XmlDocument soapEnvelop = new XmlDocument();
        soapEnvelop.LoadXml(@datos);
        return soapEnvelop;
    }

    private static void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
    {
        using (Stream stream = webRequest.GetRequestStream())
        {
            soapEnvelopeXml.Save(stream);
        }
    }

    public string Execute()
    {
        HttpWebRequest request = CreateWebRequest();
        XmlDocument soapEnvelopeXml = new XmlDocument();
        soapEnvelopeXml.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>
                <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                  <soap:Body>
                    <HelloWorld xmlns=""http://tempuri.org/"" />
                  </soap:Body>
                </soap:Envelope>");

        using (Stream stream = request.GetRequestStream())
        {
            soapEnvelopeXml.Save(stream);
        }
        string soapResult = string.Empty;
        using (WebResponse response = request.GetResponse())
        {
            using (StreamReader rd = new StreamReader(response.GetResponseStream()))
            {
                soapResult = rd.ReadToEnd();

            }
        }
        return soapResult;
    }
    public static HttpWebRequest CreateWebRequest(string uri)
    {
        //HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(@"http://localhost:30933/Service1.asmx?op=HelloWorld");
        HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(@"" + uri);
        webRequest.Headers.Add(@"SOAP:Action");
        webRequest.ContentType = "text/xml;charset=\"utf-8\"";
        webRequest.Accept = "text/xml";
        webRequest.Method = "POST";
        return webRequest;
    }
    public static HttpWebRequest CreateWebRequest()
    {
        HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(@"http://localhost:30933/Service1.asmx?op=HelloWorld");

        webRequest.Headers.Add(@"SOAP:Action");
        webRequest.ContentType = "text/xml;charset=\"utf-8\"";
        webRequest.Accept = "text/xml";
        webRequest.Method = "POST";
        return webRequest;
    }





    public XmlDocument CallWebService(string method, string operation, string xmlPayload)
{
    string result = "";
    string CREDENTIALS = "PASSword123";
    string URL_ADDRESS = "http://www.client.com/_ws/" + method + "?sso=" + CREDENTIALS + "&o=" + operation ;  //TODO: customize to your needs
    // ===== You shoudn't need to edit the lines below =====

    // Create the web request
    HttpWebRequest request = WebRequest.Create(new Uri(URL_ADDRESS)) as HttpWebRequest;

    // Set type to POST
    request.Method = "POST";
    request.ContentType = "application/xml";

    // Create the data we want to send
    StringBuilder data = new StringBuilder();
    data.Append(xmlPayload);
    byte[] byteData = Encoding.UTF8.GetBytes(data.ToString());      // Create a byte array of the data we want to send
    request.ContentLength = byteData.Length;                        // Set the content length in the request headers

    // Write data to request
    using (Stream postStream = request.GetRequestStream())
    {
        postStream.Write(byteData, 0, byteData.Length);
    }

    // Get response and return it
    XmlDocument xmlResult = new XmlDocument();
    try
    {
        using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
        {
            StreamReader reader = new StreamReader(response.GetResponseStream());
            result = reader.ReadToEnd();
            reader.Close();
        }
        xmlResult.LoadXml(result);
    }
    catch (Exception e)
    {
         //TODO: returns an XML with the error message
    }
    return xmlResult;
}



}
[Serializable]
public class ISoapMessage
{
    public string Uri { get; set; }
    public string ContentXml { get; set; }
    public string SoapAction { get; set; }
    public ICredentials Credentials { get; set; }
}
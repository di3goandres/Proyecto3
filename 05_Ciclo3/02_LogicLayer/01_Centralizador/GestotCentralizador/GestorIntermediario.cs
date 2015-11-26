using Centralizador.DAO;
using Centralizador.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Uniandes.GestotCentralizador;

namespace Uniandes.GestotCentralizador
{
    public class GestorIntermediario
    {

        public string EnviarAOperador(TransferenciaMensajes Soap)
        {
            DaoRUUS daoUsuarios = new DaoRUUS();
            SoapMessageDao dao = new SoapMessageDao();
            List<tb005_RRUS> usuarios = new List<tb005_RRUS>();
            foreach (var data in Soap.destinatarios)
            {

                usuarios.Add(daoUsuarios.ConsultaNumeroYtipoidentificacion(data.NumeroIdentificacion, data.tipoIdentificacion));

            }
            var datosIniciales = @"<?xml version=""1.0"" encoding=""utf-8""?>
             <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/""  {0}>
               <soapenv:Header/>
               <soapenv:Body>
                  <tem:RecibirMensajes><tem:mensajes>
                        <Asunto>{1}</Asunto>
                        <Mensaje>{2}</Mensaje>
                        <NombreEnvia>{3}</NombreEnvia>
                        <Origen>
                           <NumeroIdentificacion>{4}</NumeroIdentificacion>
                           <tipoIdentificacion>{5}</tipoIdentificacion>
                        </Origen>
                        <archivos>
                                {6}
                        </archivos>
                        <destinatarios>{7}
                        </destinatarios>
                </tem:mensajes></tem:RecibirMensajes></soapenv:Body></soapenv:Envelope>";



         
            var archivos = ToXML(Soap.archivos);
            var destinatarios =ToXML(Soap.destinatarios);
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(archivos);

            XmlNodeList archivo = xDoc.GetElementsByTagName("ArrayOfArchivo");

            var resultadoArchivos = archivo[0].InnerXml;


            XmlDocument xDocDestinatarios = new XmlDocument();
            xDocDestinatarios.LoadXml(destinatarios);

            XmlNodeList destinos = xDocDestinatarios.GetElementsByTagName("ArrayOfListaDestinatarios");

            var resultadodestinar = destinos[0].InnerXml;

            foreach (var envio in usuarios)
            {
                var operador = dao.obtenerUrl((int)envio.idOperador);

                string pasar = string.Format(datosIniciales, operador.body, Soap.Asunto, Soap.Mensaje, Soap.NombreEnvia, Soap.Origen.NumeroIdentificacion,Soap.Origen.tipoIdentificacion,
                   resultadoArchivos, resultadodestinar);

                var resultdo = CallWebService(operador.URL, operador.accion, pasar);
            }
            return "";
        }


        private string SerializeMessage(TransferenciaMensajes Soap)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(TransferenciaMensajes));
            var subReq = Soap;
            var st = string.Empty;
            using (StringWriter sww = new StringWriter())
            using (XmlWriter writer = XmlWriter.Create(sww))
            {
                xsSubmit.Serialize(writer, subReq);
                st = sww.ToString(); // Your XML
            }

            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(st);

            XmlNodeList archivo = xDoc.GetElementsByTagName("TransferenciaMensajes");

            var resultado = archivo[0].InnerXml;
            return resultado;
        }


        private string CallWebService(string URL, string action, string datos)
        {
            var _url = URL;
            var _action = action;

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

        private static XmlDocument CreateSoapEnvelope(string datos)
        {
            XmlDocument soapEnvelop = new XmlDocument();
            soapEnvelop.LoadXml(@datos);
            return soapEnvelop;
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

        private static void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }


        private string ArmarSoapordenado(string datos)
        {
            string retorno = string.Empty;


            return retorno;


        }

        private static string ToXML<T>(T obj)
        {
            using (StringWriter stringWriter = new StringWriter(new StringBuilder()))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(stringWriter, obj);
                return stringWriter.ToString();
            }
        }
    }


}

<%@ Page Language="C#" AutoEventWireup="true" %>

<%@ Import Namespace="AccesControl.Utilidades" %>

<%@ Import Namespace="Uniandes.Controlador" %>
<%@ Import Namespace="Operador.Entity" %>




<%
    //
    // jQuery File Tree ASP Connector
    //
    // Version 1.0
    //
    // Copyright (c)2008 Andrew Sweeny
    // asweeny@fit.edu
    // 24 March 2008
    //


    CarpetaPersonalDao cPdao = new CarpetaPersonalDao();

    List<string> carpetasTodasUsuario = new List<string>();
    List<CarpetaPersonal> resultadoCarpetas = new List<CarpetaPersonal>();
    
    string dir;
    if (Request.Form["dir"] == null || Request.Form["dir"].Length <= 0)
        dir = "/";
    else
    {
        dir = Request.Form["dir"];
        dir = dir.Replace("/", "");
    }

    resultadoCarpetas = cPdao.ObtenerCarpetasPorUsuarioCarpeta("c6d7156d-bec0-4bdb-af6b-20802dff6c00", null);



    System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(dir);
    Response.Write("<ul class=\"jqueryFileTree\" style=\"display: none;\">\n");

    foreach (var data in resultadoCarpetas)
    {
        Response.Write("\t<li class=\"directory collapsed\"><a href=\"#\" rel=\"" + dir + data.IdCarpetaPersonal + "/\">" + data.NombreCarpeta + "</a></li>\n");

    }

    foreach (var data in resultadoCarpetas.Where(x => x.NombreCarpeta == dir))
    {
        foreach (var dataHijos in data.Hijos)
            Response.Write("\t<li class=\"directory collapsed\"><a href=\"#\" rel=\"" + dir + dataHijos.IdCarpetaPersonal + "/\">" + dataHijos.NombreCarpeta + "</a></li>\n");

    }
    //foreach (System.IO.DirectoryInfo di_child in di.GetDirectories())
    //    Response.Write("\t<li class=\"directory collapsed\"><a href=\"#\" rel=\"" + dir + di_child.Name + "/\">" + di_child.Name + "</a></li>\n");

    //foreach (System.IO.FileInfo fi in di.GetFiles())
    //{
    //    string ext = "";
    //    if (fi.Extension.Length > 1)
    //        ext = fi.Extension.Substring(1).ToLower();

    //    Response.Write("\t<li class=\"file ext_" + ext + "\"><a href=\"#\" rel=\"" + dir + fi.Name + "\">" + fi.Name + "</a></li>\n");
    //}
    Response.Write("</ul>");


    //foreach (System.IO.FileInfo fi in di.GetFiles())
    //{
    //    string ext = ""; 
    //    if(fi.Extension.Length > 1)
    //        ext = fi.Extension.Substring(1).ToLower();

    //    Response.Write("\t<li class=\"file ext_" + ext + "\"><a href=\"#\" rel=\"" + dir + fi.Name + "\">" + fi.Name + "</a></li>\n");		
    //}
    //Response.Write("</ul>");
%>
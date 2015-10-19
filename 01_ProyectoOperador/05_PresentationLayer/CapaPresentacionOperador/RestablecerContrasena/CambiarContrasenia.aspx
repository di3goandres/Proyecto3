<%@ Page Title="" Language="C#" MasterPageFile="~/shared/Uniandes.master" AutoEventWireup="true" CodeFile="CambiarContrasenia.aspx.cs" Inherits="RestablecerContrasena_CambiarContrasenia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script src="../CustomJS/CambiarContrasena.js"></script>
    <style>
        .btn_cafe {
            background: #603813;
            border: 1px solid #603813;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <center>
     <div id="RegistroUsuarios" class="form">
        <div class="container_login" style="width:75%">
         <div class="row" style="width:75%">
            <h3 style="color:#603813">Cambiar Contraseña</h3>
     
            <table  class="tableregistro" style ="width: 740px; padding-left: 0px; margin-left: 0px; text-align: left;padding:4px" >
                <tr>
                    <td style ="width:49%;padding:5px">
                        <label>Contraseña Anterior</label>
                        <input type="password" id="PasswordOld"  class="TextEntry form-control"/></td>
       
                    <td style ="width:49%;padding:5px">
                        <label>Contraseña Nueva</label>
                        <input type="password" id="PasswordNew"  class="TextEntry form-control" /></td>
                </tr>
                <tr>
                        <td style ="width:49%;padding:5px">
                        <label>Pregunta Secreta</label>
                        <select id="PreguntaSelect"  class="TextEntry form-control"></select></td>
                        <td style ="width:49%;padding:5px">
                        <label>Respuesta Secreta</label>
                        <input id="respuestaSecreta" type="text"  class="TextEntry form-control"/></td>

                </tr>
            </table>    

<table>
<tr>
    <td >
    <input type="button" id="CambiarContrasena" class="center-block btn_cafe btn-success btn" style="background:#603813" value="Cambiar Contraseña" />
    </td>
</tr>
</table>
   
    </div>
           <br />
         <br />
             </div>
         

         </div>

    </center>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contentScripts" runat="Server">
    <script src="../js/bootstrap.min.js"></script>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/shared/Uniandes.master" AutoEventWireup="true" CodeFile="AsignarRespuestaSecretaContrasenia.aspx.cs" Inherits="RestablecerContrasena_AsignarRespuestaSecretaContrasenia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script src="../CustomJS/AsignarRespuestaSecretaContrasena.js"></script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <center>
     <div id="RegistroUsuarios" class="form">
        <div class="container_login" style="width:75%">
         <div class="row" style="width:75%">
              <h3 style="color:#603813">Pregunta de seguiridad  / Cambio de contraseña</h3>
     
   
       <table  class="tableregistro" style ="width: 740px; padding-left: 0px; margin-left: 0px; text-align: left;padding:4px" >
                <tr>
                    <td style ="width:49%;padding:5px">
                    <label>Seleccione una pregunta:</label>
                    <select id="PreguntaSelect" class="TextEntry form-control"></select>
                    </td>
            
                  <td style ="width:49%;padding:5px">
                    <label>Respuesta a la pregunta</label>
                    <input id="respuesta" type="text"   class="TextEntry form-control"/>
                </td>
            </tr>
            <tr>
                 <td style ="width:49%;padding:5px">
                    <label>Contraseña(actual):</label>
                    <input id="PasswordOld" type="password" class="TextEntry form-control"/>
                </td>
                <td style ="width:49%;padding:5px">
                    <label>Nueva Contraseña:</label>
                    <input id="Password" type="password" class="TextEntry form-control"/>
                </td>
            </tr>
              </table>
              <table>
            <tr>
               
                <td style="width: 100%">
                    <input id="validarYcrear" type="button"  class="center-block btn_cafe btn-success btn" style="background:#603813" style="background:#603813" value="Asignar Nueva contraseña" />
                </td>
            </tr>
        </table>
             <br />
             <br />

    </div>
        </center>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contentScripts" runat="Server">
</asp:Content>


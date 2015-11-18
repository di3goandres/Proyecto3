<%@ Page Title="" Language="C#" MasterPageFile="~/shared/Uniandes.master" AutoEventWireup="true" CodeFile="AsignarRespuestaSecretaContrasenia.aspx.cs" Inherits="RestablecerContrasena_AsignarRespuestaSecretaContrasenia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <script src="../CustomJS/AsignarRespuestaSecretaContrasena.js"></script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <h2>Pregunta de seguiridad  / Cambio de contraseña</h2>
    <div class="form"> 
        <table class="table" style="width: 650px; padding-left: 0px; margin-left: 0px; text-align: left">
            
            <tr>
                <td style="width: 20%">
                    <label>Seleccione una pregunta:</label>
                </td>
                <td style="width: 100%">
                    <select id="PreguntaSelect" style="width: 100%"></select>

                </td>
            </tr>

            <tr>
                <td style="width: 20%">
                    <label>Respuesta a la pregunta</label>
                </td>
                <td style="width: 100%">
                    <input id="respuesta" type="text" style="width: 100%" />
                </td>
            </tr>
            <tr>
                <td style="width: 20%">
                    <label>Contraseña:</label>
                </td>
                 <td style="width: 100%">
                    <input id="PasswordOld" type="password" style="width: 100%" />
                </td>
            </tr>
            <tr>
                <td style="width: 20%">
                    <label>Nueva Contraseña:</label>
                </td>
                 <td style="width: 100%">
                    <input id="Password" type="password" style="width: 100%" />
                </td>
            </tr>
            <tr>
                <td style="width: 20%">
                    <label></label>
                </td>
                <td style="width: 100%">
                    <input id="validarYcrear" type="button" style="width: 100%" class="loginButton" value="Aceptar" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contentScripts" Runat="Server">
</asp:Content>


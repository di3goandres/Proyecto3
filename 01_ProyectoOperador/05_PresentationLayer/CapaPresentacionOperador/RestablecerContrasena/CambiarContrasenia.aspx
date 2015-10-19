<%@ Page Title="" Language="C#" MasterPageFile="~/shared/Uniandes.master" AutoEventWireup="true" CodeFile="CambiarContrasenia.aspx.cs" Inherits="RestablecerContrasena_CambiarContrasenia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script src="../CustomJS/CambiarContrasena.js"></script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <center>  
     <h2>Cambiar Contraseña</h2>
    <div class="form">
    <table class="tableinside">
        <tr>
            <td style="width: 20%">
                <label>Contraseña Anterior</label></td>
            <td style="width: 100%">
                <input type="password" id="PasswordOld" /></td>
        </tr>
        <tr>
            <td style="width: 20%">
                <label>Contraseña Nueva</label></td>
            <td style="width: 100%">
                <input type="password" id="PasswordNew" /></td>
        </tr>
        <tr>
            <td style="width: 20%">
                <label>Pregunta Secreta</label>
            </td>
            <td style="width: 100%">
                <select id="PreguntaSelect"></select></td>

        </tr>
        <tr>
            <td style="width: 20%">
                <label>Respuesta Secreta</label>
            </td>
            <td style="width: 100%">
                <input id="respuestaSecreta" type="text" style="width: 90%" /></td>

        </tr>
    </table>
    <table class="tableinside">
        <tr>
            <td>
                <label></label>
            </td>
            <td>
                <input type="button" id="CambiarContrasena"  style="width: 40%" value="Cambiar Contraseña" /></td>

        </tr>
    </table>
    </div>
    </center>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contentScripts" runat="Server">
</asp:Content>


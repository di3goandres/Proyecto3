<%@ Page Title="" Language="C#" MasterPageFile="~/shared/OutsideUniandes.master" AutoEventWireup="true" CodeFile="RecuperarContrasenaMail.aspx.cs" Inherits="RecuperarContrasenaMail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    
    <script src="../CustomJS/RecuperarContrasenaMail.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h4>Recuperar Contraseña via email</h4>
    <div id="Nombreusuario"   style="margin-top:auto" class="form-group">
       <table class="tableIndicadores" style="width: 90%; margin-left: 0px">

            <tr>
                <td style="width: 100%;color:red">
                    <a href="../Login.aspx" target="_self" style="color:#c4741c"><-Regresar</a>
                </td>
            </tr>
        </table>
        <center>
         <table class="tableIndicadores"  border="0" style="width: 90%; margin-left: 0px">
            <tr>
                <td style="width: 20%">
                    <label>Nombre Usuario</label></td>
                <td style="width: 90%">
                    <input type="text" id="userName"  style="width: 40%" /></td>
            </tr>
            <tr>
                <td>
                    <label>Pregunta Secreta</label>
                </td>
                <td>
                    <select id="PreguntaSelect" style="width: 40%"></select></td>

            </tr>
            <tr>
                <td>
                    <label>Respuesta Secreta</label>
                </td>
                <td>
                    <input id="respuestaSecreta" type="text"  style="width: 40%" /></td>

            </tr>
        </table>
            </center>
       <table class="tableIndicadores" style="width: 90%; margin-left: 0px">
            <tr>
                <td>
                    <label></label>
                </td>
                <td>
                    <input type="button" id="EnviarEmail" class="loginButton center-block btn_cafe btn-success btn btn-lg" style="width: 20%" value="Enviar Email" /></td>

            </tr>
        </table>
        
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentScripts" runat="Server">
</asp:Content>


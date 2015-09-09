<%@ Page Title="" Language="C#" MasterPageFile="~/login.master" AutoEventWireup="true" CodeFile="Registrarse.aspx.cs" Inherits="Registrarse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script src="CustomJS/Usuarios/Registrar.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>Registro de Clientes</h2>

    <br />
     <table class="tableinside">

            <tr>
                <td style="width: 100%">
                    <a href="Default.aspx" target="_self"><-Regresar</a>
                </td>
            </tr>
        </table>
    <center>
    <div id="EditarAgregar" class="container_login"">

        <table class="table" style="width: 550px; padding-left: 0px; margin-left: 0px; text-align: left">
            <tr>
                <td style="width: 20%">
                    <label>Username</label></td>
                <td style="width: 100%">
                    <input id="username" type="text" style="width: 100%" /></td>
            </tr>
            <tr>
                <td style="width: 20%">
                    <label>Nombres</label></td>
                <td style="width: 100%">
                    <input id="NombreI" type="text" style="width: 100%" /></td>
            </tr>
              <tr>
                <td style="width: 20%">
                    <label>Apellidos</label></td>
                <td style="width: 100%">
                    <input id="ApellidosI" type="text" style="width: 100%" /></td>
            </tr>
            <tr>
                <td style="width: 20%">
                    <label>Tipo de Identificación</label></td>
                <td style="width: 100%">
                    <select id="TipoIdentificacion" style="width: 100%"></select>
                </td>
            </tr>
            <tr>
                <td style="width: 20%">
                    <label>Número de Identificación </label>
                </td>
                <td style="width: 100%">
                    <input id="NumeroIdentificacion" type="text" style="width: 100%" onkeypress="return EvaluarTexto('Numeros',this,event);" /></td>
            </tr>
            <tr>
                <td style="width: 20%">
                    <label>Departamento</label></td>
                <td style="width: 100%">
                    <select id="Departamento" style="width: 100%"></select></td>
            </tr>
            <tr>
                <td style="width: 20%">
                    <label>Municipio</label></td>
                <td style="width: 100%">
                    <select id="Municipio" style="width: 100%"></select></td>
            </tr>
            <tr>
                <td style="width: 20%">
                    <label>Dirección</label></td>
                <td style="width: 100%">
                    <input id="Direccion" type="text" style="width: 100%" /></td>
            </tr>
         
            <tr>
                <td style="width: 20%">
                    <label>Teléfono</label></td>
                <td style="width: 100%">
                    <input id="telefono" type="text" style="width: 100%" onkeypress="return EvaluarTexto('Numeros',this,event);" maxlength="20" /></td>
            </tr>
            <tr>
                <td style="width: 20%">
                    <label>Email</label></td>
                <td style="width: 100%">
                    <input id="Email" type="text" style="width: 100%" /></td>
            </tr>
             <tr>
                <td style="width: 20%">
                    <label>Pregunta secreta</label></td>
                <td style="width: 100%">
                    <select id="PreguntasSecretas" style="width: 100%"></select></td>
            </tr>
            
              <tr>
                <td style="width: 20%">
                    <label>Respuesta Secreta</label></td>
                <td style="width: 100%">
                    <input id="Respuesta" type="text" style="width: 100%" /></td>
            </tr>
            <tr>
                <td style="width: 20%">
                    <label></label>
                </td>
                <td style="width: 100%">
                    <input id="EditarCrear" type="button" style="width: 30%" value="Registrarse" />
                </td>
            </tr>
        </table>
    </div>

</center>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentScripts" runat="Server">
</asp:Content>



<%@ Page Title="" Language="C#" MasterPageFile="~/shared/MasterPageOutside.master" AutoEventWireup="true" CodeFile="RegistrarPaciente.aspx.cs" Inherits="Registro_RegistrarPaciente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../CustomJS/Usuarios/Registrar.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3>Registro Pacientes</h3>

    <br />
    <table class="tableinside">

        <tr>
            <td>
                <a href="../login.aspx" target="_self"><-Regresar</a>
            </td>
        </tr>
    </table>
    <center>
    <div id="EditarAgregar" class="form">

        <table class="tableinside" border="0" style="width: 840px; padding-left: 0px; margin-left: 0px; text-align: left">
            <tr>
                  <td >
                    <label>Nombres</label></td>
                <td>
                    <input id="NombreI" type="text"/></td>
                 <td  >
                    <label>Apellidos</label></td>
                <td >
                    <input id="ApellidosI" type="text" /></td>
           
              
            </tr>
             <tr>
                <td>
                    <label>Username</label></td>
                <td >
                    <input id="username" type="text" /></td>
                 <td >
                    <label>Dirección</label></td>
                <td >
                    <input id="Direccion" type="text"  /></td>

                   </tr>
             <tr>
                <td >
                    <label>Tipo de Identificación</label></td>
                <td >
                    <select id="TipoIdentificacion" ></select>
                </td>
          
                <td >
                    <label>Número de Identificación </label>
                </td>
                <td >
                    <input id="NumeroIdentificacion" type="text"  onkeypress="return EvaluarTexto('Numeros',this,event);" /></td>
                      
            
               
            </tr>
         
            <tr>
                <td >
                    <label>Teléfono</label></td>
                <td >
                    <input id="telefono" type="text"  onkeypress="return EvaluarTexto('Numeros',this,event);" maxlength="20" /></td>
           
                <td >
                    <label>Email</label></td>
                <td >
                    <input id="Email" type="text"  /></td>
             </tr>
             <tr>
                <td >
                    <label>Pregunta secreta</label></td>
                <td >
                    <select id="PreguntasSecretas" ></select></td>
                 <td >
                    <label>Respuesta Secreta</label></td>
                <td >
                    <input id="Respuesta" type="text"  /></td>
            </tr>
            
<%--              <tr>
                
            </tr>--%>
            <%--<tr>
                <td >
                    <label></label>
                </td>
                <td >
                    <input id="EditarCrear" type="button" style="width: 30%" value="Registrarse" />
                </td>
            </tr>--%>
        </table>
        <br />
        <br />

         <table>
              <tr> <td></td><td></td></tr>
          <tr>
                <td >
                    <label></label>
                </td>
                <td >
                    <input id="EditarCrear" type="button" style="width: 30%" value="Registrarse" />
                </td>
            </tr></table>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contentScripts" runat="Server">
</asp:Content>


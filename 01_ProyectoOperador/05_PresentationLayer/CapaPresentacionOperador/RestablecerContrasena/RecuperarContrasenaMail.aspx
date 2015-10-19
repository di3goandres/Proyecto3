<%@ Page Title="" Language="C#" MasterPageFile="~/shared/OutsideUniandes.master" AutoEventWireup="true" CodeFile="RecuperarContrasenaMail.aspx.cs" Inherits="RecuperarContrasenaMail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <script src="../CustomJS/RecuperarContrasenaMail.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <center>
    <table class="tableIndicadores" style="width: 90%; margin-left: 0px">

                         <tr>
                             <td style="width: 100%; color: red">
                                 <a href="../Login.aspx" target="_self" style="color: #c4741c"><-Regresar</a>
                             </td>
                         </tr>
                     </table>
     </center>
      <center>
     <div id="RegistroUsuarios" class="form">
         <div class="container_login" style="width: 75%">
             <div class="row" style="width: 75%">
                 <h3 style="color: #603813">Recuperar Contraseña via email</h3>

                  
        <table  class="tableregistro" style ="width: 740px; padding-left: 0px; margin-left: 0px; text-align: left;padding:4px" >
                <tr>
                    <td style ="width:49%;padding:5px">
                    <label>Nombre Usuario</label>
                    <input type="text" id="userName"   class="TextEntry form-control"/>

                    </td>
                    <td style ="width:49%;padding:5px"></td>
            </tr>
            <tr>
                <td style ="width:49%;padding:5px">
                    <label>Pregunta Secreta</label>
                    <select id="PreguntaSelect"  class="TextEntry form-control"></select>
                  </td>
                <td style ="width:49%;padding:5px">
                      <label>Respuesta Secreta</label>
                                   <input id="respuestaSecreta" type="text"  class="TextEntry form-control" /></td>

            </tr>
        </table>
           
                     <table >
                         <tr>
                             
                             <td>
                                 <input type="button" id="EnviarEmail" class="loginButton center-block btn_cafe btn-success btn btn-lg"  value="Enviar Email" /></td>

                         </tr>
                     </table>
                 <br />
                 <br />

                 </div>
    </center>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentScripts" runat="Server">
</asp:Content>


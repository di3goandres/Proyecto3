﻿<%@ Page Title="" Language="C#" MasterPageFile="~/shared/Uniandes.master" AutoEventWireup="true" CodeFile="EnviarMensaje.aspx.cs" Inherits="Paginas_EnviarMensaje" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../CustomJS/Mensajeria/Mensajeria.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <center>
   <div id="RegistroUsuarios" class="form">
       <div class="container_login" style="width:75%">
         <div class="row" style="width:75%">
            <h3 style="color:#603813">Enviar Mensajes</h3>
             </div>
           </div>
        
      
       
        <div class="container_login" style="width:75%">
    
             <h3 style="color:#603813">Usuario</h3>
              <table  class="tableregistro" style ="width: 740px; padding-left: 0px; margin-left: 0px; text-align: left;padding:4px" >
                   <tr>
                  <td style ="width:49%;padding:5px"> <label>Tipo de Identificación</label>
                    <select id="ValidarTipoIdentificacion" class="TextEntry form-control" ></select>
                  </td>
                  <td style ="width:49%;padding:5px"><label>Número de Identificación </label>
                    <input class="TextEntry form-control" id="ValidarNumeroIdentificacion" type="text"  onkeypress="return EvaluarTexto('Numeros',this,event);" />
                  </td>
                   </tr>
                   </table>
             <table>
             
          <tr>
               
                <td style ="width:49%;padding:5px">
                    <input id="BotonValidarRegistro" type="button" value="Validar"   class="loginButton center-block btn_cafe btn-success btn"/>
                </td>
            </tr></table>
         </div>
         </div>
        <div id="Mensaje" class="container_login" style="width:75%">
    
             <h3 style="color:#603813">Mensaje</h3>
              <table  class="tableregistro" style ="width: 740px; padding-left: 0px; margin-left: 0px; text-align: left;padding:4px" >
                   <tr>
                
                  <td style ="width:49%;padding:5px"><label>Asunto</label>
                    <input class="TextEntry form-control" id="Asunto" type="text"  onkeypress="return EvaluarTexto('Numeros',this,event);" />
                  </td>
                   </tr>

                  <tr>
                
                  <td style ="width:49%;padding:5px"><label>Mensaje </label>
                    <textarea class="TextEntry form-control" id="Text2"    ></textarea>
                  </td>
                   </tr>
                   </table>
             <table>
             
          <tr>
               
                <td style ="width:49%;padding:5px">
                    <input id="EnviarMensaje" type="button" value="Enviar"   class="loginButton center-block btn_cafe btn-success btn"/>
                </td>
            </tr></table>
         </div>
         </div>
        </center>
    </div>
        

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contentScripts" runat="Server">
</asp:Content>


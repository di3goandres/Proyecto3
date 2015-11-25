<%@ Page Title="" Language="C#" MasterPageFile="~/shared/Uniandes.master" AutoEventWireup="true" CodeFile="EnviarMensaje.aspx.cs" Inherits="Paginas_EnviarMensaje" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../js/jquery.fileupload.js"> </script>
    <script src="../js/jquery.iframe-transport.js"> </script>
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
          
        
      
       
        <div class="container_login" style="width:75%">
    
             <h3 style="color:#603813">Usuario</h3>
               <center>
                <div>
        
                    <div > 
                    <br />
                        <div id="divUsuarios">
                            <table id="DatosUsuarios" style="height: 100%;">
                            </table>
                        <div id="DatosUsuariosPagerL">
                        </div>
                    </div>
                   
                    </div>
                </div>


                </center>
            </div>
            <div id="AgregarUsuario">
                <br />


                  <div class="row"  >
                        <div class="row"  style="width:95%" >
                        <div class="col-md-6 col-sm-12 text-center"> 
                        <label>Tipo de Identificación</label>
                        </div>
                      <div class="col-md-6 col-sm-12 text-center"> 
                        <label>Número de Identificació</label>
                        </div>
                
                       
                      </div>
                       <div class="row" style="width:95%">
                        
                        <div class="col-md-6 col-sm-12 text-left">
                        <select id="ValidarTipoIdentificacion" class="TextEntry form-control" ></select>
                        </div>
                            <div class="col-md-6 col-sm-12 text-left"> 
                            <input class="TextEntry form-control" id="ValidarNumeroIdentificacion" type="text"  onkeypress="return EvaluarTexto('Numeros',this,event);" />

                            </div>
                            </div>


                 </div>

                 <div class="row text-center"  >
                       <input id="CerraValidarAgregar" type="button" value="Cerrar"    class="btn btn_ficha pull-center"/>
                    <input id="BotonValidarRegistro" type="button" value="Validar"    class="btn btn_ficha pull-center"/>
                 </div>




<%--
              <table  class="tableregistro" style ="width: 740px; padding-left: 0px; margin-left: 0px; text-align: left;padding:4px" >
                   <tr>
                  <td style ="width:49%;padding:5px"> <label>Tipo de Identificación</label>
                   
                  </td>
                  <td style ="width:49%;padding:5px"><label>Número de Identificación </label>
                  </td>
                   </tr>
                   </table>--%>
             <table>
             
          <tr>
               
                <td style ="width:49%;padding:5px">
                </td>
            </tr></table>
         </div>
         
        <div id="Mensaje" class="container_login" style="width:75%">
    
             <h3 style="color:#603813">Mensaje</h3>
              <table  class="tableregistro" style ="width: 740px; padding-left: 0px; margin-left: 0px; text-align: left;padding:4px" >
                   <tr>
                
                  <td style ="width:49%;padding:5px"><label>Asunto</label>
                    <input class="TextEntry form-control" id="Asunto" type="text"   />
                  </td>
                   </tr>

                  <tr>
                
                  <td style ="width:49%;padding:5px"><label>Mensaje </label>
                    <textarea class="TextEntry form-control" id="cuerpoMensaje"  cols="10"   rows="10" ></textarea>
                  </td>
                   </tr>
                   </table>
            <br />
                
             <div id="cargarArchivos" class="container_login" style="width:87%">
   
                    <br />
                       <div class="row"  >
                        <div class="col-md-2.5 col-sm-12 text-center"> 
                        <label>Adjuntar Archivos</label>
                        </div>

                        <div class="col-md-9 col-sm-12 text-left">
                        <input name="name" value="" id="fileUploadSolicitud" type="file" name="files[]" multiple="" />
                        <div id="progress">
                        <div class="load-file-bar" style="width: 0%;">
                        </div>
                        </div>
                    <div id="ArchivosAdjuntosDiv" class="text-left">
                    </div>
                                  </div>
                           </div>

        </div>
       

             <table>
             
          <tr>
               
                <td style ="width:49%;padding:5px">
                    <input id="EnviarMensaje" type="button" value="Enviar"   class="loginButton center-block btn_cafe btn-success btn"/>
                </td>
            </tr>

             </table>
         </div>

         </div>
        </center>


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contentScripts" runat="Server">
</asp:Content>


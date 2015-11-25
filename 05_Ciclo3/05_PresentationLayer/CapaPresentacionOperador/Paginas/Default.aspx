<%@ Page Title="" Language="C#" MasterPageFile="~/shared/Uniandes.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Paginas_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script src="../js/jquery.fileupload.js"> </script>
    <script src="../js/jquery.iframe-transport.js"> </script>
    <script src="../CustomJS/Default.js"></script>




</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <center>
   <div id="RegistroUsuarios" class="form">
       <div class="container_login" style="width:75%">
         <div class="row" style="width:75%">
              <h3 style="color:#603813">Bandeja de Entrada</h3>
       <center>
                    <div id="">
                        <table id="Datos" style="height: 100%;">
                        </table>
                        <div id="pagerL">
                        </div>
                    </div>
                  </center>

            </div>
                </div>
            </div>

       

    <%--<div id="cargarArchivos">
       <%-- <div>
            <table style="width: 100%;">
                <tr>
                    <td>
                        <h3>Seleccione el tipo de archivo que esta subiendo</h3>
                    </td>
                    <td>
                        <br />
                    </td>
                </tr>
            </table>
        </div>--%>
       <%-- <div class="Acdivocabutton-bar">
            <table style="width: 100%;">
                <tr>
                    <td>
                        <input id="AceptarCargueArchivos" type="button" value="Enviar Adjuntos" class="ButtonAc" />
                        <input id="cancelarCargueArchivos" type="button" value="Cancelar" class="ButtonAc" />


                    </td>
                </tr>
            </table>
        </div>
        <table>
            <tr>
                <th>Adjuntar archivo
                </th>
                <td>
                    <input name="name" value="" id="fileUploadSolicitud" type="file" name="files[]" multiple="" />
                    <div id="progress">
                        <div class="load-file-bar" style="width: 0%;">
                        </div>
                    </div>
                    <div id="ArchivosAdjuntosDiv">
                    </div>
                </td>
            </tr>
        </table>

    </div>--%>

   <%-- <asp:ListBox ID="lstFruits" runat="server" SelectionMode="Multiple">
        <asp:ListItem Text="Mango" Value="1" />
        <asp:ListItem Text="Apple" Value="2" />
        <asp:ListItem Text="Banana" Value="3" />
        <asp:ListItem Text="Guava" Value="4" />
        <asp:ListItem Text="Orange" Value="5" />
    </asp:ListBox>
    <asp:Button ID="Button1" Text="Submit" runat="server" />


     <asp:TreeView ID="TreeView1" runat="server" 
        onselectednodechanged="TreeView1_SelectedNodeChanged">

    </asp:TreeView>--%>



        <div id="VerDetalle">
            <br />
       

               <div class="row" width:100%">

                         <div class="col-md-5 col-sm-12"> 
                                   <label >De:</label>
                    <span   id="EnviadoDesde" class="TextEntry form-control"/>
                                </div>

                   
                         <div class="col-md-5 col-sm-12"> 
                                <label>Fecha de Envio</label>
                             <span   id="fechaEnvio" class="TextEntry form-control"/>
                                </div>
                        
                          <%--  <div class="col-md-6 col-sm-12">
                                <div class="form-group has-feedback ">
                                    <label for="exampleInputEmail1" class="labelhalf">Documento</label>
                                    <div class="ui-widget arrow-caret half25">
                                        <select class="combobox form-control">
                                            <option value="1">C.C</option>
                                            <option value="2">C.E.</option>
                                            <option value="3">NIT</option>
                                        </select>
                                    </div>
                                    <span class="half75">
                                        <input pattern="^[_A-z0-9]{1,}$" type="text" class="form-control animated" id="exampleInputEmail1" placeholder="Número de identificación" required="">
                                        <span class="glyphicon form-control-feedback" aria-hidden="true"></span>
                                        <span class="help-block with-errors"></span>
                                    </span>
                                </div>
                                
                            </div>--%>
                   </div>

              <div class="row">

                     <div class="col-md-5 col-sm-12"> 
                          <label >Para:</label>
                    <span   id="Destinatarios" class="TextEntry form-control"/>
                         </div>

                  <div class="col-md-5 col-sm-12"> 
                         <label>Asunto</label>
                       <span   id="Asunto" class="TextEntry form-control"/>
                         </div>

                  </div>


           <div class="row">
            <div class="col-md-5 col-sm-12"> 
                 <label>Mensaje</label>
                   <textarea style="resize:none" class="TextEntry form-control" id="cuerpoMensaje"  cols="100"   rows="14" disabled="disabled"></textarea>
                 
                </div>

                <div class="col-md-5 col-sm-12"> 
                    <br />
                     <div id="divAdjuntoss">
                        <table id="DatosAdjuntos" style="height: 100%;">
                        </table>
                        <div id="DatosAdjuntosPagerL">
                        </div>
                    </div>
                      <div class="row" style="padding-left:15px" > 
                           <input style="padding-left:5px" class="btn btn_ficha pull-center" id="DescargarArchivo" value="Descargar" type="button" />
                          </div>
                    </div>
                      </div>



           

           
              <div class="row">

                    <div class="form-group has-feedback ">
                   <div class="modal-footer">
                      <input id="Cerrar" type="button" value="Cerrar" class="btn btn_ficha pull-left" />
                   <%--    <input class="btn btn_ficha pull-center" id="descargar" value="Descargar Adjuntos" type="button" />--%>
                      <input id="Button1" type="button" value="Responder" class="btn btn_ficha pull-right" />

                    </div>
                  

                       
                  </div>
                  </div>

               
            
       
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScripts" runat="Server">
</asp:Content>


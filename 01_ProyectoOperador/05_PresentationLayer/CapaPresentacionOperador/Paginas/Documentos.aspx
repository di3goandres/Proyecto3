<%@ Page Title="" Language="C#" MasterPageFile="~/shared/Uniandes.master" AutoEventWireup="true" CodeFile="Documentos.aspx.cs" Inherits="Paginas_MisDocumentos_Documentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<link rel="stylesheet" type="text/css" href="jqueryFileTree.css" media="screen" />--%>
    <%--<link href="../dist/themes/default/style.min.css" rel="stylesheet" />--%>

    <%--    <script type="text/javascript" src="jquery-1.3.2.min.js"></script>--%>
    <%--<script type="text/javascript" src="jquery.easing.1.3.js"></script>
    <script type="text/javascript" src="jqueryFileTree.js"></script>--%>
    <%--<link href="../dist/themes/default/style.min.css" rel="stylesheet" />--%>
    <!--inicio para mostrar los archivos-->

    <link rel="stylesheet" href="./assets/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" href="./assets/dist/themes/default/style.min.css" />
    <link rel="stylesheet" href="./assets/docs.css" />
    <script src="../js/jquery.jqGrid.min.js" type="text/javascript"></script>
    <script src="http://code.jquery.com/jquery-1.11.1.min.js"></script>

    <script src="http://code.jquery.com/ui/1.11.1/jquery-ui.min.js"></script>




    <%--<link rel="stylesheet" href="https://code.jquery.com/ui/1.11.1/themes/smoothness/jquery-ui.css" />--%>
    <script src="assets/jquery.address-1.6.js"></script>
    <script src="assets/vakata.js"></script>
    <script src="assets/dist/jstree.min.js"></script>
    <script src="../js/jquery.fileupload.js"> </script>
    <script src="../js/jquery.iframe-transport.js"> </script>
    <script src="../CustomJS/MisDocumentos/Documentos.js"></script>


    <!--[if lt IE 9]><script src="./assets/respond.js"></script><![endif]-->


    <%--<script>window.$q = []; window.$ = window.jQuery = function (a) { window.$q.push(a); };</script>--%>
    <!--fin para mostrar los archivos-->


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">

    <table>
        <tr>
            <td>
                <table class="accountInfo" style="padding-right: 100px">
                    <tr>
                        <td>Mis Documentos</td>
                    </tr>
                    <tr style="width: 40%">
                        <td>
                            <%--  <div id="capa"></div>--%>

                            <div>
                                <div id="jstree2" class="demo" style="margin-top: 2em;"></div>
                            </div>

                        </td>

                    </tr>
                   
                </table>
            </td>
        </tr>
    </table>

    <div id="cargarArchivos">
        <div id="event_result"></div>
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
        <table>
            <tr>

                <td>
                    <input id="CancelarCargue" type="button" class="btn btn-default" value="Cerrar" /></td>
            </tr>
        </table>

    </div>



    <div id="EditarCarpeta" class="form;  col-md-6">
        <br />

        <table >
            <tr>
                <td style="width: 50%">
                    <label style="width: 80%">Nombre Carpeta</label></td>
                <td style="width: 50%">
                    <input id="textCarpeta" type="text" class="textEntry form-control" />
            </tr>
        </table>
        <table>
            <tr>

                <td style="width: 50%">
                    <input id="EditarCrearCarpeta" type="button" class="center-block btn_ficha btn-success" value="Crear" />
                    </td>
                <td style="width: 45%">
                    <input id="CerrarEditarCrearCarpeta" type="button" class="center-block btn_ficha btn-success" value="Cancelar" />
                </td>
            </tr>
        </table>
    </div>
    <%-- <div id="dialog-Busy" title="Procesando" style="display: none">
        <br />
        <b>Procesando, Un momento por favor...</b>
        <br />
        <br />
        <div style="text-align: center;">
            <%-- <span style="float: left; margin: 0 7px 50px 0;"></span><span id="Span1">
            <img id="ImgProcesando" alt="" border="0" src="../imagenes/ajax-loader2.gif" height="20px" />
            <%--  </span>
        </div>
    </div>--%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contentScripts" runat="Server">
</asp:Content>


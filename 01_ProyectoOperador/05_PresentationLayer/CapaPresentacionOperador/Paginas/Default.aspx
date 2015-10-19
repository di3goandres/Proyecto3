<%@ Page Title="" Language="C#" MasterPageFile="~/shared/Uniandes.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Paginas_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script src="../js/jquery.fileupload.js"> </script>
    <script src="../js/jquery.iframe-transport.js"> </script>
    <script src="../CustomJS/Default.js"></script>



    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--inicio del contenido   -->
    <div class="row">
        <div style="padding: 55px;">
        </div>
    </div>
    <div class="row">
        <div class="col-md-3"></div>
        <%--<div class="col-md-9"><img src="../img/intro.png" class="img-responsive" alt="intro"/></div>--%>
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
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScripts" runat="Server">
</asp:Content>


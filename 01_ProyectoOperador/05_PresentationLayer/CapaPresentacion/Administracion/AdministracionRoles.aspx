<%@ Page Title="" Language="C#" MasterPageFile="~/shared/Uniandes.master" AutoEventWireup="true" CodeFile="AdministracionRoles.aspx.cs" Inherits="Administracion_AdministracionRoles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../CustomJS/Administracion/AdministracionRoles.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <center> 
       <h1 style="font-size:medium;">Administración Roles</h1><div id="">
        <table id="Datos">
        </table>
        <div id="pagerL">
        </div>
    </div>
</center>

      <div id="EditarAgregar">

        <table>
          
            <tr>
                <td style="width: 20%">
                    <label>Nombre del Perfil</label></td>
                <td style="width: 100%">
                    <input type="text" id="NombrePerfil" style="width: 80%" /></td>

            </tr>
            <tr>
                <td style="width: 20%">
                    <label>Descripcion</label></td>
                <td style="width: 100%">
                    
                    <input type="text" id="Descripcion" style="width: 80%" /></td>
            </tr>

             <tr id="PrefijoTR">
                <td style="width: 20%">
                    <label>Prefijo</label></td>
                <td style="width: 100%">
                    
                    <input type="text" id="Prefijo" style="width: 80%" /></td>
            </tr>

        </table>

        <table class="table">
            <tr>

                <td>
                    <input id="EditarCrear" type="button" style="width: 50%" value="" />
                    <input id="Cancel" type="button" style="width: 40%" value="Cancel" />

                <td></td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contentScripts" runat="Server">
</asp:Content>


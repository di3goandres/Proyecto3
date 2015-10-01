<%@ Page Title="" Language="C#" MasterPageFile="~/shared/Uniandes.master" AutoEventWireup="true" CodeFile="AdminRolMenu.aspx.cs" Inherits="Administracion_AdminRolMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../CustomJS/Administracion/AdminRolMenu.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <center> 
       <h1 style="font-size:medium;">Administración Rol - Menús</h1><div id="Div2">
        <table id="Table1">
        </table>
        <div id="Div3">
        </div>
    </div>
    </center>


    <left>
        <table>
           
           <tr>
               <td>
                <div id="">
                        <table id="Datos" >
                        </table>
                        <div id="pagerL">
                        </div>
                    </div>
                   </td>
                  <td>
                <div id="Div1">
                        <table id="DatosMenus" >
                        </table>
                        <div id="pagerMenus">
                        </div>
                    </div>
           </td>

           </tr>
            </table>
          </left>

<div id="EditarAgregar" class="form";"col-md-6" >

        <table class="table" style="width: 550px; padding-left: 0px; margin-left: 0px; text-align: left">
               <tr>
                <td style="width: 20%">
                    <label style="width:100%">Perfil</label></td>
                <td style="width: 100%">
                    <select id="Perfil" class="form-control" style="width: 100%"></select></td>
            </tr>
           <tr>
                <td style="width: 20%">
                    <label></label>
                </td>
                <td style="width: 100%">
                    <input id="EditarCrear" type="button" class="btn btn-default" value="Asociar" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contentScripts" runat="Server">
</asp:Content>


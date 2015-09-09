<%@ Page Title="" Language="C#" MasterPageFile="~/shared/Uniandes.master" AutoEventWireup="true" CodeFile="CreacionMenu.aspx.cs" Inherits="Administracion_CreacionMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../CustomJS/Administracion/CreacionMenu.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <center><h1 style="font-size:medium;">Menú</h1></center>
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
                     <br />
                     <br />
                   
                  </left>

    <div id="EditarAgregar">

        <table>
            <tr>
                <td style="width: 20%">
                    <label>Es Padre </label>
                </td>
                <td style="width: 70%">
                    <input type="checkbox" id="esPadre" style="width: 80%" /></td>

            </tr>
            <tr>
                <td style="width: 20%">
                    <label>Nombre del Menu</label></td>
                <td style="width: 100%">
                    <input type="text" id="MenuName" style="width: 80%" /></td>

            </tr>
            <tr>
                <td style="width: 20%">
                    <label>Url</label></td>
                <td style="width: 100%">
                    <input type="text" id="url" style="width: 80%" /></td>
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

    <div id="EditarCrearMenuHijo">

        <table>

            <tr>
                <td style="width: 20%">
                    <label>Nombre del Menu</label></td>
                <td style="width: 100%">
                    <input type="text" id="MenuHijo" style="width: 80%" /></td>

            </tr>
            <tr>
                <td style="width: 20%">
                    <label>Url</label></td>
                <td style="width: 100%">
                    <input type="text" id="UrlHijo" style="width: 80%" /></td>
            </tr>

        </table>

        <table class="table">
            <tr>

                <td>
                    <input id="EditarCrearMenu" type="button" style="width: 50%" value="Agregar" />
                    <input id="CancelMenu" type="button" style="width: 40%" value="Cancel" />

                <td></td>
            </tr>
        </table>



        <div id="ActualizarMenus">

            <table>

                <tr>
                    <td style="width: 20%">
                        <label>Nombre del Menu</label></td>
                    <td style="width: 100%">
                        <input type="text" id="ActualizarMenu" style="width: 80%" /></td>

                </tr>
                <tr>
                    <td style="width: 20%">
                        <label>Url</label></td>
                    <td style="width: 100%">
                        <input type="text" id="ActualizarURL" style="width: 80%" /></td>
                </tr>

            </table>

            <table class="table">
                <tr>

                    <td>
                        <input id="ActuMenu" type="button" style="width: 50%" value="Actualizar" />
                        <input id="CancelActMenu" type="button" style="width: 40%" value="Cancel" />

                    <td></td>
                </tr>
            </table>

        </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contentScripts" runat="Server">
</asp:Content>


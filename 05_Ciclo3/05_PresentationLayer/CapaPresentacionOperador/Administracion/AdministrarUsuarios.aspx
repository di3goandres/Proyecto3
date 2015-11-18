<%@ Page Title="" Language="C#" MasterPageFile="~/shared/Uniandes.master" AutoEventWireup="true" CodeFile="AdministrarUsuarios.aspx.cs" Inherits="Administracion_AdministrarUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../CustomJS/Administracion/AdministrarUsuarios.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <center> 
       <h1 style="font-size:medium;">Administración Usuarios</h1><div id="">
        <table id="Datos">
        </table>
        <div id="pagerL">
        </div>
    </div>


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
                    <label>Usuario</label></td>
                <td style="width: 100%">
                    <input id="username" class="form-control" type="text" aria-describedby="basic-addon1" placeholder="Usuario"/></td>
            </tr>
          
            <tr>
                <td style="width: 20%">
                    <label>Email</label></td>
                <td style="width: 100%">
                    <input id="Email" class="form-control" type="text" aria-describedby="basic-addon1" placeholder="Contraseña inicial"/>
                </td>
            </tr>
             <tr>
                <td style="width: 20%">
                    <label>Pregunta secreta</label></td>
                <td style="width: 100%">
                    <select id="PreguntasSecretas" class="form-control" style="width: 100%"></select></td>
            </tr>
            
              <tr>
                <td style="width: 20%">
                    <label>Respuesta Secreta</label></td>
                <td style="width: 100%">
                    <input id="Respuesta"class="form-control" type="text" aria-describedby="basic-addon1" placeholder="Respuesta Secreta"/></td>
            </tr>
            <tr>
                <td style="width: 20%">
                    <label></label>
                </td>
                <td style="width: 100%">
                    <input id="EditarCrear" type="button" class="btn btn-default" value="Registrarse" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contentScripts" runat="Server">
</asp:Content>


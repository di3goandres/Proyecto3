<%@ Page Title="" Language="C#" MasterPageFile="~/shared/Uniandes.master" AutoEventWireup="true" CodeFile="HistorialEpisodios.aspx.cs" Inherits="Pacientes_HistorialEpisodios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <script src="../CustomJS/Pacientes/ConsultasPacientes.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
     <div id="HistorialEpisodios">
        <center> 
          <h1 style="font-size:medium;">Historial Episodios Usuarios</h1>
       
       
          
        <table id="DatosH">
        </table>
        <div id="pagerLH">
        </div>
        </center>
        <table class="table-filtro-busqueda">
            <tr>
                <td>
                   
            </tr>
        </table>

    </div>

   
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contentScripts" Runat="Server">
</asp:Content>


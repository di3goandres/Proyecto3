<%@ Page Title="" Language="C#" MasterPageFile="~/shared/Uniandes.master" AutoEventWireup="true" CodeFile="ConsultaPacientes.aspx.cs" Inherits="Doctores_ConsultaPacientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>

<script src="http://www.google.com/jsapi" type="text/javascript"></script>
    <script type="text/javascript">
        // Global variable to hold data
        // Load the Visualization API and the piechart package.
        google.load('visualization', '1', { packages: ['corechart'] });
    </script>
    <script src="../CustomJS/Doctores/DoctorConsultarPacientes.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <center> 
       <h1 style="font-size:medium;">Consultar Paciente</h1>
       
           </center>
    <div id="Pacientes">
        <table class="table-filtro-busqueda">
            <tr>
                <td>
                    <label>Número de identificación</label></td>
                <td>
                    <input id="identificacion" type="text" placeholder="Número de Identificación" /></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <input id="consultar" value="Consultar" style="width: 100px" /></td>
            </tr>
        </table>
        <br />
        <br />

        <center> 
          
        <table id="Datos">
        </table>
        <div id="pagerL">
        </div>
        </center>
    </div>
     <div id="HistorialEpisodios">
        <center> 
          <h1 style="font-size:medium;">Historial Episodios Usuarios</h1>
       
        <table class="table-filtro-busqueda" style="width:60%">
            <tr>
                <td>
                    <label>Fecha Inicial</label></td>
                <td>
                    <input id="fecha1"  /></td>
            
                <td>
                    <label>Fecha Final</label></td>
                <td>
                    <input id="fecha2"   /></td>
            </tr>
            <tr>
            
        </table>
            <br />
        <table class="table-filtro-busqueda" style="width:60%" >
            <tr>
                <td>
                    <input id="botonconsultar" value="Consultar"  style="width: 100px" /><%--</td><td>--%>
                     <input id="Graficos" value="Ver Graficos" style="width: 110px" type="button" /> 
                  
                    <input id="atras" value="Atras" style="width: 100px" type="button" />

                </td>
            </tr>
        </table>
          <br />
        <div id="FiltrosBu">
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
    </div>
    <center>
    <div id="popupGraficos">
         <h1 style="font-size:medium;">Graficos Episodios Usuarios</h1>

        <table class="table-bordered">

            <tr>

                <td> <div id="PieChartsDIV" style="width:450px" >
        </div></td>
                <td> <div id="ColumnChartdiv" style="width:450px">
        </div></td>

            </tr>
              <tr>

                <td> <div id="PieChartsDIV2" style="width:450px" >
        </div></td>
                <td> <div id="ColumnChartdiv2" style="width:450px">
        </div></td>

            </tr>
        </table>
     
        <br />
       
    </center>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contentScripts" runat="Server">
</asp:Content>


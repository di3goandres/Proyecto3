<%@ Page Title="" Language="C#" MasterPageFile="~/shared/Uniandes.master" AutoEventWireup="true" CodeFile="RegistrarEpisodio.aspx.cs" Inherits="Pacientes_RegistrarEpisodio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script src="../CustomJS/Pacientes/RegistrarSintoma.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <center> 
          <h1 style="font-size:medium;">Registrar Episodios</h1>

      <p>Completa el siguiente formulario para introducir la información nueva. Cuando termines, haz clic en <strong>Enviar registro</strong> para guardarlo.</p>
</center>
    <center> 
     <table class="tableIndicadores" style="width:50%">

         <tr >
             <td>
                 <table">

                <tr>

                <td>Fecha*</td>
                <td><input id="fechaRegistro"/>  </td>
                
              <%--  <td>Hora*</td>
                <td><input id="hora" placeholder="Formato hh:mm"/></td </tr>>--%>
    </tr>
              <tr>
                
             <%--   <td>Duracion Horas*</td>

                <td><input id="horasduracion" placeholder="horas"/>  </td>--%>
                
                <td>Minutos</td>
                <td><input id="minutos" placeholder="Minutos" /></td>
            </tr>
                       <tr>
                <td>Intensidad</td>
                <td>
                 <select id="selectIntensidad" >
						<option value="0" selected="selected">Seleccione una opcion</option>
						<option value="1">Leve</option>
						<option value="2">Moderada</option>
						<option value="3">Fuerte</option>

					</select>
                    </td>  <td></td>
                 <td></td>

            </tr>
     </table>
     </table>
        <br />
        <br />

         <table class="tableIndicadores" border="2" style="width:670px">
        <tr>
            <td style="width: 50%; margin-top: auto; vertical-align: top">
                <div>
                    <table id="pregunta9"></table>
                    <table id="pre9"></table>
                </div>
            </td>
           <%-- </tr>
           <tr>--%>
            <td style="width: 50%; margin-top: auto; vertical-align: top">
                <div>
                    <table id="pregunta10"></table>
                    <table id="pre10"></table>

                </div>
            </td>

        </tr>
          </table>
   
			
     <p class="notas">Los campos obligatorios se indican con un asterisco(*).<br>
Para las lecturas de colesterol actuales, introduce al menos uno de los valores del colesterol.<br>
Para obtener más ayuda, imprime Cómo Analizar los Resultados y las Metas con su Profesional de la Salud.</p>
    


             <table>

                 <tr>

                     <td>
                         <input type="button" value="registrar" id="registrar"/>
                     </td>
                    
                 </tr>
             </table>

                  
          
         </table>
         </center>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contentScripts" runat="Server">
</asp:Content>


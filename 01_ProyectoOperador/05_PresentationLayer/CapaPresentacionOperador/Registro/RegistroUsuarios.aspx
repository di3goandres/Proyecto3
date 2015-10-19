<%@ Page Title="" Language="C#" MasterPageFile="~/shared/MasterPageOutside.master" AutoEventWireup="true" CodeFile="RegistroUsuarios.aspx.cs" Inherits="Registro_RegistroUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script src="../CustomJS/Usuarios/Registrar.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentScripts" runat="Server">
    <h3>Registro usuarios</h3>

    <br />
    <table class="tableinside">

        <tr>
            <td>
                <a href="../login.aspx" target="_self"><-Regresar</a>
            </td>
        </tr>
    </table>
    <center>
    <div id="EditarAgregar" class="form">

        <table class="tableIndicadores" border="0" style="width: 840px; padding-left: 0px; margin-left: 0px; text-align: left">
            <tr>
                  <td >
                    <label>Primer Nombre</label></td>
                <td>
                    <input id="NombreI" type="text"/></td>
                 <td  >
                    <label>Segundo Nombre</label></td>
                <td >
                    <input id="NombreII" type="text" /></td>
           
              
            </tr>
            <tr>
                  <td >
                    <label>Primer Apellido</label></td>
                <td>
                    <input id="ApellidosI" type="text"/></td>
                 <td  >
                    <label>Segundo Apellido</label></td>
                <td >
                    <input id="ApellidosII" type="text" /></td>
           
              
            </tr>
             <tr>
                <td >
                    <label>Tipo de Identificación</label></td>
                <td >
                    <select id="TipoIdentificacion" ></select>
                </td>
          
                <td >
                    <label>Número de Identificación </label>
                </td>
                <td >
                    <input id="NumeroIdentificacion" type="text"  onkeypress="return EvaluarTexto('Numeros',this,event);" /></td>
                      
            
               
            </tr>

             <tr>
                <td >
                    <label>Departamento expedicion</label></td>
                <td >
                    <select id="DepartamentoExpedicion" ></select>
                </td>
          
                <td >
                    <label>Municipio Expedicion</label>
                </td>
                <td >
                       <select id="MunicipioExpedicion" ></select>
                      
            
               
            </tr>
          <tr>
                <td >
                    <label>Fecha expedición</label></td>
                <td >
                                     <input id="fechaExpedicion" type="text"  /></td>

           
                <td >
                    <label>Genero</label></td>
                <td >
                   <select id="Generos" ></select>
             </tr>

            <tr>
                <td >
                    <label>Fecha nacimiento</label></td>
                <td >
                                     <input id="fechanacimiento" type="text"  /></td>

           
                <td >
                    <label>x</label></td>
                <td >
                   <select id="xx" ></select>
             </tr>


             <tr>
                <td >
                    <label>Departamento Nacimiento</label></td>
                <td >
                    <select id="DepartNacimiento" ></select>
                </td>
          
                <td >
                    <label>Municipio Nacimiento</label>
                </td>
                <td >
                       <select id="MNacimiento" ></select>
                      
            
               
            </tr>


               <tr>
                <td >
                    <label>Nacionalidad</label></td>
                <td >
                    <select id="Nacionalidad" ></select>
                </td>
          
                <td >
                    <label>Direccion de residencia</label>
                </td>
                <td >
                        <input id="dirresidencia" type="text"  /></td>
                      
            
               
            </tr>

             <tr>
                <td >
                    <label>Departamento Residencia</label></td>
                <td >
                    <select id="depResidencia" ></select>
                </td>
          
                <td >
                    <label>Municipio Residencia</label>
                </td>
                <td >
                       <select id="munResidencia" ></select>
                      
            
               
            </tr>

            <tr>
                <td >
                    <label>Teléfono</label></td>
                <td >
                    <input id="telefono" type="text"  onkeypress="return EvaluarTexto('Numeros',this,event);" maxlength="20" /></td>
           
                <td >
                    <label>Email</label></td>
                <td >
                    <input id="Email" type="text"  /></td>
             </tr>
             <tr>
                <td >
                    <label>Pregunta secreta</label></td>
                <td >
                    <select id="PreguntasSecretas" ></select></td>
                 <td >
                    <label>Respuesta Secreta</label></td>
                <td >
                    <input id="Respuesta" type="text"  /></td>
            </tr>
            
<%--              <tr>
                
            </tr>--%>
            <%--<tr>
                <td >
                    <label></label>
                </td>
                <td >
                    <input id="EditarCrear" type="button" style="width: 30%" value="Registrarse" />
                </td>
            </tr>--%>
        </table>
        <br />
        <br />

         <table>
              <tr> <td></td><td></td></tr>
          <tr>
                <td >
                    <label></label>
                </td>
                <td >
                    <input id="EditarCrear" type="button" style="width: 30%" value="Registrarse" />
                </td>
            </tr></table>
    </div>
</asp:Content>


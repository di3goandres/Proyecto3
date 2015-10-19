<%@ Page Title="" Language="C#" MasterPageFile="~/shared/MasterPageOutside.master" AutoEventWireup="true" CodeFile="RegistroUsuarios.aspx.cs" Inherits="Registro_RegistroUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script src="../CustomJS/Usuarios/Registrar.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    <div id="RegistroUsuarios" class="form">
        <div class="container_login" style="width:75%">
         <div class="row" style="width:75%">
            <h3 style="color:#603813">Datos Personales</h3>
        <table  class="tableregistro" style ="width: 740px; padding-left: 0px; margin-left: 0px; text-align: left;padding:4px" >
            <tr>
                  <td style ="width:49%;padding:5px">
                    <label >Primer Nombre</label>
                    <input   id="NombreI" type="text" class="TextEntry form-control"/></td>
                 <td style ="width:49%;padding:5px">
                    <label>Segundo Nombre</label>
                    <input     id="NombreII" type="text"  class="TextEntry form-control" /></td>
                </tr>
            <tr>
                <td style ="width:49%;padding:5px">
                    <label>Primer Apellido</label>
                    <input   id="ApellidosI" type="text" class="TextEntry form-control" /></td>
                    <td style ="width:49%;padding:5px">
                    <label>Segundo Apellido</label>
                    <input  id="ApellidosII" type="text" class="TextEntry form-control"/></td>

             </tr> 

            <tr>
                  <td style ="width:49%;padding:5px"> <label>Tipo de Identificación</label>
                    <select id="TipoIdentificacion" class="TextEntry form-control" ></select>
                  </td>
                  <td style ="width:49%;padding:5px"><label>Número de Identificación </label>
                    <input class="TextEntry form-control" id="NumeroIdentificacion" type="text"  onkeypress="return EvaluarTexto('Numeros',this,event);" />
                  </td>
            </tr>
            <tr>
                 <td style ="width:49%;padding:5px"> 
                    <label>Departamento expedicion</label>
                    <select id="DepartamentoExpedicion" class="TextEntry form-control" ></select>
                </td>
                 <td style ="width:49%;padding:5px"> 
                    <label>Municipio Expedicion</label>
                    <select id="MunicipioExpedicion" class="TextEntry form-control"  ></select></td>
            </tr>
            <tr>
                <td style ="width:49%;padding:5px"> 
                <label>Fecha expedición</label>
                <input id="fechaExpedicion" type="text"  class="TextEntry form-control" /></td>
                <td style ="width:49%;padding:5px"> 
                <label>Genero</label>
                <select id="Generos" class="TextEntry form-control" ></select>
            </tr>

             <tr>
                  <td style ="width:49%;padding:5px"> 
                    <label>Departamento Nacimiento</label>
                    <select id="DepartNacimiento" class="TextEntry form-control"  ></select>
                 </td>
                 <td style ="width:49%;padding:5px"> 
                    <label>Municipio Nacimiento</label>
                    <select id="MNacimiento" class="TextEntry form-control" ></select>
                  </td>
            </tr>
            <tr>
               <td style ="width:49%;padding:5px"> 
                    <label>Fecha nacimiento</label>
                                     <input id="fechanacimiento" type="text"   class="TextEntry form-control"/></td>

              <td style ="width:49%;padding:5px"> 
                    <label>Nacionalidad</label><select id="Nacionalidad" class="TextEntry form-control"></select>
                </td>
             </tr>
            <tr>
                <td style ="width:49%;padding:5px">
                    <label>Departamento Residencia</label>
                    <select id="depResidencia"  class="TextEntry form-control"></select>
                </td>
          
                <td style ="width:49%;padding:5px">
                    <label>Municipio Residencia</label>
                    <select id="munResidencia" class="TextEntry form-control"></select>
              </td>
          
               
            </tr>
               <tr>
                 <td style ="width:49%;padding:5px">
                    <label>Direccion de residencia</label>
                        <input id="dirresidencia" type="text"  class="TextEntry form-control" />
                 </td>
                   <td style ="width:49%;padding:5px">
                    <label>Teléfono</label>
                    <input  class="TextEntry form-control"  id="telefono" type="text"  onkeypress="return EvaluarTexto('Numeros',this,event);" maxlength="20" /></td>
           
            </tr>

            <tr>
               
               <td style ="width:49%;padding:5px">
                    <label>Email</label>
                    <input id="Email" type="text"  class="TextEntry form-control" /></td>
                 <td style ="width:49%;padding:5px"></td>
             </tr>
             <tr>
               <td style ="width:49%;padding:5px">
                    <label>Pregunta secreta</label>
                    <select id="PreguntasSecretas"  class="TextEntry form-control"></select></td>
                 <td style ="width:49%;padding:5px">
                    <label>Respuesta Secreta</label>    
                    <input id="Respuesta" type="text"  class="passwordEntry form-control" type="password" /></td>
            </tr>


        </table>

        <br />
        <br />

         <table>
             
          <tr>
               
                <td >
                    <input id="EditarCrear" type="button" value="Registrarse"   class="loginButton center-block btn_cafe btn-success btn"/>
                </td>
            </tr></table>
              <br />
        <br />  <br />
        <br />
    </div>
        </div>
    </div>  
     <div id="ValidarRegistro" class="form">
        <div class="container_login" style="width:75%">
             <h3 style="color:#603813">Registro Usuarios</h3>
              <table  class="tableregistro" style ="width: 740px; padding-left: 0px; margin-left: 0px; text-align: left;padding:4px" >
                   <tr>
                  <td style ="width:49%;padding:5px"> <label>Tipo de Identificación</label>
                    <select id="ValidarTipoIdentificacion" class="TextEntry form-control" ></select>
                  </td>
                  <td style ="width:49%;padding:5px"><label>Número de Identificación </label>
                    <input class="TextEntry form-control" id="ValidarNumeroIdentificacion" type="text"  onkeypress="return EvaluarTexto('Numeros',this,event);" />
                  </td>
                   </tr>
                   </table>
             <table>
             
          <tr>
               
                <td style ="width:49%;padding:5px">
                    <input id="BotonValidarRegistro" type="button" value="Validar"   class="loginButton center-block btn_cafe btn-success btn"/>
                </td>
            </tr></table>
         </div>
         </div>

        </center>

    <br />
    <br />
    <br />
    <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentScripts" runat="Server">
</asp:Content>


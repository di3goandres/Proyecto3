<%@ Page Title="" Language="C#" MasterPageFile="~/shared/Uniandes.master" AutoEventWireup="true" CodeFile="DatosPersonales.aspx.cs" Inherits="Registro_DatosPersonales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <%--<link rel="stylesheet" href="./assets/bootstrap/css/bootstrap.min.css" />--%>
    <%-- <link rel="stylesheet" href="./assets/dist/themes/default/style.min.css" />--%>
    <link rel="stylesheet" href="./assets/docs.css" />
    <script src="../CustomJS/Usuarios/DatosPersonales.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <center>
   <div id="RegistroUsuarios" class="form">
       <div class="container_login" style="width:75%">
         <div class="row" style="width:75%">
            <h3 style="color:#603813">Datos Personales</h3>
        <table  class="tableregistro" style ="width: 740px; padding-left: 0px; margin-left: 0px; text-align: left;padding:4px" >
            <tr>
                  <td style ="width:49%;padding:5px">
                    <label >Primer Nombre</label>
                    <span   id="NombreI" class="TextEntry form-control"/></td>
                 <td style ="width:49%;padding:5px">
                    <label>Segundo Nombre</label>
                     <span   id="NombreII" class="TextEntry form-control"/></td>
                 
                </tr>
            <tr>
                <td style ="width:49%;padding:5px">
                    <label>Primer Apellido</label>
                      <span   id="ApellidosI" class="TextEntry form-control"/></td>
                    
                    <td style ="width:49%;padding:5px">
                    <label>Segundo Apellido</label>
                          <span   id="ApellidosII" class="TextEntry form-control"/></td>
                 

             </tr> 

            <tr>
                  <td style ="width:49%;padding:5px"> <label>Tipo de Identificación</label>
                    <select id="TipoIdentificacion" class="TextEntry form-control" disabled="disabled" ></select>
                  </td>
                  <td style ="width:49%;padding:5px"><label>Número de Identificación </label>
                       <span   id="NumeroIdentificacion" class="TextEntry form-control"/></td>
                    
                 
            </tr>
            <tr>
                 <td style ="width:49%;padding:5px"> 
                    <label>Departamento expedicion</label>
                    <select id="DepartamentoExpedicion" class="TextEntry form-control"  disabled="disabled"></select>
                </td>
                 <td style ="width:49%;padding:5px"> 
                    <label>Municipio Expedicion</label>
                     <span   id="MunicipioExpedicion" class="TextEntry form-control"/></td>
                  
            </tr>
            <tr>
                <td style ="width:49%;padding:5px"> 
                <label>Fecha expedición</label>
                      <span   id="fechaExpedicion" class="TextEntry form-control"/></td>
             
                <td style ="width:49%;padding:5px"> 
                <label>Genero</label>
                    <span   id="Generos" class="TextEntry form-control"/></td>
               
            </tr>

             <tr>
                  <td style ="width:49%;padding:5px"> 
                    <label>Departamento Nacimiento</label>
                    <select id="DepartNacimiento" class="TextEntry form-control"  disabled ="disabled"></select>
                 </td>
                 <td style ="width:49%;padding:5px"> 
                    <label>Municipio Nacimiento</label>
                      <span   id="MNacimiento" class="TextEntry form-control"/></td>
                  
                 
            </tr>
            <tr>
               <td style ="width:49%;padding:5px"> 
                    <label>Fecha nacimiento</label>
                    <span   id="fechanacimiento" class="TextEntry form-control"/></td>
                                    

              <td style ="width:49%;padding:5px"> 
                    <label>Nacionalidad</label>
                                      <span   id="Nacionalidad" class="TextEntry form-control"/></td>
                  
              
             </tr>
          </table>
             </div>
             <div class="row" style="width:75%">
                  <h3 style="color:#603813">Datos de Ubicación </h3>
             <table  id="UbicacionVivienda" class="tableregistro" style ="width: 740px; padding-left: 0px; margin-left: 0px; text-align: left;padding:4px" >

            <tr>
                <td style ="width:49%;padding:5px">
                    <label>Departamento Residencia</label>
                    <select id="depResidencia"  class="TextEntry form-control" disabled ="disabled"></select>
                </td>
          
                <td style ="width:49%;padding:5px">
                    <label>Municipio Residencia</label>
                       <span   id="munResidencia" class="TextEntry form-control"/></td>
                   
          
          
               
            </tr>
               <tr>
                 <td style ="width:49%;padding:5px">
                    <label>Direccion de residencia</label>
                       <span   id="dirresidencia" class="TextEntry form-control"/></td>
                 
                   <td style ="width:49%;padding:5px">
                    <label>Teléfono</label>
                        <span   id="telefono" class="TextEntry form-control"/></td>
                    
           
            </tr>

            <tr>
               
               <td style ="width:49%;padding:5px">
                    <label>Email</label>
                <span   id="Email" class="TextEntry form-control"/></td>
                 <td style ="width:49%;padding:5px"></td>
             </tr>
            


        </table>

       <div id="a"  >
       <table> 
          <tr>
                <td >
                    <input id="EditarCrear" type="button" value="Editar Direccion Residencia" class="center-block btn btn_ficha "/>
                </td>
            </tr>
       </table>
        </div>
        <br />
    </div>
       

           
        </div>
      
    </div>  
   </center>

    <br />
    <br />
    <br />
    <br />
    <div id="editarLugarUbicacio">

        <table>

            <tr>
                <td style="width: 49%; padding: 5px">
                    <label>Departamento Residencia</label>
                    <select id="departamentoResidenciaActuali" class="TextEntry form-control"></select>
                </td>

                <td style="width: 49%; padding: 5px">
                    <label>Municipio Residencia</label>
                    <select id="MunicipioResidenciaActu" class="TextEntry form-control"></select>
                </td>


            </tr>
            <tr>
                <td style="width: 49%; padding: 5px">
                    <label>Direccion de residencia</label>
                    <input id="direccionResActu" type="text" class="TextEntry form-control" />
                </td>
                <td style="width: 49%; padding: 5px">
                    <label>Teléfono</label>
                    <input class="TextEntry form-control" id="telefonoActu" type="text" onkeypress="return EvaluarTexto('Numeros',this,event);" maxlength="20" /></td>

            </tr>
        </table>
        <div id="Div1">
            <table>
                <tr>
                    <td>
                        <input id="Actualizar" type="button" value="Editar Direccion Residencia" class="center-block btn btn_ficha " />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contentScripts" runat="Server">
</asp:Content>


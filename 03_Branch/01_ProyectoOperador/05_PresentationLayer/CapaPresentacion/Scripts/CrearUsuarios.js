var pagina ="CrearUsuarios.aspx/"

$(function () {
    TraerInformacionInicial();
    agregarAlselect();
    $("#validarYcrear").click(function () {
        validarYagregar();
    });
});


function TraerInformacionInicial() {
    DoJsonRequestBusy(pagina, "/TraerInformacionInicial", cargarDatosInicales, '{}');
}



function cargarDatosInicales(jsonrequest) {
    var data = jsonrequest.d;
    
    if (data.Ok == "Error Consultando información inicial.") {
        AlertUI(".:Error", data.mensaje);
        return false;
    }
    
    if (data.Ok == "OK") {
        fillSelect($("#TipoIdentificacion"), data.TIPOIDENTIFICACION);
       // fillSelect($("#SelectPerfil"), data.PERFILES);
        //$("#nombre_proyecto").text(data.PROYECTO.NOMBRE_PROYECTO);
        //$("#codigo_proyecto").text(data.PROYECTO.CODIGO_PROYECTO);
        //var option = '<option value="' + "0" + '">' + "SELECCIONE UNA OPCIÓN" + '</option>';
        //($('#municipio').append(option));
    }
}


function agregarAlselect() {

    agregarOpcionesAlSelect("0", "SELECCIONE UNA OPCIÓN", "#SelectPerfil");
    agregarOpcionesAlSelect("BANSAT", "BANSAT", "#SelectPerfil");
    agregarOpcionesAlSelect("DISTRIBUIDOR", "DISTRIBUIDOR", "#SelectPerfil");
    agregarOpcionesAlSelect("CLIENTEFINAL", "CLIENTE FINAL", "#SelectPerfil");




}
function validarYagregar() {
  


    var parametros = {};
    var n = false;
    n = validarYAgregarDatos("#NombreRazonSocial", "input", "Digite el nombre o Razón social", "NOMBRE_RAZON_SOCIAL", parametros);
    if (!n) return;

    n = validarYAgregarDatos("#TipoIdentificacion", "select", "seleccione el tipo de identificación.", "TIPO_IDENTIFICACION", parametros);
    if (!n) return;

    n = validarYAgregarDatos("#NumeroDeIdentificacion", "input", "Ingrese el numero de identificacion, ", "NUMERO_IDENTIFICACION", parametros);
    if (!n) return;

    n = validarYAgregarDatos("#SelectPerfil", "select", "seleccione el tipo de perfil para asignar.", "PERFILP", parametros);
    if (!n) return;
    n = validarYAgregarDatos("#Username", "input", "Ingrese el nombre de usuario que desea.", "userName", parametros);
    if (!n) return;

    //n = validarYAgregarDatos("#Password", "input", "Ingrese una contraseña por favor", "Password", parametros);
    //if (!n) return;

    n = validarYAgregarDatos("#Email", "email", "Seleccione un municipio por favor", "Email", parametros);
    if (!n) return;

    //n = validarYAgregarDatos("#SecurityAnswer", "input", "Ingrese la respuesta secreta.", "SecurityAnswer", parametros);
    //if (!n) return;

  


    var Pasar = $.toJSON(parametros);
    DoJsonRequestBusy(pagina, 'CrearUsuario', resultadoGuardar, Pasar);

}

function LimpiarCampos() {
    $("#NombreRazonSocial").val("");
    $("#TipoIdentificacion").val("0");
    $("#SelectPerfil").val("0");
    $("#NumeroDeIdentificacion").val("");
    $("#Username").val("");
    $("#Email").val("");



}

function resultadoGuardar(jsonrequest) {
    var data = jsonrequest.d;
    if (data.status == "OK") {
        AlertUI(".:Información", data.mensaje.toString(), function () {

            LimpiarCampos();
        });
    }
    if (data.status == "Existe") {
        AlertUI(".:Información", data.mensaje.toString(), function () {

            
        });
    }
    if (data.status == "error") {
        AlertUI(".:Error", data.mensaje.toString());
        return;
    }
}
var pagina = "RecuperarContrasenaMail.aspx/";

$(function () {   
    TraerInformacionInicial();
    //agregarAlselect();
    $("#EnviarEmail").click(function () {
        validarYEnviarEmail();
    });
});

function validarYEnviarEmail() {
    var parametros = {};
    var n = false;
    n = validarYAgregarDatos("#userName", "input", "Ingrese el nombre su usuario con el cual ingresa", "userName", parametros);
    if (!n) return;
    n = validarYAgregarDatos("#PreguntaSelect", "select", "Seleccione la pregunta secreta.", "Pregunta", parametros);
    if (!n) return;
    n = validarYAgregarDatos("#respuestaSecreta", "input", "Ingrese la respuesta secreta a la pregunta", "respuestaSecreta", parametros);
    if (!n) return;
    var Pasar = $.toJSON(parametros);
    DoJsonRequestBusy(pagina, 'RecuperarContrasenaEmail', resultadoGuardar, Pasar);
}


function LimpiarDatos(){
    $("#userName").val("");
    $("#PreguntaSelect").val("0");
    $("#respuestaSecreta").val("");
}

function resultadoGuardar(jsonrequest) {
    var data = jsonrequest.d;
    if (data.Ok == "OK") {
        AlertUI(".:Información", data.mensaje.toString(), function () {
            LimpiarDatos();
            document.location.target = "self";
            document.location.href = '../Login.aspx';
        });
        LimpiarDatos();
    }
    else {
        AlertUI(".:Información", data.mensaje.toString());
        return;
    }
}

function TraerInformacionInicial() {
    DoJsonRequestBusy(pagina, "TraerInformacionInicial", cargarDatosInicales, '{}');
}

function cargarDatosInicales(jsonrequest) {
    var data = jsonrequest.d;

    if (data.Ok == "Error Consultando información inicial.") {
        AlertUI(".:Error", data.mensaje);
        return false;
    }
    if (data.Ok == "OK") {
        fillSelect($("#PreguntaSelect"), data.PREGUNTAS);      
    }
}
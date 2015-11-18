var pagina = "CambiarContrasenia.aspx/";

$(function () {
    TraerInformacionInicial();    
    $("#CambiarContrasena").button().click(function () {
        validarYCambiarContrasena();
    });
});

function validarYCambiarContrasena() {
    var parametros = {};
    var n = false;
    n = validarYAgregarDatos("#PasswordOld", "input", "Ingrese la contraseña anterior por favor", "PasswordOld", parametros);
    if (!n) return;
    n = validarYAgregarDatos("#PasswordNew", "input", "Ingrese la nueva contraseña por favor", "PasswordNew", parametros);
    if (!n) return;
    n = validarYAgregarDatos("#PreguntaSelect", "select", "Seleccione la pregunta secreta.", "PreguntaSelect", parametros);
    if (!n) return;
    n = validarYAgregarDatos("#respuestaSecreta", "input", "Ingrese la respuesta secreta a la pregunta", "respuestaSecreta", parametros);
    if (!n) return;

    var Pasar = $.toJSON(parametros);
    DoJsonRequestBusy(pagina, 'CambiarContrasena', resultadoGuardar, Pasar);
}


function LimpiarDatos() {
    $("#PasswordOld").val("");
    $("#PasswordNew").val("");
    $("#PreguntaSelect").val("0");
    $("#respuestaSecreta").val("");
}

function resultadoGuardar(jsonrequest) {
    var data = jsonrequest.d;
    if (data.Ok == "OK") {
        AlertUI(".:Información", data.mensaje.toString(), function () {
            LimpiarDatos();
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
var pagina = "AsignarRespuestaSecretaContrasenia.aspx/";

$(function () {   
    TraerInformacionInicial();
    //agregarAlselect();
    $("#validarYcrear").button().click(function () {
        validarYagregar();
    });
});

function validarYagregar() {
    var parametros = {};
    var n = false;
    n = validarYAgregarDatos("#PreguntaSelect", "select", "Seleccione una pregunta por favor", "PREGUNTA", parametros);
    if (!n) return;
    n = validarYAgregarDatos("#respuesta", "input", "Ingrese la respuesta secreta.", "SecurityAnswer", parametros);
    if (!n) return;
    n = validarYAgregarDatos("#PasswordOld", "input", "Ingrese una contraseña por favor", "contraseñaAnterior", parametros);
    if (!n) return;
    n = validarYAgregarDatos("#Password", "input", "Ingrese una contraseña nueva por favor", "Password", parametros);
    if (!n) return;

    var Pasar = $.toJSON(parametros);
    DoJsonRequestBusy(pagina, 'CambioContrasenaRespuestaSecreta', resultadoGuardar, Pasar);
}

function resultadoGuardar(jsonrequest) {
    var data = jsonrequest.d;
    if (data.Ok == "OK") {
        AlertUI(".:Información", data.mensaje.toString(), function () {
            location.reload();
        });
    }
    if (data.Ok == "Contra") {
        AlertUI(".:Información", data.mensaje.toString());
        return;
    }
    if (data.Ok == "error") {
        AlertUI(".:Error", data.mensaje.toString());
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

var pagina = "DesbloquearUsuario.aspx/";

$(function () {
    $("#ValidarYDesbloquear").button().click(function () {
        validarYDesbloquearusuario();
    });
});

function validarYDesbloquearusuario() {
    var parametros = {};
    var n = false;
    n = validarYAgregarDatos("#NombreUsuario", "input", "Ingrese un nombre de usuario para desbloquear", "NombreUsuario", parametros);
    if (!n) return;

    var Pasar = $.toJSON(parametros);
    DoJsonRequestBusy(pagina, 'DesbloquearUsuario', resultadoDesbloquear, Pasar);
}

function LimpiarDatos() {
    $("#NombreUsuario").val("");
}

function resultadoDesbloquear(jsonrequest) {
    var data = jsonrequest.d;
    if (data.status == "OK") {
        AlertUI(".:Información", data.mensaje.toString(), function () {
            LimpiarDatos();
        });       
    }
    else {
        AlertUI(".:Información", data.mensaje.toString());
        return;
    }
}

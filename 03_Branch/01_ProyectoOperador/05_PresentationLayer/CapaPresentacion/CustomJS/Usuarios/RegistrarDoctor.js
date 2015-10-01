var pagina = "RegistrarDoctor.aspx/";


$(function () {
    
    TraerInformacionInicial();
    $("#EditarCrear").button().click(function () {
        ValidarEditarAgregar();
    });
   
});


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
        fillSelect($("#TipoIdentificacion"), data.TIPOIDENTIFICACION);
        //fillSelect($("#PreguntasSecretas"), data.PREGUNTAS);

      
    }
}




function ValidarEditarAgregar() {
    var parametros = {};
    var parametrosNOpasan = {}
    var n = false;
   
  
    n = validarYAgregarDatos("#NombreI", "input", "Ingrese su nombre por favor", "Nombres", parametros);
    if (!n) return;
    n = validarYAgregarDatos("#ApellidosI", "input", "Ingrese su Apellidos por favor", "Apellidos", parametros);
    if (!n) return;
    n = validarYAgregarDatos("#username", "input", "Ingrese su Username, por favor", "userName", parametros);
    if (!n) return;
    n = validarYAgregarDatos("#TipoIdentificacion", "select", "Seleccione un tipo de identificación", "TIPO_IDENTIFICACION", parametros);
    if (!n) return;

    n = validarYAgregarDatos("#NumeroIdentificacion", "input", "Ingrese el Número de identificación", "NUMERO_IDENTIFICACION", parametros);
    if (!n) return;
  

    n = validarYAgregarDatos("#Direccion", "input", "Ingrese una dirección, por favor ", "Direccion", parametros);
    if (!n) return;
    n = validarYAgregarDatos("#telefono", "input", "Ingrese un teléfono de contacto, por favor ", "telefono", parametros);
    if (!n) return;
    n = validarYAgregarDatos("#Email", "email", "", "Email", parametros);
    if (!n) return;
    //n = validarYAgregarDatos("#PreguntasSecretas", "select", "Seleccione una pregunta secreta, por favor", "passwordQuestion", parametros);
    //if (!n) return;
    //n = validarYAgregarDatos("#Respuesta", "input", "Ingrese su respuesta a la pregunta secreta, por favor ", "SecurityAnswer", parametros);
    if (!n) return;
   

    var Pasar = $.toJSON(parametros);
    DoJsonRequestBusy(pagina, 'CrearUsuario', resultadoGuardarEditar, Pasar);
}

function resultadoGuardarEditar(jsonrequest) {
    var data = jsonrequest.d;
    if (data.Ok == "OK") {
        $("#EditarAgregar").dialog('close');
        AlertUI(".:Información", data.mensaje.toString(), function () {
            document.location.target = "self";
            document.location.href = '../Login.aspx';
        });
    }
    else {
        AlertUI(".:Información", data.mensaje.toString());
        return
    }
}




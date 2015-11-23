var pagina = "EnviarMensaje.aspx/";


$(function () {

    $("#Mensaje").hide();

    $('#ValidarTipoIdentificacion').prop('disabled', false);
    $("#ValidarNumeroIdentificacion").prop('disabled', false);
    $("#ValidarNumeroIdentificacion").val("");
    $("#ValidarTipoIdentificacion").val("0");
    TraerInformacionInicial();
   

    $("#EnviarMensaje").button().click(function () {
        validarEnviarMensaje();
    });
    $("#BotonValidarRegistro").button().click(function () {
        ValidarRegistroUsuario();
    });
    LoadUploadFilesSend('#fileUploadSolicitud', '#ArchivosAdjuntosDiv', 'FileUploadMessage.ashx');

});


function validarEnviarMensaje() {

    var parametros = {};
    var n = false;
    n = validarYAgregarDatos("#Asunto", "input", "Ingrese el asunto del mensaje, por favor", "Asunto", parametros);
    if (!n) return;

    n = validarYAgregarDatos("#cuerpoMensaje", "input", "Ingrese el mensaje que desea enviar, por favor", "cuerpoMensaje", parametros);
    if (!n) return;
    var filenames = GetLoadFilenames('#ArchivosAdjuntosDiv');

    if (filenames == null || filenames == "") {
        AlertUI(".:Info", "por favor ingresar por lo menos, un archivo adjunto.");
        return;
    }
    parametros.FILENAMES = filenames;

    var Pasar = $.toJSON(parametros);
    DoJsonRequestBusy(pagina, 'EnviarMensaje', resultadoEnviarMensaje, Pasar);

   
}



function resultadoEnviarMensaje(jsonrequest) {
    var data = jsonrequest.d;
    if (data.OK == "OK") {
      

        AlertUI(".:Información", data.mensaje.toString(), function () {

            return;
        });
    }
    else {
        AlertUI(".:Información", data.mensaje.toString());
        return
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
        fillSelect($("#TipoIdentificacion"), data.TIPOIDENTIFICACION);
        fillSelect($("#ValidarTipoIdentificacion"), data.TIPOIDENTIFICACION);

        fillSelect($("#PreguntasSecretas"), data.PREGUNTAS);
        fillSelect($("#DepartamentoExpedicion"), data.DEPARTAMENTOS);
        fillSelect($("#Generos"), data.Generos);
        cambioDepartamento("DepartamentoExpedicion", "MunicipioExpedicion");
        fillSelect($("#DepartNacimiento"), data.DEPARTAMENTOS);
        cambioDepartamento("DepartNacimiento", "MNacimiento");


        fillSelect($("#depResidencia"), data.DEPARTAMENTOS);
        cambioDepartamento("depResidencia", "munResidencia");

        fillSelect($("#Nacionalidad"), data.Paises);

        $("#fechaExpedicion").datepicker({
            selectOtherMonths: true, changeYear: true,
            dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sa'],
            monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
            showOtherMonths: true,
            //maxDate: new Date(data.aniofechaIngresoMaxima, data.mesfechaIngresoMaxima, data.diafechaIngresoMaxima),
            yearRange: data.yearRange
        });
        $("#fechanacimiento").datepicker({
            selectOtherMonths: true, changeYear: true,
            dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sa'],
            monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
            showOtherMonths: true,
            //maxDate: new Date(data.aniofechaIngresoMaxima, data.mesfechaIngresoMaxima, data.diafechaIngresoMaxima),
            yearRange: data.yearRange
        });


    }
}



function ValidarRegistroUsuario() {
    var parametros = {};
    var parametrosNOpasan = {}
    var n = false;

    n = validarYAgregarDatos("#ValidarTipoIdentificacion", "select", "Seleccione un tipo de identificación", "TIPO_IDENTIFICACION", parametros);
    if (!n) return;
    n = validarYAgregarDatos("#ValidarNumeroIdentificacion", "input", "Ingrese el numero de identificacion por favor", "NUMERO_IDENTIFICACION", parametros);
    if (!n) return;


    var Pasar = $.toJSON(parametros);
    DoJsonRequestBusy(pagina, 'validar', resultadoValidar, Pasar);
}

function resultadoValidar(jsonrequest) {
    var data = jsonrequest.d;
    if (data.OK == "OK") {
        /**
        SE PROCEDE A ARMAR EL MENSAJE PARA ENVIAR.
        */
        $("#BotonValidarRegistro").hide();
        
        $("#Mensaje").show();
        $('#ValidarTipoIdentificacion').prop('disabled', true);
        $("#ValidarNumeroIdentificacion").prop('disabled', true);


        AlertUI(".:Información", data.mensaje.toString(), function () {

            return;
        });
    }
    else {
        AlertUI(".:Información", data.mensaje.toString());
        return
    }
}


function ValidarEditarAgregar() {
    var parametros = {};
    var parametrosNOpasan = {}
    var n = false;


    n = validarYAgregarDatos("#NombreI", "input", "Ingrese su nombre por favor", "NombresI", parametros);
    if (!n) return;

    parametros.NombresII = $("#NombreII").val();

    n = validarYAgregarDatos("#ApellidosI", "input", "Ingrese su Apellidos por favor", "ApellidosI", parametros);
    if (!n) return;

    parametros.ApellidosII = $("#ApellidosII").val();


    n = validarYAgregarDatos("#TipoIdentificacion", "select", "Seleccione un tipo de identificación", "TIPO_IDENTIFICACION", parametros);
    if (!n) return;


    n = validarYAgregarDatos("#NumeroIdentificacion", "input", "Ingrese el numero de identificacion por favor", "NUMERO_IDENTIFICACION", parametros);
    if (!n) return;

    n = validarYAgregarDatos("#DepartamentoExpedicion", "select", "Seleccione por favor el departamento de expedicion", "DeparExpedicion", parametrosNOpasan);
    if (!n) return;

    n = validarYAgregarDatos("#MunicipioExpedicion", "select", "Seleccione por favor el municipio de expedición", "MunExpedicion", parametros);
    if (!n) return;

    n = validarYAgregarDatos("#fechaExpedicion", "fecha", "Seleccione la fecha de expedición de su documento de identidad", "fechaExpedicion", parametros);
    if (!n) return;

    n = validarYAgregarDatos("#Generos", "select", "Seleccione por su genero", "Genero", parametros);
    if (!n) return;



    n = validarYAgregarDatos("#fechanacimiento", "fecha", "Seleccione la fecha de nacimiento", "fechaNacimiento", parametros);
    if (!n) return;


    n = validarYAgregarDatos("#DepartNacimiento", "select", "Seleccione por favor el departamento de nacimiento", "DeparNacimiento", parametrosNOpasan);
    if (!n) return;

    n = validarYAgregarDatos("#MNacimiento", "select", "Seleccione por favor el municipio de nacimiento", "MunNacimiento", parametros);
    if (!n) return;

    n = validarYAgregarDatos("#Nacionalidad", "select", "Seleccione por favor su nacionalidad", "Nacionalidad", parametros);
    if (!n) return;


    n = validarYAgregarDatos("#depResidencia", "select", "Seleccione por favor el departamento de residencia", "DeparResidencia", parametrosNOpasan);
    if (!n) return;

    n = validarYAgregarDatos("#munResidencia", "select", "Seleccione por favor el municipio de residencia actual", "munResidencia", parametros);
    if (!n) return;

    n = validarYAgregarDatos("#dirresidencia", "input", "Ingrese una dirección, por favor ", "DireccionResidencia", parametros);
    if (!n) return;
    n = validarYAgregarDatos("#telefono", "input", "Ingrese un teléfono de contacto, por favor ", "telefono", parametros);
    if (!n) return;
    n = validarYAgregarDatos("#Email", "email", "", "Email", parametros);
    if (!n) return;
    n = validarYAgregarDatos("#PreguntasSecretas", "select", "Seleccione una pregunta secreta, por favor", "passwordQuestion", parametros);
    if (!n) return;
    n = validarYAgregarDatos("#Respuesta", "input", "Ingrese su respuesta a la pregunta secreta, por favor ", "SecurityAnswer", parametros);
    if (!n) return;


    var Pasar = $.toJSON(parametros);
    DoJsonRequestBusy(pagina, 'CrearUsuario', resultadoGuardarEditar, Pasar);
}

function resultadoGuardarEditar(jsonrequest) {
    var data = jsonrequest.d;
    if (data.status == "OK") {
        $("#EditarAgregar").dialog('close');
        AlertUI(".:Información", data.mensaje.toString(), function () {
            document.location.target = "self";
            document.location.href = '../Login.aspx';
            return;

        });
    }
    else {
        AlertUI(".:Información", data.mensaje.toString());
        return
    }
}





function cambioDepartamento(datOrigen, DataDestino) {
    //secccion para los dropdownlist de las  departamntos y municipios
    $('#' + datOrigen).change(function () {
        $('#' + DataDestino).empty(); //se limpia para agregar los datos del departamento seleccionado
        if ($('#' + datOrigen).val() == "0") {
            $('#' + DataDestino).empty();   //se limpia tambien los datos del municipio 
            var option = '<option value="' + "0" + '">' + "Seleccione una opción" + '</option>';
            ($('#' + DataDestino).append(option));
        } else {
            var nommbreDepartamento = $('#' + datOrigen).val();

            var parametros = {};
            parametros.identificador = nommbreDepartamento;
            parametros.select = DataDestino;
            //var parametros = '{"identificador":"' + nommbreDepartamento + '"}';
            $('#' + DataDestino).empty();
            var Pasar = $.toJSON(parametros);
            DoJsonRequestBusy(pagina, 'CargarDatosDropDownMunicipio', CargarDatosParametricosMunicipio, Pasar);
        }
    });
}


function CargarDatosParametricosMunicipio(jsonrequest) {
    var respuesta = jsonrequest.d;
    fillSelect("#" + respuesta.select, respuesta.items);

}

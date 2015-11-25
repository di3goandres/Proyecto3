var pagina = "RegistroUsuarios.aspx/";


$(function () {

    $("#ValidarRegistro").show();
    $("#RegistroUsuarios").hide();

    var option = '<option value="' + "0" + '">' + "SELECCIONE UNA OPCIÓN" + '</option>';
    ($('#municipiosList').append(option));
    ($('#MNacimiento').append(option));
    ($('#munResidencia').append(option));
    ($('#MunicipioExpedicion').append(option));
    TraerInformacionInicial();
    $("#EditarCrear").button().click(function () {
        ValidarEditarAgregar();
    });

    $("#BotonValidarRegistro").button().click(function () {
        ValidarRegistroUsuario();
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
            dateFormat: 'dd/mm/yy',
            dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sa'],
            monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
            showOtherMonths: true,
            //maxDate: new Date(data.aniofechaIngresoMaxima, data.mesfechaIngresoMaxima, data.diafechaIngresoMaxima),
            yearRange: data.yearRange
        });
        $("#fechanacimiento").datepicker({
            selectOtherMonths: true, changeYear: true,
            dateFormat: 'dd/mm/yy',
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
        $("#TipoIdentificacion").val(data.TIPO_IDENTIFICACION);
        $("#NumeroIdentificacion").val(data.NUMERO_IDENTIFICACION);
        $('#NumeroIdentificacion').prop('disabled', true);
        $("#TipoIdentificacion").prop('disabled', true);


        AlertUI(".:Información", data.mensaje.toString(), function () {
            $("#ValidarRegistro").hide();
            $("#RegistroUsuarios").show();
            return;
        });
    }
    else if (data.OK == "Mismo") {
        ConfirmUI(".:Información","¿Deseas Recuperar la contraseña?"+  data.mensaje.toString(),
            function () {
                document.location.target = "self";
                document.location.href = '../RestablecerContrasena/RecuperarContrasenaMail.aspx';
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

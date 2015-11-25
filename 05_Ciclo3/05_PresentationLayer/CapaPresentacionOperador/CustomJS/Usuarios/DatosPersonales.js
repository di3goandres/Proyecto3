var pagina = "DatosPersonales.aspx/";


$(function () {

    //$("#ValidarRegistro").show();
    $("#editarLugarUbicacio").hide();

    //var option = '<option value="' + "0" + '">' + "SELECCIONE UNA OPCIÓN" + '</option>';
    //($('#MunicipioResidenciaActu').append(option));
    //($('#MNacimiento').append(option));
    //($('#munResidencia').append(option));
    //($('#MunicipioExpedicion').append(option));

   
    TraerInformacionInicial();
    $("#EditarCrear").button().click(function () {
        abrirEditarUbicacion();
    });

    $("#Actualizar").button().click(function () {
        ValidarEditarAgregar();
    });


});

function abrirEditarUbicacion() {

    $('#editarLugarUbicacio').dialog({
        width: 590, height: 280, modal: true, resizable: false, draggable: false, title: 'Editar Datos de Ubicación', closeOnEscape: true,
        open: function (event, ui) { $(".ui-dialog-titlebar-close", ui.dialog).show() }
    });
}


function TraerInformacionInicial() {
    DoJsonRequestBusy(pagina, "TraerInformacionInicial", cargarDatosInicales, '{}');
}

function cargarDatosInicales(jsonrequest) {
    var data = jsonrequest.d;
    if (data.Ok == "SESSIONEND") {
        AlertUI(".:Error", data.mensaje, function () {
            document.location.target = "self";
            document.location.href = '../Logoff.aspx';
            return;

        });
    }
    else if (data.Ok == "Error Consultando información inicial.") {
        AlertUI(".:Error", data.mensaje);
        return false;
    } else if (data.Ok == "OK") {
        fillSelect($("#TipoIdentificacion"), data.TIPOIDENTIFICACION);
        fillSelect($("#DepartamentoExpedicion"), data.DEPARTAMENTOS);
        fillSelect($("#departamentoResidenciaActuali"), data.DEPARTAMENTOS);
        fillSelect($("#departamentoResidenciaActuali"), data.DEPARTAMENTOS);

        fillSelect($("#MunicipioResidenciaActu"), data.MUNICIPIOSDEPARTAMENTO);
        $("#MunicipioResidenciaActu").val(data.USUARIO.idMunicipioResidencia);
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

        $("#TipoIdentificacion").val(data.USUARIO.idTipoIdentificacion);
        $("#DepartamentoExpedicion").val(data.USUARIO.idDepartamentoExpedicionDocumento);
        $("#departamentoResidenciaActuali").val(data.USUARIO.idDepartamentoResidencia);

        

        $("#munResidencia").text(data.USUARIO.NombreMunicipioResidencia);
        $("#depResidencia").val(data.USUARIO.idDepartamentoResidencia);
        $("#DepartNacimiento").val(data.USUARIO.idDepartamentoNacimiento);
        $("#MunicipioExpedicion").val(data.USUARIO.idDepartamentoExpedicionDocumento);
        $("#NombreI").text(data.USUARIO.primerNombre);
        $("#NombreII").text(data.USUARIO.segundoNombre);
        $("#ApellidosI").text(data.USUARIO.primerApellido);
        $("#ApellidosII").text(data.USUARIO.segundoApellido);
        $("#NumeroIdentificacion").text(data.USUARIO.numeroIdentificacion);
        $("#MunicipioExpedicion").text(data.USUARIO.NombreMunicipioExpedicionDocumento);
        $("#fechaExpedicion").text(data.fechaExpedicion);
        $("#fechanacimiento").text(data.fechaNacimiento);
        $("#MNacimiento").text(data.USUARIO.NombreMunicipioNacimiento);
        if (data.USUARIO.genero == "M")
            $("#Generos").text("MASCULINO");
        else
            $("#Generos").text("FEMENINO");
        $("#Nacionalidad").text(data.USUARIO.NombrePaisNacionalidad);
        $("#dirresidencia").text(data.USUARIO.direccionResidencia);
        $("#telefono").text(data.USUARIO.telefono);


        $("#direccionResActu").val(data.USUARIO.direccionResidencia);
        $("#telefonoActu").val(data.USUARIO.telefono);
        $("#Email").text(data.USUARIO.correoElectronico);
       

        cambioDepartamento("departamentoResidenciaActuali", "MunicipioResidenciaActu");
    }
}







function ValidarEditarAgregar() {
    var parametros = {};
    var parametrosNOpasan = {}
    var n = false;



    n = validarYAgregarDatos("#departamentoResidenciaActuali", "select", "Seleccione por favor el departamento de residencia", "DeparResidencia", parametrosNOpasan);
    if (!n) return;

    n = validarYAgregarDatos("#MunicipioResidenciaActu", "select", "Seleccione por favor el municipio de residencia actual", "munResidencia", parametros);
    if (!n) return;

    n = validarYAgregarDatos("#direccionResActu", "input", "Ingrese una dirección, por favor ", "DireccionResidencia", parametros);
    if (!n) return;
    n = validarYAgregarDatos("#telefonoActu", "input", "Ingrese un teléfono de contacto, por favor ", "telefono", parametros);
    if (!n) return;
   


    var Pasar = $.toJSON(parametros);
    DoJsonRequestBusy(pagina, 'ActualizarUsuario', resultadoGuardarEditar, Pasar);
}

function resultadoGuardarEditar(jsonrequest) {
    $('.ui-dialog-titlebar-close').show();
    var data = jsonrequest.d;
    if (data.Ok == "OK") {
        $("#EditarAgregar").dialog('close');
        $('#editarLugarUbicacio').dialog('close');
        AlertUI(".:Información", data.mensaje.toString(), function () {
          
            TraerInformacionInicial();

        });
    }
    else {
        AlertUI(".:Error", data.mensaje.toString());
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
    $('.ui-dialog-titlebar-close').show();
    var respuesta = jsonrequest.d;
    fillSelect("#" + respuesta.select, respuesta.items);

}

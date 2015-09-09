var pagina = "RegistrarEpisodio.aspx";


$(function () {
    $("#registrar").button().click(function () {
       
        validarDatosyGuardarBasededatos();
    });
    TraerdatosIniciales();
    
});

function validarDatosyGuardarBasededatos() {
    var parametros = {};
    var ListaSintomas = new Array();
    var ListaDesencadentes = new Array();

    var n = false;
    n = validarYAgregarDatos("#fechaRegistro", "input", "Seleccione, la fecha de la migraña", "fechaRegistro", parametros);
    if (!n) return;

    n = validarYAgregarDatos("#minutos", "input", "Ingrese la duracion en minutos del episodio de migraña", "minutos", parametros);
    if (!n) return;


    n = validarYAgregarDatos("#selectIntensidad", "select", "Seleccione la intensidad del episodio", "intensidad", parametros);
    if (!n) return;
   


    var checkbox11 = ObtenerValorCheckBoxSeleccionadosTypeCheck('form1', 'Sintomas_checkbox', true);

    if (checkbox11 == []) {
        AlertUI(".:info", "Seleccione, por lo menos un sintoma");
        return;
    }
    for (var i = 0; i < checkbox11.length; i++) {
       
        AgregarRespuestaArray(ListaSintomas, checkbox11[i].valor, "");
    }


    var checkbox12 = ObtenerValorCheckBoxSeleccionadosTypeCheck('form1', 'Catalizadores_checkbox', true);
    if (checkbox12 == []) {
        AlertUI(".:info", "Seleccione, por lo menos un desencadenante de migraña");
        return;
    }

    for (var i = 0; i < checkbox12.length; i++) {

        AgregarRespuestaArray(ListaDesencadentes, checkbox12[i].valor, "");
    }


    parametros.sintomas = ListaSintomas;
    parametros.catalizadore = ListaDesencadentes;
   
    var Pasar = $.toJSON(parametros);
    DoJsonRequestBusy(pagina, '/registraSintoma', resultadoGuardar, Pasar);
}


function resultadoGuardar(jsonrequest) {
    var data = jsonrequest.d;
    if (data.status == "OK") {

        AlertUI(".:Info", "Se ha registrado correctamente", function () {
            document.location.target = "self";
            document.location.href = '../Pacientes/RegistrarEpisodio.aspx';
        });

    }
    if (data.status == "error") {
        AlertUI(".:Error", data.MsgError.toString());
        return;
    }
}

function AgregarRespuestaArray(respuestasPreguntas, id_respuesta, otro) {

    ///se pasara todos los datos como lista par guardar en base de datos
    respuestasPreguntas.push({ ID: id_respuesta });
}

function TraerdatosIniciales() {
    DoJsonRequestBusy(pagina, "/TraerInformacionInicial", cargarDatosInicales, '{}');
}

function cargarDatosInicales(jsonrequest) {
    var data = jsonrequest.d;
    if (data.Ok == "Su session ha finalizado") {
        AlertUI('.:Información', 'Su session a Finalizado, por favor ingrese nuevamente');
        document.location.target = "self";
        document.location.href = '../../Logoff.aspx';
    }
    if (data.Ok == "Error Consultando información inicial.") {
        AlertUI(".:Error", data.MsgError);
        return false;
    }
   
    if (data.Ok == "OK") {

        $("#fechaRegistro").datepicker({
            dateFormat: 'dd/mm/yy', dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sa'],
            monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
            showOtherMonths: true,
            maxDate: new Date(data.aniofechaIngresoMaxima, data.mesfechaIngresoMaxima, data.diafechaIngresoMaxima),
            selectOtherMonths: true, changeYear: true
        });

        var resultado = data.preguntas;
        lleanrDivsPreguntas(resultado);



    }
}



function lleanrDivsPreguntas(data) {

    var datos = data


    var items = data;


    for (var i = 0; i < items.length; i++) {
        var numeroDiv = items[i].NUMERO_PREGUNTA;
        var pregunta = items[i].PREGUNTA;

        var cual = cualTabla(numeroDiv);
        var tablaActual = $(cual);

        var cualDivp = cualDiv(numeroDiv)


        armarDatos(items[i], cual, cualDivp, numeroDiv, pregunta);
    }


}



function cualTabla(numero) {

    var div = "";
    switch (numero) {

        case 3: div = "#pre9";
            break;
        case 4: div = "#pre10"
            break;
        case 11: div = "#pre11"
            break;
        case 12: div = "#pre12"
            break;
        case 14: div = "#pre14"
            break;
        case 16: div = "#pre16"
            break;
        case 17: div = "#pre17"
            break;
        case 18: div = "#pre18"
            break;
        case 19: div = "#pre19"
            break;
        case 20: div = "#pre20"
            break;
        case 21: div = "#pre21"
            break;
        case 22: div = "#pre22"
            break;
        case 23: div = "#pre23"
            break;
        case 32: div = "#pre32"
            break;
        case 35: div = "#pre35"
            break;
        case 37: div = "#pre37"
            break;
        case 38: div = "#pre38"
            break;
        case 39: div = "#pre39"
            break;
        case 40: div = "#pre40"
            break;
        case 41: div = "#pre41"
            break;
        case 42: div = "#pre42"
            break;
        case 43: div = "#pre43"
            break;
        case 44: div = "#pre44"
            break;
        case 45: div = "#pre45"
            break;
        case 46: div = "#pre46"
            break;
        case 47: div = "#pre47"
            break;
        case 48: div = "#pre48"
            break;
        case 49: div = "#pre49"
            break;
        case 50: div = "#pre50"
            break;
        case 51: div = "#pre51"
            break;

    }
    return div;
}


function cualDiv(numero) {

    var div = "";
    switch (numero) {

        case 3: div = "#pregunta9";
            break;
        case 4: div = "#pregunta10"
            break;
        case 11: div = "#pregunta11"
            break;
        case 12: div = "#pregunta12"
            break;
        case 14: div = "#pregunta14"
            break;
        case 16: div = "#pregunta16"
            break;
        case 17: div = "#pregunta17"
            break;
        case 18: div = "#pregunta18"
            break;
        case 19: div = "#pregunta19"
            break;
        case 20: div = "#pregunta20"
            break;
        case 21: div = "#pregunta21"
            break;
        case 22: div = "#pregunta22"
            break;
        case 23: div = "#pregunta23"
            break;
        case 32: div = "#pregunta32"
            break;
        case 35: div = "#pregunta35"
            break;
        case 37: div = "#pregunta37"
            break;
        case 38: div = "#pregunta38"
            break;
        case 39: div = "#pregunta39"
            break;
        case 40: div = "#pregunta40"
            break;
        case 41: div = "#pregunta41"
            break;
        case 42: div = "#pregunta42"
            break;
        case 43: div = "#pregunta43"
            break;
        case 44: div = "#pregunta44"
            break;
        case 45: div = "#pregunta45"
            break;
        case 46: div = "#pregunta46"
            break;
        case 47: div = "#pregunta47"
            break;
        case 48: div = "#pregunta48"
            break;
        case 49: div = "#pregunta49"
            break;
        case 50: div = "#pregunta50"
            break;
        case 51: div = "#pregunta51"
            break;

    }
    return div;
}



function armarDatos(data, tableDiv, Div, numeroDiv, pregunta) {
    var items = data.respuestas_pregunta;
    var table = $(tableDiv);
    var div = $(Div);
    var $linea = $('<tr><td></td></tr>');
    var tdsuperior = '<table><tr><td>' + '<span>' + numeroDiv + ". " + pregunta + '</span> <br /></td></tr></table>';
    div.append(tdsuperior);
    for (var i = 0; i < items.length; i++) {
        var tds = '<tr>';
        tds += '<td ><label>' + items[i].RESPUESTA + ":" + '</label></td>';
        tds += '<td> <input type="' + items[i].TIPO_DATO + '" id="' + items[i].ID_HTML + '"  name="' + items[i].NAME + '"  value="' + items[i].VALUE + '"/></td>'
        tds += '</tr>';
        $linea.append(tds);
        table.append($linea);
    }
}

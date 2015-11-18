var pagina = "ConsultaPacientes.aspx/";

var gridId = "#Datos";
var gridPagerId = "#pagerL";
var gridHId = "#DatosH";
var gridPagerHId = "#pagerLH";
var NoPedirPrefijo = true;
var idPaciente = 0;
var graficos = false;
$(function () {
    $("#EditarAgregar").hide();
    $("#Pacientes").show();
    $("#popupGraficos").hide();

    $("#atras").hide();
    $("#HistorialEpisodios").hide();

    $("#consultar").button().click(function () {
        TraerDatos();

    });
    $("#botonconsultar").button().click(function () {

        TraerHistorial(0);
    });
    $("#Graficos").button().click(function () {
        graficos = true;
        $("#popupGraficos").show();
        $("#FiltrosBu").hide();
        $("#botonconsultar").hide();

        cargarGrafica();
        //$('#popup').dialog({
        //    width: 650, height: 510, modal: true, resizable: false, draggable: false, title: 'Edit', closeOnEscape: false,
        //    open: function (event, ui) {
        //        $(".ui-dialog-titlebar-close", ui.dialog).show()
               

        //    }

        //});

   

    });
    $("#atras").button().click(function () {

        if (graficos) {
            $("#popupGraficos").hide();
            $("#FiltrosBu").show();
            $("#botonconsultar").show();

            graficos = false;

        } else {
            $("#popupGraficos").hide();
            $("#HistorialEpisodios").hide();
            $("#Pacientes").show();
            TraerDatos();
        }

    });
    $("#Cancel").button().click(function () {
        LimpiarDatos();
        $("#EditarAgregar").dialog('close');
    });

    $("#fecha1").datepicker({
        dateFormat: 'dd/mm/yy', dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sa'],
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        showOtherMonths: true,
        // maxDate: new Date(data.aniofechaIngresoMaxima, data.mesfechaIngresoMaxima, data.diafechaIngresoMaxima),
        selectOtherMonths: true, changeYear: true
    });
    $("#fecha2").datepicker({
        dateFormat: 'dd/mm/yy', dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sa'],
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        showOtherMonths: true,
        // maxDate: new Date(data.aniofechaIngresoMaxima, data.mesfechaIngresoMaxima, data.diafechaIngresoMaxima),
        selectOtherMonths: true, changeYear: true
    });
    TraerDatos();

});



function TraerInformacionInicial() {
    DoJsonRequestBusy(pagina, "TraerInformacionInicial", cargarDatosInicales, '{}');
}

function ValidarEditarAgregar() {
    var parametros = {};
    var parametrosNOpasan = {}
    var n = false;
    n = validarYAgregarDatos("#Perfil", "select", "Seleccione un perfil, por favor", "Perfil", parametros);
    if (!n) return;

    n = validarYAgregarDatos("#username", "input", "Ingrese su Username, por favor", "userName", parametros);
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
    if (data.OK == "OK") {
        AlertUI(".:Información", data.mensaje.toString(), function () {
            $("#EditarAgregar").dialog('close');
            TraerDatos();
        });
    }
    else {
        AlertUI(".:Información", data.mensaje.toString());
        return
    }
}
function cargarDatosInicales(jsonrequest) {
    var data = jsonrequest.d;
    if (data.Ok == "Error Consultando información inicial.") {
        AlertUI(".:Error", data.mensaje);
        return false;
    }
    if (data.Ok == "OK") {
        //fillSelect($("#PreguntasSecretas"), data.PREGUNTAS);
        //fillSelect($("#Perfil"), data.PERFILES);
        TraerDatos();
    }
}

function TraerDatos() {
    $("#Pacientes").show();
    $("#HistorialEpisodios").hide();
    $("#atras").hide();
    OnBusy();
    jQuery(gridId).jqGrid('GridUnload');
    jQuery(gridId).jqGrid({
        height: '100%',
        loadtext: "Cargando datos...",
        multiselect: false,
        pager: gridPagerId,
        emptyrecords: "Sin registros",
        rownumbers: true,
        shrinkToFit: true,
        width: 740,
        datatype: "json",
        url: pagina + 'GetGridDataWithPaging',
        rowNum: 15,
        rowList: [15, 30, 45],
        viewsortcols: [true, 'horizontal', true],
        loadonce: false,
        viewrecords: true,
        mtype: 'POST',
        ajaxGridOptions: { contentType: "application/json" },
        serializeGridData: function (postData) {
            if (postData.searchField === undefined) postData.searchField = null;
            if (postData.searchString === undefined) postData.searchString = null;
            if (postData.searchOper === undefined) postData.searchOper = null;
            postData.NumeroIdentificacion = $("#identificacion").val()
            return $.toJSON(postData);
        },

        colNames: ['id', 'Nombre Paciente', 'Número de Identificación','Email', 'Movil'],
        colModel: [
                    {
                        name: 'ID', Index: "ID", sortable: false, width: 100, editable: true, edittype: 'text', hidden: true, editoptions: { readonly: true },
                        editrules: { edithidden: true, required: true }
                    },
                    {
                        name: 'Name', Index: "Name", sortable: false, width: 190, editable: true, edittype: 'text', hidden: false, editoptions: { readonly: true },
                        editrules: { edithidden: true, required: true }
                    },
                    {
                        name: 'NumeroIdentificacion', Index: "NumeroIdentificacion", sortable: false, width: 90, editable: true, edittype: 'text', hidden: false, editoptions: { readonly: true },
                        editrules: { edithidden: true, required: true }
                    },
                    {
                        name: 'Descripcion', Index: "descripcion", sortable: false, width: 90, editable: true, edittype: 'text', hidden: false, editoptions: { readonly: true },
                        editrules: { edithidden: true, required: true }
                    }
                   , {
                       name: 'email', Index: "email", sortable: false, width: 90, editable: true, edittype: 'text', hidden: false, editoptions: { readonly: true },
                       editrules: { edithidden: true, required: true }
                   }
                  

        ],

        jsonReader: {
            root: "d.rows",
            page: "d.page",
            total: "d.total",
            records: "d.records"
        },
        sortname: 'NOMBRE',
        sortorder: 'ASC',
        sortable: false,
        caption: "Pacientes",
        loadComplete: function (data) {
            if (data.d.status == 2) { /*AlertUI(".:INFO", data.d.userMessage); */} else if (data.d.status == 3) {
                document.location.target = "self";
                document.location.href = 'Default.aspx';
            } else {

                $("#dialog-Busy").dialog('close');
            }
        },
        loadError: function (xhr, status, error) { AlertUI(".:ERROR", status.toUpperCase() + ": " + error); },
        prmNames: { page: 'numPage', rows: 'numRows', sort: 'colName', order: 'sortOrder', search: 'isSearch', nd: 'nd', id: 'id', oper: 'oper', editoper: 'edit', addoper: 'add', deloper: 'del', totalrows: 'totalrows' }

    }).navGrid(gridPagerId, {
        edit: false, add: false, del: false, search: false, view: false, refresh: false
    })

    .navButtonAdd(gridPagerId, {
        caption: "", buttonicon: "ui-icon-refresh",

        onClickButton: function (data) { TraerDatos() },
        position: "last", title: "Refrescar", cursor: "pointer"
    })
    .navButtonAdd(gridPagerId, {
        caption: "Historial", buttonicon: "ui-icon-circle-zoomin",
        onClickButton: function (data) { consultarHistorial() },
        position: "last", title: "Historial", cursor: "pointer"

    })
  

}

function Delete() {
    var rowid = $(gridId).jqGrid('getGridParam', 'selrow');

    if ((rowid == null) || (rowid == undefined)) {
        AlertUI(".:Info", "Por favor seleccione un Registro..");
        return false;
    }
    return ConfirmUI("Desea Continuar?", "eliminando este registro?",
        function () {
            var ret = $(gridId).getRowData(rowid);
            var parametros = {};
            parametros.ID = ret.ID;
           

            var Pasar = $.toJSON(parametros);
            DoJsonRequestBusy(pagina, 'Eliminar', resultadoGuardarEditar, Pasar);
        });

}

function consultarHistorial() {
    var rowid = $(gridId).jqGrid('getGridParam', 'selrow');

    if ((rowid == null) || (rowid == undefined)) {
        AlertUI(".:Info", "Por favor seleccione un Registro..");
        return false;
    }

    var ret = $(gridId).getRowData(rowid);
    var parametros = {};
    parametros.ID = ret.ID;
    idPaciente = ret.ID;;
    TraerHistorial(ret.ID);
}


function EditarCrearPOPUP(Editar) {

    var rowid = $(gridId).jqGrid('getGridParam', 'selrow');

    if (Editar) {
        if ((rowid == null) || (rowid == undefined)) {
            AlertUI(".:Info", "Debe seleccionar al menos un registro por favor.");
            return false;
        }
        $("#EditarCrear").val("Update");
        EsEditar = true;
        var ret = $(gridId).getRowData(rowid);
        idActual = ret.ID;

        $("#PrefijoTR").hide();

        $("#NombrePerfil").val(ret.Name);
        $("#Descripcion").val(ret.descripcion);
        $("#Prefijo").val(ret.Prefijo);





        $('#EditarAgregar').dialog({
            width: 450, height: 410, modal: true, resizable: false, draggable: false, title: 'Edit', closeOnEscape: false,
            open: function (event, ui) { $(".ui-dialog-titlebar-close", ui.dialog).show() }
        });
    } else {
        $("#EditarCrear").val("Insert");
        NoPedirPrefijo = false;
        idActual = 0;
        EsEditar = false;
        LimpiarDatos();
        $('#EditarAgregar').dialog({
            width: 450, height: 410, modal: true, resizable: false, draggable: false, title: 'Insert', closeOnEscape: false,
            open: function (event, ui) { $(".ui-dialog-titlebar-close", ui.dialog).show() }
        });
    }
}

function LimpiarDatos() {

    $("#NombrePerfil").val("");
    $("#Descripcion").val("");
    $("#Prefijo").val("");
}


function TraerHistorial(idT) {
    $("#Pacientes").hide();
    $("#HistorialEpisodios").show();
    $("#atras").show();
    OnBusy();
    jQuery(gridHId).jqGrid('GridUnload');
    jQuery(gridHId).jqGrid({
        height: '100%',
        loadtext: "Cargando datos...",
        multiselect: false,
        pager: gridPagerHId,
        emptyrecords: "Sin registros",
        rownumbers: true,
        shrinkToFit: true,
        width: 740,
        datatype: "json",
        url: pagina + 'GetGridDataWithPagingHistorial',
        rowNum: 15,
        rowList: [15, 30, 45],
        viewsortcols: [true, 'horizontal', true],
        loadonce: false,
        viewrecords: true,
        mtype: 'POST',
        ajaxGridOptions: { contentType: "application/json" },
        serializeGridData: function (postData) {
            if (postData.searchField === undefined) postData.searchField = null;
            if (postData.searchString === undefined) postData.searchString = null;
            if (postData.searchOper === undefined) postData.searchOper = null;
            postData.Paciented = idPaciente
            postData.Paciented = idPaciente
            postData.Paciented = idPaciente
            postData.fechaini = $("#fecha1").val();
            postData.fechafin = $("#fecha2").val();

            return $.toJSON(postData);
        },

        colNames: ['id', 'Intensidad', 'Fecha Episodio', 'Duración(Minutos)'],
        colModel: [
                    {
                        name: 'ID', Index: "ID", sortable: false, width: 100, editable: true, edittype: 'text', hidden: true, editoptions: { readonly: true },
                        editrules: { edithidden: true, required: true }
                    },
                    {
                        name: 'Intensidad', Index: "Intensidad", sortable: false, width: 150, editable: true, edittype: 'text', hidden: false, editoptions: { readonly: true },
                        editrules: { edithidden: true, required: true }
                    },
                    {
                        name: 'FechaEpisodio', Index: "FechaEpisodio", sortable: false, width: 90, editable: true, edittype: 'text', hidden: false, editoptions: { readonly: true },
                        editrules: { edithidden: true, required: true }
                    },
                    {
                        name: 'Duracion', Index: "Duracion", sortable: false, width: 90, editable: true, edittype: 'text', hidden: false, editoptions: { readonly: true },
                        editrules: { edithidden: true, required: true }
                    }
                    


        ],

        jsonReader: {
            root: "d.rows",
            page: "d.page",
            total: "d.total",
            records: "d.records"
        },
        sortname: 'NOMBRE',
        sortorder: 'ASC',
        sortable: false,
        caption: "Historial Paciente",
        loadComplete: function (data) {
            if (data.d.status == 2) {
                $("#dialog-Busy").dialog('close');
                AlertUI(".:INFO", data.d.userMessage);
            } else if (data.d.status == 3) {
                document.location.target = "self";
                document.location.href = 'Default.aspx';
            } else {

                $("#dialog-Busy").dialog('close');
            }
        },
        loadError: function (xhr, status, error) { AlertUI(".:ERROR", status.toUpperCase() + ": " + error); },
        prmNames: { page: 'numPage', rows: 'numRows', sort: 'colName', order: 'sortOrder', search: 'isSearch', nd: 'nd', id: 'id', oper: 'oper', editoper: 'edit', addoper: 'add', deloper: 'del', totalrows: 'totalrows' }

    }).navGrid(gridPagerHId, {
        edit: false, add: false, del: false, search: false, view: false, refresh: false
    })

    .navButtonAdd(gridPagerHId, {
        caption: "", buttonicon: "ui-icon-refresh",

        onClickButton: function (data) { TraerHistorial() },
        position: "last", title: "Refrescar", cursor: "pointer"
    })
  

}



function cargarGrafica() {
    var parametros = {};
    parametros.idPaciente = idPaciente;
    parametros.fechaini = $("#fecha1").val();
    parametros.fechafin = $("#fecha2").val();
    var Pasar = $.toJSON(parametros);
    DoJsonRequestBusy(pagina, 'GetPiechartData', CrearGraficas, Pasar);
  
   
}

function CrearGraficas(data){

    var datos = data.d;
   
 
   
    if (datos.status == 1) {
        drawchart(datos.pie);
        columnChart(datos.lineas);
    }
    else {
        
        AlertUI(".:Error", data.userMessage);
        return false;

    }

   

}

function drawchart(dataP) {
    //    // Callback that creates and populates a data table,
    //    // instantiates the pie chart, passes in the data and
    //    // draws it
    var dataValues = dataP;
    var data = new google.visualization.DataTable();

    data.addColumn('string', 'Intensidad');
    data.addColumn('number', 'Cantidad');

    for (var i = 0; i < dataValues.length; i++) {
        data.addRow([dataValues[i].PlanName, dataValues[i].PaymentAmount]);
    }
    // Instantiate and draw our chart, passing in some options
    var chart = new google.visualization.PieChart(document.getElementById('PieChartsDIV'));

    chart.draw(data,
      {
          title: "Reporte Sintomas en rango de fechas",
          position: "top",
          fontsize: "14px",
          chartArea: { width: '50%' },
      });
    

}

    function columnChart(dataValues) {
        // Callback that creates and populates a data table,
        // instantiates the pie chart, passes in the data and
        // draws it.
       
        var data = new google.visualization.DataTable();

        data.addColumn('string', 'Sintoma');
        data.addColumn('number', 'Cantidad');

        for (var i = 0; i < dataValues.length; i++) {
            data.addRow([dataValues[i].nombre_sintoma, dataValues[i].cantidad]);
        }
        // Instantiate and draw our chart, passing in some options
        var chart = new google.visualization.BarChart(document.getElementById('ColumnChartdiv'));

        chart.draw(data,
          {
              title: "Reporte Sintomas",
              position: "top",
              fontsize: "14px",
              chartArea: { width: '50%' },
          });
       
    }



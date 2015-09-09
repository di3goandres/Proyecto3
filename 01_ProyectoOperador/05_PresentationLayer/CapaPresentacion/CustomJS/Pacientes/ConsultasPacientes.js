var pagina = "HistorialEpisodios.aspx/";

var gridId = "#Datos";
var gridPagerId = "#pagerL";
var gridHId = "#DatosH";
var gridPagerHId = "#pagerLH";
var NoPedirPrefijo = true;
var idPaciente = 0;
$(function () {
    TraerDatos();

    $("#EditarAgregar").hide();
    $("#Pacientes").show();
    $("#atras").hide();
    $("#HistorialEpisodios").hide();

    $("#consultar").button().click(function () {
        TraerDatos();

    });

 

    
    $("#atras").button().click(function () {
        TraerDatos();

    });
    $("#Cancel").button().click(function () {
        LimpiarDatos();
        $("#EditarAgregar").dialog('close');
    });

    
   // TraerInformacionInicial();

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
        //fillSelect($("#Perfil  idPaciente = data.id;
        TraerDatos();
    }
}
function TraerDatos(idT) {
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
            postData.Paciented = idPaciente;
            postData.fechaini = "";
            postData.fechafin = "";

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
            if (data.d.status == 2) { /*AlertUI(".:INFO", data.d.userMessage); */ } else if (data.d.status == 3) {
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

        onClickButton: function (data) { TraerDatos() },
        position: "last", title: "Refrescar", cursor: "pointer"
    })


}
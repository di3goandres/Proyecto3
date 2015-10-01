var pagina = "AdministrarUsuarios.aspx/";

var gridId = "#Datos";
var gridPagerId = "#pagerL";
var NoPedirPrefijo = true;
$(function () {
    $("#EditarAgregar").hide();
    $("#EditarCrear").button().click(function () {
        ValidarEditarAgregar();

    });
    $("#Cancel").button().click(function () {
        LimpiarDatos();
        $("#EditarAgregar").dialog('close');
    });

    TraerInformacionInicial();

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
        fillSelect($("#PreguntasSecretas"), data.PREGUNTAS);
        fillSelect($("#Perfil"), data.PERFILES);
        TraerDatos();
    }
}






function TraerDatos() {
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
            return $.toJSON(postData);
        },

        colNames: ['Username', 'Bloqueado', 'Ultima Actividad', 'email', 'Roles'],
        colModel: [
                    {
                        name: 'ID', Index: "ID", sortable: false, width: 100, editable: true, edittype: 'text', hidden: false, editoptions: { readonly: true },
                        editrules: { edithidden: true, required: true }
                    },
                    {
                        name: 'Name', Index: "Name", sortable: false, width: 100, editable: true, edittype: 'text', hidden: true, editoptions: { readonly: true },
                        editrules: { edithidden: true, required: true }
                    }, {
                        name: 'Descripcion', Index: "descripcion", sortable: false, width: 90, editable: true, edittype: 'text', hidden: false, editoptions: { readonly: true },
                        editrules: { edithidden: true, required: true }
                    }
                   , {
                       name: 'email', Index: "email", sortable: false, width: 150, editable: true, edittype: 'text', hidden: false, editoptions: { readonly: true },
                       editrules: { edithidden: true, required: true }
                   }
                   , {
                       name: 'Roles', Index: "Roles", sortable: false, width: 150, editable: true, edittype: 'text', hidden: false, editoptions: { readonly: true },
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
        caption: "Roles del Sistema",
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
    //.navButtonAdd(gridPagerId, {
    //    caption: "Ins", buttonicon: "ui-icon-plusthick",
    //    onClickButton: function (data) { EditarCrearPOPUP(false) },
    //    position: "last", title: "Insert", cursor: "pointer"

    //})
        .navButtonAdd(gridPagerId, {
        caption: "Del", buttonicon: "ui-icon-close",
        onClickButton: function (data) { Delete() },
        position: "last", title: "Eliminar", cursor: "pointer"
    });

    //$('#Datos').contextMenu(menu, { triggerOn: 'contextmenu' });

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



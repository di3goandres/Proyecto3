var pagina = "AdministracionRoles.aspx/";

var gridId = "#Datos";
var gridPagerId = "#pagerL";
var NoPedirPrefijo = true;
$(function () {
    $("#EditarAgregar").hide();
    TraerDatos();
    $("#EditarCrear").button().click(function () {
        ValidarEditarAgregar();

    });
    $("#Cancel").button().click(function () {
        LimpiarDatos();
        $("#EditarAgregar").dialog('close');
    });

});



function ValidarEditarAgregar() {
    var parametros = {};
    var parametrosNOpasan = {}
    var n = false;
    n = validarYAgregarDatos("#NombrePerfil", "input", "Ingrese el nombre del menú ", "NombrePerfil", parametros);
    if (!n) return;


    n = validarYAgregarDatos("#Descripcion", "input", "Ingrese por favor la URl asociada", "Descripcion", parametros);
    if (!n) return;

    if (NoPedirPrefijo) {
        parametros.Prefijo = "";
    } else {
        n = validarYAgregarDatos("#Prefijo", "input", "Ingrese por favor la URl asociada", "Prefijo", parametros);
        if (!n) return;
    }



    parametros.EsEditar = EsEditar;
    parametros.ID = idActual;
    parametros.EsPadre = true;

    var Pasar = $.toJSON(parametros);
    DoJsonRequestBusy(pagina, 'EditarAgregar', resultadoGuardarEditar, Pasar);
}

function resultadoGuardarEditar(jsonrequest) {
    var data = jsonrequest.d;
    if (data.Ok == "OK") {
        $("#EditarAgregar").dialog('close');
        AlertUI(".:Información", data.mensaje.toString(), function () {
            TraerDatos();
        });
    }
    else {
        AlertUI(".:Información", data.mensaje.toString());
        return;
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
        width: 540,
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

        colNames: ['ID', 'Nombre del Perfil', 'Descripcion', 'Prefijo'],
        colModel: [
                    {
                        name: 'ID', Index: "ID", sortable: false, width: 0, editable: true, edittype: 'text', hidden: true, editoptions: { readonly: true },
                        editrules: { edithidden: true, required: true }
                    },
                    {
                        name: 'Name', Index: "Name", sortable: false, width: 100, editable: true, edittype: 'text', hidden: false, editoptions: { readonly: true },
                        editrules: { edithidden: true, required: true }
                    }, {
                        name: 'descripcion', Index: "descripcion", sortable: false, width: 150, editable: true, edittype: 'text', hidden: false, editoptions: { readonly: true },
                        editrules: { edithidden: true, required: true }
                    }
                   , {
                       name: 'Prefijo', Index: "Prefijo", sortable: false, width: 50, editable: true, edittype: 'text', hidden: false, editoptions: { readonly: true },
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
            if (data.d.status == 2) {/* AlertUI(".:INFO", data.d.userMessage); */} else if (data.d.status == 3) {
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
        caption: "Ins", buttonicon: "ui-icon-plusthick",
        onClickButton: function (data) { EditarCrearPOPUP(false) },
        position: "last", title: "Insert", cursor: "pointer"

    }).navButtonAdd(gridPagerId, {
        caption: "Act", buttonicon: "ui-icon-pencil",
        onClickButton: function (data) { EditarCrearPOPUP(true) },
        position: "last", title: "Actualizar", cursor: "pointer"
    }).navButtonAdd(gridPagerId, {
        caption: "Del", buttonicon: "ui-icon-close",
        onClickButton: function (data) { Delete() },
        position: "last", title: "Eliminar", cursor: "pointer"
    });

    //$('#Datos').contextMenu(menu, { triggerOn: 'contextmenu' });

}

function Delete() {
    var rowid = $(gridId).jqGrid('getGridParam', 'selrow');

    if ((rowid == null) || (rowid == undefined)) {
        AlertUI(".:Info", "Please select a record.");
        return false;
    }
    return ConfirmUI("Continue?", "Delete this record?",
        function () {
            var ret = $(gridId).getRowData(rowid);
            var parametros = {};
            parametros.ID = ret.ID;
            parametros.Prefijo = ret.Prefijo;

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
            width: 450, height: 280, modal: true, resizable: false, draggable: false, title: 'Edit', closeOnEscape: false,
            open: function (event, ui) { $(".ui-dialog-titlebar-close", ui.dialog).show() }
        });
    } else {
        $("#EditarCrear").val("Insert");
        NoPedirPrefijo = false;
        idActual = 0;
        EsEditar = false;
        LimpiarDatos();
        $('#EditarAgregar').dialog({
            width: 450, height: 280, modal: true, resizable: false, draggable: false, title: 'Insert', closeOnEscape: false,
            open: function (event, ui) { $(".ui-dialog-titlebar-close", ui.dialog).show() }
        });
    }
}

function LimpiarDatos() {

    $("#NombrePerfil").val("");
    $("#Descripcion").val("");
    $("#Prefijo").val("");
}



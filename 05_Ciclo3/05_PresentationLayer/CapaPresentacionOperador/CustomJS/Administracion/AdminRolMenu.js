var pagina = "AdminRolMenu.aspx/";

var gridId = "#Datos";
var gridPagerId = "#pagerL";
var gridSubMenusId = "#DatosMenus";
var gridSubmenusPagerId = "#pagerMenus";
var idActual = 0;
var permiteInsertarURL = true;
var esHijo = false;
$(function () {
    $("#EditarAgregar").hide();

    $("#EditarCrearMenuHijo").hide();
    $("#ActualizarMenus").hide();


    $("#EditarCrear").button().click(function () {
        ValidarEditarAgregar();

    });
    $("#Cancel").button().click(function () {
        LimpiarDatos();
        $("#EditarAgregar").dialog('close');
    });
    $("#CancelActMenu").button().click(function () {
        LimpiarDatos();
        $("#ActualizarMenus").dialog('close');
    })

    $("#EditarCrearMenu").button().click(function () {
        ValidarEditarAgregarHijo();

    });
    $("#ActuMenu").button().click(function () {
        ValidarActualizarMenuOHijo();

    });

    $("#CancelMenu").button().click(function () {
        LimpiarDatos();
        $("#EditarCrearMenuHijo").dialog('close');
    })

    $('#esPadre').change(function () {
        if ($(this).is(":checked")) {
            $("url").disabled = false;
        } else {
            $("url").disabled = true;

        }
    });


    TraerInformacionInicial();

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

        colNames: ['ID', 'Nombre del Menu', 'URL', 'Activo'],
        colModel: [
                    {
                        name: 'ID', Index: "ID", sortable: false, width: 0, editable: true, edittype: 'text', hidden: true, editoptions: { readonly: true },
                        editrules: { edithidden: true, required: true }
                    },
                    {
                        name: 'Name', Index: "Name", sortable: false, width: 100, editable: true, edittype: 'text', hidden: false, editoptions: { readonly: true },
                        editrules: { edithidden: true, required: true }
                    }, {
                        name: 'Url', Index: "url", sortable: false, width: 50, editable: true, edittype: 'text', hidden: false, editoptions: { readonly: true },
                        editrules: { edithidden: true, required: true }
                    }
                    ,
                    {
                        name: 'Activo', Index: "Activo", sortable: false, width: 50, editoptions: { value: "True:False" }, formatter: "checkbox", formatoptions: { disabled: true }, height: '100%', editable: true, hidden: false, align: 'center',
                        editrules: { edithidden: true, required: true },

                    }

        ],
        onSelectRow: function (id) {
            traerMenuSecundario();
        },
        jsonReader: {
            root: "d.rows",
            page: "d.page",
            total: "d.total",
            records: "d.records"
        },
        sortname: 'NOMBRE',
        sortorder: 'ASC',
        sortable: false,
        caption: "Menú",
        loadComplete: function (data) {
            if (data.d.status == 2) { /*AlertUI(".:INFO", data.d.userMessage);*/ } else if (data.d.status == 3) {
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
    });



}


function ActualizarMenus(EsPadre) {

    if (EsPadre) {

        var rowid = $(gridId).jqGrid('getGridParam', 'selrow');

        if ((rowid == null) || (rowid == undefined)) {
            AlertUI(".:Info", "Please select a record.");
            return false;
        }
        var ret = $(gridId).getRowData(rowid);
        idActual = ret.ID;

        $("#ActualizarMenu").val(ret.Name);

        $("#ActualizarURL").val(ret.Url);

        permiteInsertarURL = false;
        esHijo = false;

        $('#ActualizarMenus').dialog({
            width: 450, height: 280, modal: true, resizable: false, draggable: false, title: 'Edit', closeOnEscape: false,
            open: function (event, ui) { $(".ui-dialog-titlebar-close", ui.dialog).show() }
        });

    } else {
        esHijo = true;

        permiteInsertarURL = true;

        var rowid = $(gridSubMenusId).jqGrid('getGridParam', 'selrow');

        if ((rowid == null) || (rowid == undefined)) {
            AlertUI(".:Info", "Please select a record.");
            return false;
        }

        var ret = $(gridSubMenusId).getRowData(rowid);
        idActual = ret.ID;

        idActual = ret.ID;

        $("#ActualizarMenu").val(ret.Name);
        $("#ActualizarURL").val(ret.Url);


        $('#ActualizarMenus').dialog({
            width: 450, height: 280, modal: true, resizable: false, draggable: false, title: 'Edit', closeOnEscape: false,
            open: function (event, ui) { $(".ui-dialog-titlebar-close", ui.dialog).show() }
        });
    }

}



function DesactivarActivarMenu() {


    var rowid = $(gridId).jqGrid('getGridParam', 'selrow');

    if ((rowid == null) || (rowid == undefined)) {
        AlertUI(".:Info", "Please select a record.");
        return false;
    }

    var ret = $(gridId).getRowData(rowid);

    var ac = ret.Activo;
    if (ac == "True") {

        return ConfirmUI("Continue?", "Desea desactivar /Activar este menú?",
        function () {
            var ret = $(gridId).getRowData(rowid);
            var parametros = {};
            parametros.ID = ret.ID;
            parametros.Activar = false;

            var Pasar = $.toJSON(parametros);
            DoJsonRequestBusy(pagina, 'DesactivarActivar', resultadoGuardarEditar, Pasar);
        });
        return;

    } else {

        return ConfirmUI("Continue?", "Desea Activar este menú?",
      function () {
          var ret = $(gridId).getRowData(rowid);
          var parametros = {};
          parametros.ID = ret.ID;
          parametros.Activar = true;

          var Pasar = $.toJSON(parametros);
          DoJsonRequestBusy(pagina, 'DesactivarActivar', resultadoGuardarEditar, Pasar);
      });
        return;
    }
    return;

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
            var Pasar = $.toJSON(parametros);
            DoJsonRequestBusy(pagina, 'Delete', resultadoGuardarEditar, Pasar);
        });

}




function ValidarEditarAgregar() {
    var parametros = {};
    var parametrosNOpasan = {}
    var n = false;
 

    //parametros.ID = 0;

    parametros.Activar = true;
    parametros.Padre = idPadreActual;
    parametros.Perfil = 0;

    n = validarYAgregarDatos("#Perfil", "select", "Ingrese el nombre del menú ", "ID", parametros);
    if (!n) return;

  


  
    var Pasar = $.toJSON(parametros);
    DoJsonRequestBusy(pagina, 'DesactivarActivar', resultadoGuardarEditarHijos, Pasar);
}


function ValidarActualizarMenuOHijo() {
    var parametros = {};
    var parametrosNOpasan = {}
    var n = false;
    //  var esPadre = ($("#esPadre").is(':checked'));

    n = validarYAgregarDatos("#ActualizarMenu", "input", "Ingrese el nombre del menú ", "MenuName", parametros);
    if (!n) return;
    //parametros.URL = "";
    if (permiteInsertarURL) {
        n = validarYAgregarDatos("#ActualizarURL", "input", "Ingrese por favor la URl asociada", "URL", parametros);
        if (!n) return;
    } else {

        parametros.URL = $("#ActualizarURL").val();

    }




    parametros.ID = idActual;


    var Pasar = $.toJSON(parametros);
    if (esHijo) {
        DoJsonRequestBusy(pagina, 'Actualizar', resultadoGuardarEditarHijos, Pasar);
    } else {
        DoJsonRequestBusy(pagina, 'Actualizar', resultadoGuardarEditar, Pasar);

    }
}



function ValidarEditarAgregarHijo() {
    var parametros = {};
    var parametrosNOpasan = {}
    var n = false;


    n = validarYAgregarDatos("#MenuHijo", "input", "Ingrese el nombre del menú ", "MenuName", parametros);
    if (!n) return;

    n = validarYAgregarDatos("#UrlHijo", "input", "Ingrese por favor la URl asociada", "URL", parametros);
    if (!n) return;



    parametros.EsEditar = EsEditar;
    parametros.ID = idActual;
    parametros.EsPadre = false;

    var Pasar = $.toJSON(parametros);
    DoJsonRequestBusy(pagina, 'EditarAgregar', resultadoGuardarEditarHijos, Pasar);
}

function resultadoGuardarEditar(jsonrequest) {
    var data = jsonrequest.d;
    if (data.Ok == "OK") {
        $("#EditarAgregar").dialog('close');
        $('#ActualizarMenus').dialog('close');
        AlertUI(".:Información", data.mensaje.toString(), function () {
            TraerDatos();
        });
    }
    else {
        AlertUI(".:Información", data.mensaje.toString());
        return;
    }
}


function resultadoGuardarEditarHijos(jsonrequest) {
    var data = jsonrequest.d;
    if (data.Ok == "OK") {
        $("#EditarAgregar").dialog('close');
        $('#ActualizarMenus').dialog('close');
        AlertUI(".:Información", data.mensaje.toString(), function () {
            traerMenuSecundario();
        });
    }
    else {
        AlertUI(".:Información", data.mensaje.toString());
        return;
    }
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
        $("#CompanyName").val(ret.Name);

        $("#SignedIN").val(ret.SignedIn);
        $("#SignedOUT").val(ret.SignedOut);
        $("#CompanyDesc").val(ret.Description);



        $('#EditarAgregar').dialog({
            width: 450, height: 280, modal: true, resizable: false, draggable: false, title: 'Edit', closeOnEscape: false,
            open: function (event, ui) { $(".ui-dialog-titlebar-close", ui.dialog).show() }
        });
    } else {
        $("#EditarCrear").val("Insert");
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
    $("#ActualizarMenu").val("");
    $("#ActualizarURL").val("");
}




function traerMenuSecundario(id) {
    var rowid = $(gridId).jqGrid('getGridParam', 'selrow');
    var ret = $(gridId).getRowData(rowid);
    idPadreActual = ret.ID;
    jQuery(gridSubMenusId).jqGrid('GridUnload');
    jQuery(gridSubMenusId).jqGrid({
        height: '100%',
        loadtext: "Cargando datos...",
        multiselect: false,
        pager: gridSubmenusPagerId,
        emptyrecords: "Sin registros",
        rownumbers: true,
        shrinkToFit: true,
        width: 590,
        datatype: "json",
        url: pagina + 'GetGridDataWithPagingHijos',
        rowNum: 10,
        rowList: [10, 20, 30],
        viewsortcols: [true, 'horizontal', true],
        loadonce: false,
        viewrecords: true,
        mtype: 'POST',
        ajaxGridOptions: { contentType: "application/json" },
        serializeGridData: function (postData) {
            if (postData.searchField === undefined) postData.searchField = null;
            if (postData.searchString === undefined) postData.searchString = null;
            if (postData.searchOper === undefined) postData.searchOper = null;
            postData.IDPADRE = idPadreActual;
            return $.toJSON(postData);
        },

        colNames: ['ID', 'Nombre del Perfil'],
        colModel: [
                    {
                        name: 'ID', Index: "ID", sortable: false, width: 0, editable: true, edittype: 'text', hidden: true, editoptions: { readonly: true },
                        editrules: { edithidden: true, required: true }
                    },
                    {
                        name: 'Name', Index: "Name", sortable: false, width: 100, editable: true, edittype: 'text', hidden: false, editoptions: { readonly: true },
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
        caption: "Perfiles asociados al Menú",
        loadComplete: function (data) { if (data.d.status == 2) { /*AlertUI(".:INFO", data.d.userMessage); */} else if (data.d.status == 3) { AlertUI(".:ERROR", data.d.userMessage); } },
        loadError: function (xhr, status, error) { AlertUI(".:ERROR", status.toUpperCase() + ": " + error); },
        prmNames: { page: 'numPage', rows: 'numRows', sort: 'colName', order: 'sortOrder', search: 'isSearch', nd: 'nd', id: 'id', oper: 'oper', editoper: 'edit', addoper: 'add', deloper: 'del', totalrows: 'totalrows' }

    }).navGrid(gridSubmenusPagerId, {
        edit: false, add: false, del: false, search: false, view: false, refresh: false
    }).navButtonAdd(gridSubmenusPagerId, {
        caption: "", buttonicon: "ui-icon-refresh",
        onClickButton: function (data) { traerMenuSecundario() },
        position: "last", title: "Refrescar", cursor: "pointer"
    })
        .navButtonAdd(gridSubmenusPagerId, {
            caption: "Asociar", buttonicon: "ui-icon-pencil",
            onClickButton: function (data) { Asociar(false) },
            position: "last", title: "Asociar", cursor: "pointer"
        }).navButtonAdd(gridSubmenusPagerId, {
            caption: "Desasociar", buttonicon: "ui-icon-cancel",
            onClickButton: function (data) { DesactivarActivarMenuHijos(true) },
            position: "last", title: "Desasociar", cursor: "pointer"
        });
}


function DesactivarActivarMenuHijos(Desactivar) {


    var rowid = $(gridSubMenusId).jqGrid('getGridParam', 'selrow');

    if ((rowid == null) || (rowid == undefined)) {
        AlertUI(".:Info", "Please select a record.");
        return false;
    }




    return ConfirmUI("Continue?", "Desea desactivar  este perfil para el menú?",
function () {
    var ret = $(gridSubMenusId).getRowData(rowid);
    var parametros = {};
    parametros.ID = ret.Name;

    parametros.Activar = false;
    parametros.Padre = idPadreActual;
    parametros.Perfil = 0;

    var Pasar = $.toJSON(parametros);
    DoJsonRequestBusy(pagina, 'DesactivarActivar', resultadoGuardarEditarHijos, Pasar);
});
    return;

    return;

}


function Asociar(Editar) {

    var rowid = $(gridId).jqGrid('getGridParam', 'selrow');




    if ((rowid == null) || (rowid == undefined)) {
        AlertUI(".:Info", "Debe seleccionar al menos un registro por favor.");
        return false;
    }
    //    $("#EditarCrear").val("Update");
    EsEditar = true;
    var ret = $(gridSubMenusId).getRowData(rowid);

    $('#EditarAgregar').dialog({
        width: 450, height: 200, modal: true, resizable: false, draggable: false, title: 'Asociar', closeOnEscape: false,
        open: function (event, ui) { $(".ui-dialog-titlebar-close", ui.dialog).show() }
    });

}
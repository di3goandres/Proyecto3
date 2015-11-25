var pagina = "Default.aspx/";

var gridBandejaEntradaId = "#Datos";
var gridBandejaEntradaPagerId = "#pagerL";
var gridAdjuntosId = "#DatosAdjuntos";
var gridAdjuntosIdPagerId = "#DatosAdjuntosPagerL";
$(function () {
    $("#VerDetalle").hide();
    $("#DescargarArchivo").button().click(function () {
        Descargar();
    });

    $("#Cerrar").button().click(function () {
        $("#VerDetalle").dialog('close');
    });
    //DoJsonRequestBusy(pagina, 'TraerinformacionInicial', datosIniciales, {});
    LoadUploadFiles('#fileUploadSolicitud', '#ArchivosAdjuntosDiv');
    TraerBandejaEntrada();


});

function datosIniciales(jsonrequest) {
    var data = jsonrequest.d;

    if (data.OK) {
        AlertUI(".:Info", "No tiene los permisos necesarios para estar en esta pagina.");
        //, function () {

        //    document.location.target = "self";
        //    document.location.href = '../Paginas/Default.aspx';
        //})
    }

}



function TraerBandejaEntrada() {
    jQuery(gridBandejaEntradaId).jqGrid('GridUnload');
    jQuery(gridBandejaEntradaId).jqGrid({
        height: '100%',
        loadtext: "Cargando datos...",
        multiselect: false,
        pager: gridBandejaEntradaPagerId,
        emptyrecords: "Sin registros",
        //rownumbers: true,
        shrinkToFit: true,
        width: '850px',
        datatype: "json",
        url: pagina + 'GetGridDataWithPagingBandejaNotificaciones',
        rowNum: 15,
        rowList: [15, 30, 45],
        //viewsortcols: [true, 'horizontal', true],
        //  scroll: true,
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
        colNames: ['ID', 'De', 'Fecha de Envio', 'Asunto', 'Adjuntos', 'Destinatarios', 'Mensaje'],
        colModel: [
                {
                    name: 'ID', Index: "ID", sortable: false, width: 0, editable: true, edittype: 'text', hidden: true, editoptions: { readonly: true },
                    editrules: { edithidden: true, required: true }
                },
                 {
                     name: 'De', Index: "De", sortable: false, width: 250, editable: true, edittype: 'text', hidden: false, editoptions: { readonly: true }, align: 'center',
                     editrules: { edithidden: true, required: true }
                 },
                {
                    name: 'FECHA', Index: "FECHA", sortable: false, width: 250, editable: true, edittype: 'text', hidden: false, editoptions: { readonly: true }, align: 'center',
                    editrules: { edithidden: true, required: true }
                }
                ,
                 {
                     name: 'ASUNTO', Index: "ASUNTO", sortable: false, width: 250, editable: true, edittype: 'text', hidden: false, editoptions: { readonly: true }, align: 'left',
                     editrules: { edithidden: true, required: true }
                 },
                {
                    name: 'Adjunto', Index: "Adjunto", sortable: false, width: 50,
                    editoptions: { value: "True:False" },
                    //formatter: "checkbox", formatoptions: { disabled: true },
                    formatter: imageFormat5, unformat: imageUnFormat,
                    height: '100%', editable: true, hidden: false, align: 'center',
                    editrules: { edithidden: true, required: true },

                },
                    {
                        name: 'Destinatarios', Index: "Destinatarios", sortable: false, width: 250, editable: true, edittype: 'text', hidden: true, editoptions: { readonly: true }, align: 'left',
                        editrules: { edithidden: true, required: true }
                    },
                    {
                        name: 'Mensaje', Index: "Mensaje", sortable: false, width: 250, editable: true, edittype: 'text', hidden: true, editoptions: { readonly: true }, align: 'left',
                        editrules: { edithidden: true, required: true }
                    },



        ],
        ondblClickRow: function (rowid, iRow, iCol, e) {
            var data = $('#grdSearchResults').getRowData(rowid);
            VerDetalles();
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
        caption: "Bandeja de Entrada",
        loadComplete: function (data) {
            if (data.d.status == 2) { AlertUI(".:INFO", data.d.userMessage); }
            else if (data.d.status == 3) {
                AlertUI(".:ERROR", data.d.userMessage, function () {
                    document.location.target = "self";
                    document.location.href = '../Logoff.aspx';
                    return;

                });
            }
            $("tr.jqgrow:odd").addClass('myAltRowClass');
        },
        loadError: function (xhr, status, error) { AlertUI(".:ERROR", status.toUpperCase() + ": " + error); },
        prmNames: { page: 'numPage', rows: 'numRows', sort: 'colName', order: 'sortOrder', search: 'isSearch', nd: 'nd', id: 'id', oper: 'oper', editoper: 'edit', addoper: 'add', deloper: 'del', totalrows: 'totalrows' }
    }).navGrid(gridBandejaEntradaPagerId, {
        edit: false, add: false, del: false, search: false, view: false, refresh: false
    }).navButtonAdd(gridBandejaEntradaPagerId, {
        caption: "Ver Detalle", buttonicon: "ui-icon-folder-open",
        onClickButton: function (data) { VerDetalles() },
        position: "last", title: "Ver Detalle", cursor: "pointer"
    }).navButtonAdd(gridBandejaEntradaPagerId, {
        caption: "Refrescar", buttonicon: "ui-icon-refresh",
        onClickButton: function (data) { TraerBandejaEntrada() },
        position: "first", title: "Refrescar", cursor: "pointer"
    });
}

function imageFormat5(cellvalue, options, rowObject) {
    var adjunto = "../imagenes/icon_paperclip.gif";


    if (rowObject[4] == true) {

        return '<img src="' + adjunto + '" />';
    }
    else {

        return "";

    }
}

function imageUnFormat(cellvalue, options, cell) {
    return $('img', cell).attr('src');
}


function VerDetalles() {
    var rowid = $(gridBandejaEntradaId).jqGrid('getGridParam', 'selrow');

    if ((rowid == null) || (rowid == undefined)) {
        AlertUI(".:Ver Detalle", "Debe seleccionar un registro por favor.");
        return false;
    }
    var ret = $(gridBandejaEntradaId).getRowData(rowid);

    traerDatosAdjuntos(ret.ID);

    $("#EnviadoDesde").text(ret.De);
    $("#fechaEnvio").text(ret.FECHA);
    $("#Destinatarios").text(ret.Destinatarios);
    $("#Asunto").text(ret.ASUNTO);
    $("textarea#cuerpoMensaje").val(ret.Mensaje);


    $('#VerDetalle').dialog({
        width: 800, height: 690, modal: true, resizable: true, draggable: true, title: 'Detalle Mensaje', closeOnEscape: true,
        open: function (event, ui) { $(".ui-dialog-titlebar-close", ui.dialog).show() }
    });


}






function traerDatosAdjuntos(idNotificacion) {
    jQuery(gridAdjuntosId).jqGrid('GridUnload');
    jQuery(gridAdjuntosId).jqGrid({
        height: '250px',
        loadtext: "Cargando datos...",
        multiselect: false,
        pager: gridAdjuntosIdPagerId,
        emptyrecords: "Sin registros",
        rownumbers: true,
        shrinkToFit: true,
        width: '850px',
        datatype: "json",
        url: pagina + 'GetGridDataWithPagingDocumentosAdjuntos',
        rowNum: 15,
        rowList: [15, 30, 45],
        //viewsortcols: [true, 'horizontal', true],
        //  scroll: true,
        loadonce: false,
        viewrecords: true,
        mtype: 'POST',
        ajaxGridOptions: { contentType: "application/json" },
        serializeGridData: function (postData) {
            if (postData.searchField === undefined) postData.searchField = null;
            if (postData.searchString === undefined) postData.searchString = null;
            if (postData.searchOper === undefined) postData.searchOper = null;
            postData.idNotificacion = idNotificacion;
            return $.toJSON(postData);
        },
        colNames: ['ID', 'Nombre',''],
        colModel: [
                {
                    name: 'ID', Index: "ID", sortable: false, width: 0, editable: true, edittype: 'text', hidden: true, editoptions: { readonly: true },
                    editrules: { edithidden: true, required: true }
                },
                 {
                     name: 'nombre', Index: "nombre", sortable: false, width: 300, editable: true, edittype: 'text', hidden: false, editoptions: { readonly: true }, align: 'center',
                     editrules: { edithidden: true, required: true }
                 },
                  {
                      name: 'uid', Index: "uid", sortable: false, width: 300, editable: true, edittype: 'text', hidden: true, editoptions: { readonly: true }, align: 'center',
                      editrules: { edithidden: true, required: true }
                  },




        ],
        ondblClickRow: function (rowid, iRow, iCol, e) {
            var data = $('#grdSearchResults').getRowData(rowid);

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
        caption: "Documentos Adjuntos",
        loadComplete: function (data) {
            if (data.d.status == 2) { AlertUI(".:INFO", data.d.userMessage); }
            else if (data.d.status == 3) {
                AlertUI(".:ERROR", data.d.userMessage, function () {
                    document.location.target = "self";
                    document.location.href = '../Logoff.aspx';
                    return;

                });
            }
            $("tr.jqgrow:odd").addClass('myAltRowClass');
        },
        loadError: function (xhr, status, error) { AlertUI(".:ERROR", status.toUpperCase() + ": " + error); },
        prmNames: { page: 'numPage', rows: 'numRows', sort: 'colName', order: 'sortOrder', search: 'isSearch', nd: 'nd', id: 'id', oper: 'oper', editoper: 'edit', addoper: 'add', deloper: 'del', totalrows: 'totalrows' }
    }).navGrid(gridAdjuntosIdPagerId, {
        edit: false, add: false, del: false, search: false, view: false, refresh: false
    });
    //.navButtonAdd(gridAdjuntosIdPagerId, {
    //    caption: "D", buttonicon: "ui-icon-arrowreturn-1-s",
    //    onClickButton: function (data) { VerDetalles() },
    //    position: "last", title: "Descargar", cursor: "pointer"
    //});
}


function Descargar() {
    var rowid = $(gridAdjuntosId).jqGrid('getGridParam', 'selrow');

    if ((rowid == null) || (rowid == undefined)) {
        AlertUI(".:Descargar", "Debe seleccionar un registro por favor.");
        return false;
    }
    var ret = $(gridAdjuntosId).getRowData(rowid);

    var ventana = window.open('DownloadAdjuntos.aspx?ID_ARCHIVO=' + ret.uid, "Descarga", "");


}
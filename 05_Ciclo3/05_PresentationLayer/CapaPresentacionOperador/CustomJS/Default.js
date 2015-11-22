var pagina = "Default.aspx/";

var gridBandejaEntradaId = "#Datos";
var gridBandejaEntradaPagerId = "#pagerL";

$(function () {
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
        rownumbers: true,
        shrinkToFit: true,
        width: 100%,
        datatype: "json",
        url: pagina + 'GetGridDataWithPagingBandejaNotificaciones',
        rowList: [10, 20, 30],
        viewsortcols: [true, 'horizontal', true],
        scroll: true,
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
        colNames: ['ID', 'De', 'Fecha de Envio', 'Asunto'],
        colModel: [
                {
                    name: 'ID', Index: "ID", sortable: false, width: 0, editable: true, edittype: 'text', hidden: true, editoptions: { readonly: true },
                    editrules: { edithidden: true, required: true }
                },
                 {
                     name: 'De', Index: "De", sortable: false, width: 0, editable: true, edittype: 'text', hidden: false, editoptions: { readonly: true }, align: 'center',
                     editrules: { edithidden: true, required: true }
                 },
                {
                    name: 'FECHA', Index: "FECHA", sortable: false, width: 150, editable: true, edittype: 'text', hidden: false, editoptions: { readonly: true }, align: 'center',
                    editrules: { edithidden: true, required: true }
                }
                ,
                 {
                     name: 'ASUNTO', Index: "ASUNTO", sortable: false, width: 150, editable: true, edittype: 'text', hidden: false, editoptions: { readonly: true }, align: 'center',
                     editrules: { edithidden: true, required: true }
                 },
               
        ],
        ondblClickRow: function (rowid, iRow, iCol, e) {
            var data = $('#grdSearchResults').getRowData(rowid);
         //   SeleccionarporServicios(rowid);
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
            else if (data.d.status == 3)
            {
                AlertUI(".:ERROR", data.d.userMessage, function () {
                    document.location.target = "self";
                    document.location.href = '../Logoff.aspx';
                    return;

                });
            }
        },
        loadError: function (xhr, status, error) { AlertUI(".:ERROR", status.toUpperCase() + ": " + error); },
        prmNames: { page: 'numPage', rows: 'numRows', sort: 'colName', order: 'sortOrder', search: 'isSearch', nd: 'nd', id: 'id', oper: 'oper', editoper: 'edit', addoper: 'add', deloper: 'del', totalrows: 'totalrows' }
    }).navGrid(gridBandejaEntradaPagerId, {
        edit: false, add: false, del: false, search: false, view: false, refresh: false
    }).navButtonAdd(gridBandejaEntradaPagerId, {
        caption: "Ver Datos", buttonicon: "ui-icon-folder-open",
        onClickButton: function (data) { Seleccionar() },
        position: "last", title: "Ver Datos", cursor: "pointer"
    }).navButtonAdd(gridBandejaEntradaPagerId, {
        caption: "Refrescar", buttonicon: "ui-icon-refresh",
        onClickButton: function (data) { TraerBandejaEntrada() },
        position: "first", title: "Refrescar", cursor: "pointer"
    });
}

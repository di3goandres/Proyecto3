var varType;
var varUrl;
var varData;
var varContentType;
var varDataType;
var varProcessData;
var serviceSucceeded;
var serviceError;
var selectRow;

$(function () {
    serviceError = defaultError;
    $(".button").button();
    $(".datepicker").datepicker({dateFormat: 'dd/mm/yy' });
   // loadProperties();
    $(window).bind('resize', function () {}).trigger('resize');
    $(window).scrollLeft("100px");
});
function defaultError(mydata) {
    alert(mydata.Message);
}


function OnBusy() {
    $('.ui-dialog-titlebar-close').hide();
    $("#dialog-Busy").dialog({
        modal: true,
        resizable: false,
        draggable: false,
        //show: 'explode',
        //show: 'clip',      
        //show: 'fold',  
        close: function (event, ui) {
            //            $("select").show();
        }
    });

}

function OnError(msg) {
    alert(msg.responseText);
}

function DoJsonRequestBusy(page, WebMethod, SuccessFunction, Data) {
    OnBusy();
    jQuery.ajax({
        error: function (jsonrequest) {
            $("#dialog-Busy").dialog('close');
            OnError(jsonrequest);
        },
        timeout: 900000, //15 minutos
        url: page + WebMethod,
        data: Data, dataType: 'json', type: 'POST', contentType: "application/json; charset=utf-8",
        success: function (jsonrequest) {
            $("#dialog-Busy").dialog('close');
            SuccessFunction(jsonrequest);
        }
    });
}

function CallService() {
    $.ajax({
        type: varType, //GET or POST or PUT or DELETE verb
        url: varUrl, // Location of the service
        data: varData, //Data sent to server
        contentType: varContentType, // content type sent to server
        dataType: varDataType, //Expected data format from server
        processdata: varProcessData, //True or False
        success: function (data) {//On Successfull service call
            if (serviceSucceeded != null && serviceSucceeded != undefined) {
                serviceSucceeded(getMain(data));
            }
        },
        error: function (data) {
            serviceError(getMain(data.responseText));
        } // When Service call fails
    });
}


function getMain(dObj) {
    if (dObj.hasOwnProperty('d'))
        return dObj.d;
    else
        return dObj;
}

///
///Load a basic grid without grouping
///
function loadGrid(myGrid, pager, caption, mydata, colNames, colModel, onSelectedRow, ondblClickRow, onRightClickRow, height, rowNum, multiselect) {
    var num = (rowNum == undefined) ? 10 : rowNum;
    loadGridGrouping(myGrid, pager, caption, mydata, colNames, colModel, onSelectedRow, ondblClickRow, onRightClickRow, false, null, false, false, height, num, multiselect)
}

///
///Load a grid with grouping
///
function loadGridGrouping(myGrid, pager, caption, mydata, colNames, colModel, onSelectedRow, ondblClickRow, onRightClickRow, grouping, groupingView, footerrow, userDataOnFooter, height, rowNum, multiselect) {

    if (multiselect == undefined)
        multiselect = false;
    myGrid.jqGrid(
    {
        data: mydata.rows,
        datatype: "local",
        height: height,

        colNames: colNames,
        colModel: colModel,
        index: 'Id',
        viewrecords: true,

        ondblClickRow: ondblClickRow,
        onSelectRow: onSelectedRow,
        onRightClickRow: onRightClickRow,

        ///Pager
        pager: pager,
        rownumbers: false,

        gridview: true,
        rowNum: rowNum, rowTotal: mydata.totalRecords, rowList: [10,20, 30],
        caption: caption,

        editurl: 'clientArray',
        grouping: grouping,
        groupingView: groupingView,
        footerrow: footerrow,
        userDataOnFooter: userDataOnFooter,
        multiselect: multiselect
    });

    ///
    ///Add search row
    ///
    myGrid.jqGrid('filterToolbar', { stringResult: true, searchOnEnter: false });

    ///
    //Set width to grids.
    ///
    $('.grid').setGridWidth(890);
}


function loadProperties() {
    ///Add delete config from all jqgrid components
    jQuery.extend(jQuery.jgrid.del,
    {
        caption: "Eliminar",
        msg: "Esta seguro de que desea eliminar?",
        bSubmit: "Eliminar",
        bCancel: "Cancelar",
        processData: "Eliminando...",
        closeAfterDel: true,
        ajaxDelOptions: { contentType: "application/json" },
        serializeDelData: function (postData) { //Seralize data
            if (selectRow == undefined) return;
            var delData = { entity: { Id: selectRow.Id} };
            return delData;
        },
        afterSubmit: function (response, postdata) { //After submit for errors control
            var json = response.responseText;
            var result = eval("(" + (eval("(" + json + ")")).d + ")");
            var retBool = result.Success != undefined ? true : false;
            var retMessage = result.Success != undefined ? result.Success : (result.Error != undefined ? result.Error : "Error, retorno no valido.");
            //if (!retBool) console.log(result.FullMessage)
            return [retBool, retMessage, null];
        }
    });

    ///
    ///Add navigator config from all jqgrid componentss
    ///
    jQuery.extend(jQuery.jgrid.nav,
    {
        edittext: "",
        edittitle: "Editar",
        addtext: "",
        addtitle: "Agregar",
        deltext: "",
        deltitle: "Eliminar",
        searchtext: "",
        searchtitle: "Buscar",
        refreshtext: "",
        refreshtitle: "Refrescar",
        alertcap: "Alerta",
        alerttext: "Porfavor seleccione un elemento de la tabla",
        viewtext: "",
        viewtitle: "Visualización"

    });

    ///
    ///Add add-edit config from all jqgrid components
    ///
    jQuery.extend(jQuery.jgrid.edit,
    {
        addCaption: "Agregar",
        editCaption: "Editar",
        bSubmit: "Guardar",
        bCancel: "Cancelar",
        bClose: "Cerrar",
        saveData: "Los datos han cambiado, desea guardar estos cambios?",
        bYes: "Si",
        bNo: "No",
        bExit: "Cancelar",
        processData: "Processing...",

        ///Form actions
        reloadAfterSubmit: false,
        closeAfterAdd: true,
        closeAfterEdit: true,

        afterSubmit: function (response, postdata) { //After submit for errors control
            var json = response.responseText;
            var result = eval("(" + (eval("(" + json + ")")).d + ")");
            var retBool = result.Success != undefined ? true : false;
            var retMessage = result.Success != undefined ? result.Success : (result.Error != undefined ? result.Error : "Error, retorno no valido.");
            postdata.Id = result.Id;
            //if (!retBool) console.log(result.FullMessage);
            return [retBool, retMessage, postdata.id];
        },
        ajaxEditOptions: { contentType: "application/json" },

        serializeEditData: function (postData) {
            var entity;
            var data = jQuery.extend(true, {}, postData);
            if (data.Id != null && data.Id < 1)
                delete data.Id;
            delete data.id;
            delete data.oper;
            entity = { entity: data }; // GetSeguimiento() };
            return entity;
        }
    });
    $.jgrid.edit.msg.required = "es obligatorio";
}

function abrirCargando() {
    var dialog = '<div id="cargandoVariables" title="Cargando datos..."><label>Cargando, por favor espere</label></div>'
    $("body").append(dialog);
    $("#cargandoVariables").dialog();
}

function cerrarCargando() {
    $("#cargandoVariables").remove();
    //$("#cargandoVariables").dialog();
}